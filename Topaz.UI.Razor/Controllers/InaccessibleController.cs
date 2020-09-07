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
        private static readonly ContactActivityTypeEnum[] phoneActivity = {
            ContactActivityTypeEnum.PhoneWithoutVoicemail,
            ContactActivityTypeEnum.PhoneWithVoicemail
        };

        private static readonly PhoneReponseTypeEnum[] phoneCheck = {
            PhoneReponseTypeEnum.AnsweredRespondedFavorably,
            PhoneReponseTypeEnum.AnsweredNotInterested,
            PhoneReponseTypeEnum.AnsweredDoNotContact,
            PhoneReponseTypeEnum.AnsweredProfanityOrThreatening,
            PhoneReponseTypeEnum.AnsweredNoEnglish
        };

        private static readonly PhoneReponseTypeEnum[] voicemailResponseCheck = {
            PhoneReponseTypeEnum.VoicemailFullOrNotSetUp,
            PhoneReponseTypeEnum.NoResponseFaxModem,
            PhoneReponseTypeEnum.NoResponseBusySignal,
            PhoneReponseTypeEnum.NoResponseNotWorkingNumber,
            PhoneReponseTypeEnum.NoResponseRingNoAnswer,
            PhoneReponseTypeEnum.AnsweredHangUpImmediate
        };

        private static readonly PhoneReponseTypeEnum[] voicemailCheck = {
            PhoneReponseTypeEnum.VoicemailNoName,
            PhoneReponseTypeEnum.VoicemaiNameMatches,
            PhoneReponseTypeEnum.VoicemailDifferentName,
            PhoneReponseTypeEnum.VoicemailBusiness,
            PhoneReponseTypeEnum.AnsweredRespondedFavorably,
            PhoneReponseTypeEnum.AnsweredNotGoodTime,
            PhoneReponseTypeEnum.AnsweredHangUpAfterListening,
            PhoneReponseTypeEnum.AnsweredNotInterested,
            PhoneReponseTypeEnum.AnsweredDoNotContact,
            PhoneReponseTypeEnum.AnsweredProfanityOrThreatening,
            PhoneReponseTypeEnum.AnsweredNoEnglish
        };

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
        public IEnumerable<Object> GetAvailableAssignments(int id, string type)
        {
            var Assignments = _context.InaccessibleProperties
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.AssignPublisher)
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.PhoneType)
                .Where(x => x.TerritoryId == id)
                .SelectMany(x => x.ContactLists.Where(y => y.InaccessibleContactListId == x.CurrentContactListId).SelectMany(y => y.Contacts))
                .AsNoTracking();

            var FilteredAssignments = Assignments;
            if (type == "phone")
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has not been attempted
                    x.ContactActivity.All(y => !phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId))
                );
            }
            else if (type == "vm")
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has been attempted
                    x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                    // there has been no answer
                    x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                    // phone with voicemail has not been attempted
                    x.ContactActivity.All(y => y.ContactActivityTypeId != (int)ContactActivityTypeEnum.PhoneWithVoicemail)
                );
            }
            else if (type == "letter")
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // has mailing address
                    !string.IsNullOrEmpty(x.MailingAddress1) &&
                    (
                        // has no phone number
                        string.IsNullOrEmpty(x.PhoneNumber) ||
                        (
                            // has phone number
                            !string.IsNullOrEmpty(x.PhoneNumber) &&
                            // phone has been attempted
                            x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                            // there has been no answer
                            x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                            // phone with voicemail has been attempted
                            x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail) &&
                            // no voicemail left and no answer
                            x.ContactActivity.Where(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail).All(y => !voicemailCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                            // a letter has NOT been sent
                            !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                        )
                    )
                );
            }
            else
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    (
                        // has phone number
                        !string.IsNullOrEmpty(x.PhoneNumber) &&
                        // phone has been attempted and answered
                        x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                        x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).Any(y => phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId))
                    ) ||
                    (
                        // has phone number
                        !string.IsNullOrEmpty(x.PhoneNumber) &&
                        // phone with voicemail has been attempted
                        x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail) &&
                        // either voicemail left or there was an answer
                        x.ContactActivity.Where(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail).Any(y => voicemailCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId))
                    ) ||
                    (
                        // letter has been sent
                        x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                    )
                );
            }

            return FilteredAssignments;
        }

        [HttpGet]
        [Route("[action]/{id:int}")]
        public IEnumerable<Object> GetContactActivity(int id)
        {
            return _context.InaccessibleContactActivities
                .Where(x => x.InaccessibleContactId == id)
                .Include(x => x.Publisher)
                .Include(x => x.PhoneResponseType)
                .Include(x => x.ContactActivityType)
                .AsNoTracking();
        }

        [HttpGet]
        [Route("[action]")]
        public Object CurrentUserAssignments()
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var UserId = Claims.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            var Assignments = _context.InaccessibleProperties
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.AssignPublisher)
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.PhoneType)
                .SelectMany(x => x.ContactLists.Where(y => y.InaccessibleContactListId == x.CurrentContactListId).SelectMany(y => y.Contacts.Where(z => z.AssignPublisher.UserId == UserId)))
                .AsNoTracking();

            return new
            {
                PhoneWithoutVoicemail = Assignments.Where(x =>
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has not been attempted
                    x.ContactActivity.All(y => !phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId))
                ),
                PhoneWithVoicemail = Assignments.Where(x =>
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has been attempted
                    x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                    // there has been no answer
                    x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                    // phone with voicemail has not been attempted
                    x.ContactActivity.All(y => y.ContactActivityTypeId != (int)ContactActivityTypeEnum.PhoneWithVoicemail)
                ),
                Letter = Assignments.Where(x =>
                    // has mailing address
                    !string.IsNullOrEmpty(x.MailingAddress1) &&
                    (
                        // has no phone number
                        string.IsNullOrEmpty(x.PhoneNumber) ||
                        (
                            // has phone number
                            !string.IsNullOrEmpty(x.PhoneNumber) &&
                            // phone has been attempted
                            x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                            // there has been no answer
                            x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                            // phone with voicemail has been attempted
                            x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail) &&
                            // a letter has NOT been sent
                            !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                        )
                    )
                )
            };
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetPhoneResponseTypes()
        {
            return _context.PhoneResponseTypes.OrderBy(x => x.Name);
        }

        [HttpPost]
        [Route("[action]/{assignee:int}")]
        public int Assign(int assignee, [FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x => x.AssignPublisherId = assignee);
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("[action]")]
        public int Unassign([FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x => x.AssignPublisherId = null);
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("/Inaccessible/ResponseType/{responseTypeId:int}/{activityTypeId:int}/PhoneActivities")]
        public int SavePhoneActivities(int responseTypeId, int activityTypeId, [FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Include(x => x.ContactActivity).Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x =>
            {
                if (x.AssignPublisherId.HasValue)
                {
                    x.ContactActivity.Add(new InaccessibleContactActivity()
                    {
                        PublisherId = x.AssignPublisherId.Value,
                        ActivityDate = DateTime.UtcNow,
                        ContactActivityTypeId = activityTypeId,
                        PhoneResponseTypeId = responseTypeId
                    });
                    x.AssignPublisherId = null;
                }
            });
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("/Inaccessible/LetterActivities")]
        public int SaveLetterActivities([FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Include(x => x.ContactActivity).Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x =>
            {
                if (x.AssignPublisherId.HasValue)
                {
                    x.ContactActivity.Add(new InaccessibleContactActivity()
                    {
                        PublisherId = x.AssignPublisherId.Value,
                        ActivityDate = DateTime.UtcNow,
                        ContactActivityTypeId = (int)ContactActivityTypeEnum.Letter
                    });
                    x.AssignPublisherId = null;
                }
            });
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("/Inaccessible/Contact/{id:int}/PhoneActivity")]
        public int SavePhoneActivity(int id, [FromBody] SavePhoneActivityDto dto)
        {
            var contact = _context.InaccessibleContacts.Find(id);

            if (contact != null && contact.AssignPublisherId.HasValue)
            {
                _context.InaccessibleContactActivities.Add(new InaccessibleContactActivity()
                {
                    InaccessibleContactId = id,
                    PublisherId = contact.AssignPublisherId.Value,
                    ActivityDate = System.DateTime.UtcNow,
                    ContactActivityTypeId = dto.contactActivityTypeId,
                    PhoneResponseTypeId = dto.phoneResponseTypeId,
                    Notes = dto.notes
                });
            }

            contact.AssignPublisherId = null;

            return _context.SaveChanges();
        }
        public class SavePhoneActivityDto
        {
            public int contactActivityTypeId { get; set; }
            public string notes { get; set; }
            public int phoneResponseTypeId { get; set; }
        }
    }
}
