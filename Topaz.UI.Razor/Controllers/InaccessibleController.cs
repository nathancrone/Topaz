using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;
using System.Security.Claims;
using Topaz.Common.Enums;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InaccessibleController : ControllerBase
    {
        private readonly Topaz.Data.TopazDbContext _context;
        private readonly ILogger<InaccessibleController> _logger;

        public InaccessibleController(Topaz.Data.TopazDbContext context, ILogger<InaccessibleController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetCurrentTerritory()
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var UserId = Claims.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            return _context.TerritoryActivities
                .Where(x => x.Publisher.UserId == UserId && x.CheckInDate == null && x.InaccessibleTerritory != null)
                .Select(x => new
                {
                    x.TerritoryActivityId,
                    x.InaccessibleTerritory.TerritoryId,
                    x.InaccessibleTerritory.TerritoryCode,
                    StreetTerritoryId = x.InaccessibleTerritory.StreetTerritory.TerritoryId,
                    StreetTerritoryCode = x.InaccessibleTerritory.StreetTerritory.TerritoryCode,
                    x.CheckOutDate
                })
                .AsNoTracking()
                .ToList();
        }

        [HttpGet]
        [Route("[action]/{take?}")]
        public IEnumerable<Object> GetAvailableTerritory(int? take = null)
        {
            var LinqResult = _context.InaccessibleTerritories.Where(x => !x.InActive && !x.Activity.Any());

            LinqResult = LinqResult.Union(_context.InaccessibleTerritories.Where(x => !x.InActive && !x.Activity.Any(y => y.CheckInDate == null)));

            return LinqResult
                .Select(x => new
                {
                    x.TerritoryId,
                    x.TerritoryCode,
                    StreetTerritoryId = x.StreetTerritory.TerritoryId,
                    StreetTerritoryCode = x.StreetTerritory.TerritoryCode,
                    CheckInDate = x.Activity.Max(y => y.CheckInDate)
                })
                .OrderBy(x => x.CheckInDate)
                .Take(take ?? 3)
                .AsNoTracking()
                .ToList();
        }

        [HttpGet]
        [Route("[action]/{id}/{type}")]
        public IEnumerable<Object> GetAssignments(int id, string type)
        {
            var Assignments = _context.InaccessibleProperties
                .Include(x => x.ContactLists.Where(y => y.InaccessibleContactListId == x.CurrentContactListId))
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.ContactActivity)
                .Include(x => x.ContactLists.Where(y => y.InaccessibleContactListId == x.CurrentContactListId))
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.AssignPublisher)
                .Where(x => x.TerritoryId == id)
                .SelectMany(x => x.ContactLists.SelectMany(y => y.Contacts))
                .AsNoTracking()
                .ToList();

            var FilteredAssignments = Assignments.AsQueryable();

            if (type == "phone")
            {
                FilteredAssignments = FilteredAssignments
                    .Where(x =>
                        !string.IsNullOrEmpty(x.PhoneNumber) &&
                        !x.ContactActivity.Any()
                    );
            }
            else if (type == "vm")
            {
                FilteredAssignments = FilteredAssignments
                    .Where(x =>
                        !string.IsNullOrEmpty(x.PhoneNumber) &&
                        !x.ContactActivity.Any(y =>
                            y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail
                        ) ||
                        x.ContactActivity.Where(y =>
                            y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithoutVoicemail ||
                            y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail
                        ).All(y =>
                            y.PhoneResponseTypeId == (int)PhoneReponseTypeEnum.AnsweredNotGoodTime
                        )
                    );
            }
            else if (type == "letter")
            {
                PhoneReponseTypeEnum[] NoContact = new PhoneReponseTypeEnum[] {
                    PhoneReponseTypeEnum.VoicemailFullOrNotSetUp,
                    PhoneReponseTypeEnum.NoResponseFaxModem,
                    PhoneReponseTypeEnum.NoResponseBusySignal,
                    PhoneReponseTypeEnum.NoResponseNotWorkingNumber,
                    PhoneReponseTypeEnum.NoResponseRingNoAnswer,
                    PhoneReponseTypeEnum.AnsweredImmediateHangup,
                };

                FilteredAssignments = FilteredAssignments
                    .Where(x =>
                        !string.IsNullOrEmpty(x.MailingAddress1) &&
                        !x.ContactActivity.Any(y =>
                            y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter ||
                            y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Email ||
                            y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Text
                        ) &&
                        (
                            string.IsNullOrEmpty(x.PhoneNumber) ||
                            (
                                !string.IsNullOrEmpty(x.PhoneNumber) &&
                                x.ContactActivity.Any(y =>
                                    y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail
                                ) &&
                                x.ContactActivity.Where(y =>
                                    y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail
                                ).All(y =>
                                    Array.IndexOf(NoContact, y.PhoneResponseTypeId) == -1
                                )
                            )
                        )
                    );
            }

            return Assignments;
        }
    }
}
