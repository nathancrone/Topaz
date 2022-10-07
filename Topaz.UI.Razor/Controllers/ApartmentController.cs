using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Microsoft.AspNetCore.Authorization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Topaz.UI.ReportShared;
using Topaz.Common.Extensions;
using Topaz.Common.Enums;
using Topaz.Common.Models;
using Topaz.Common.Models.Extensions;
using Topaz.Data;


namespace Topaz.UI.Razor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly Topaz.Data.TopazDbContext _context;
        private readonly ILogger<ApartmentController> _logger;

        public ApartmentController(Topaz.Data.TopazDbContext context, ILogger<ApartmentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetCurrentTerritory()
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var PublisherId = int.Parse(Claims.FindFirst("PublisherId").Value);

            return _context.TerritoryActivities
                .Where(x => x.PublisherId == PublisherId && x.CheckInDate == null && x.ApartmentTerritory != null)
                .Select(x => new
                {
                    x.TerritoryActivityId,
                    x.ApartmentTerritory.TerritoryId,
                    x.ApartmentTerritory.TerritoryCode,
                    StreetTerritoryId = x.ApartmentTerritory.StreetTerritory.TerritoryId,
                    StreetTerritoryCode = x.ApartmentTerritory.StreetTerritory.TerritoryCode,
                    x.CheckOutDate
                })
                .AsNoTracking()
                .ToList();
        }

        [HttpGet]
        [Route("[action]/{take?}")]
        public IEnumerable<Object> GetAvailableTerritory(int? take = null)
        {
            var LinqResult = _context.ApartmentTerritories.Where(x => !x.InActive && !x.Activity.Any());

            LinqResult = LinqResult.Union(_context.ApartmentTerritories.Where(x => !x.InActive && !x.Activity.Any(y => y.CheckInDate == null)));

            return LinqResult
                .Select(x => new
                {
                    x.TerritoryId,
                    x.TerritoryCode,
                    x.MapLocation, 
                    CheckInDate = x.Activity.Max(y => y.CheckInDate)
                })
                .OrderBy(x => x.CheckInDate)
                .Take(take ?? 3)
                .AsNoTracking()
                .ToList();
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetTerritory()
        {
            var result = _context.ApartmentTerritories.Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
                        StreetTerritoryId = x.StreetTerritory.TerritoryId,
                        StreetTerritoryCode = x.StreetTerritory.TerritoryCode,
                        x.InActive,
                        Activity = x.Activity.OrderByDescending(y => y.CheckOutDate).FirstOrDefault()
                    }
                ).ToList();

            var publisherIds = result.Where(y => y.Activity != null).Select(y => y.Activity.PublisherId).ToList();

            var publishers = _context.Publishers.Where(x => publisherIds.Contains(x.PublisherId)).Select(x => new { x.PublisherId, x.FirstName, x.LastName }).ToList();

            return result.Select(x => new
            {
                x.TerritoryId,
                x.TerritoryCode,
                x.StreetTerritoryId,
                x.StreetTerritoryCode,
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetTerritoryOut()
        {
            var result = _context.ApartmentTerritories.Where(x =>
                x.Activity.Any(y =>
                    !y.CheckOutDate.HasValue && y.CheckInDate.HasValue
                )).Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
                        StreetTerritoryId = x.StreetTerritory.TerritoryId,
                        StreetTerritoryCode = x.StreetTerritory.TerritoryCode,
                        x.InActive,
                        Activity = x.Activity.OrderByDescending(y => y.CheckOutDate).FirstOrDefault()
                    }
                ).ToList();

            var publisherIds = result.Where(y => y.Activity != null).Select(y => y.Activity.PublisherId).ToList();

            var publishers = _context.Publishers.Where(x => publisherIds.Contains(x.PublisherId)).Select(x => new { x.PublisherId, x.FirstName, x.LastName }).ToList();

            return result.Select(x => new
            {
                x.TerritoryId,
                x.TerritoryCode,
                x.StreetTerritoryId,
                x.StreetTerritoryCode,
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetTerritoryIn()
        {
            var result = _context.ApartmentTerritories.Where(x =>
                !x.Activity.Any() || x.Activity.All(y => y.CheckOutDate.HasValue && y.CheckInDate.HasValue)).Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
                        StreetTerritoryId = x.StreetTerritory.TerritoryId,
                        StreetTerritoryCode = x.StreetTerritory.TerritoryCode,
                        x.InActive,
                        Activity = x.Activity.OrderByDescending(y => y.CheckOutDate).FirstOrDefault()
                    }
                ).ToList();

            var publisherIds = result.Where(y => y.Activity != null).Select(y => y.Activity.PublisherId).ToList();

            var publishers = _context.Publishers.Where(x => publisherIds.Contains(x.PublisherId)).Select(x => new { x.PublisherId, x.FirstName, x.LastName }).ToList();

            return result.Select(x => new
            {
                x.TerritoryId,
                x.TerritoryCode,
                x.StreetTerritoryId,
                x.StreetTerritoryCode,
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetReport()
        {
            return _context.ApartmentTerritories.Where(x => !x.InActive).Select(x => new ReportTerritory() {
                TerritoryId = x.TerritoryId, 
                TerritoryCode = x.StreetTerritory.TerritoryCode + " / " + x.TerritoryCode, 
                Activity = x.Activity.Select(y => new ReportActivity() {
                    TerritoryActivityId = y.TerritoryActivityId, 
                    TerritoryId = y.TerritoryId, 
                    PublisherId = y.PublisherId, 
                    FirstName = y.Publisher.FirstName, 
                    LastName = y.Publisher.LastName, 
                    CheckOutDate = y.CheckOutDate.Value, 
                    CheckInDate = y.CheckInDate
                }).OrderBy(y => y.CheckOutDate)
            }).OrderBy(x => x.TerritoryCode);
        }
    }
}
