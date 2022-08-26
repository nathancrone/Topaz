using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StreetController : ControllerBase
    {
        private readonly Topaz.Data.TopazDbContext _context;
        private readonly ILogger<StreetController> _logger;

        public StreetController(Topaz.Data.TopazDbContext context, ILogger<StreetController> logger)
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
                .Where(x => x.PublisherId == PublisherId && x.CheckInDate == null && x.StreetTerritory != null)
                .Select(x => new
                {
                    x.TerritoryActivityId,
                    x.StreetTerritory.TerritoryId,
                    x.StreetTerritory.TerritoryCode,
                    x.StreetTerritory.MapLocation,
                    x.CheckOutDate
                })
                .AsNoTracking()
                .ToList();
        }

        [HttpGet]
        [Route("[action]/{take?}")]
        public IEnumerable<Object> GetAvailableTerritory(int? take = null)
        {
            var LinqResult = _context.StreetTerritories.Where(x => !x.InActive && !x.Activity.Any());

            LinqResult = LinqResult.Union(_context.StreetTerritories.Where(x => !x.InActive && !x.Activity.Any(y => y.CheckInDate == null)));

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
            var result = _context.StreetTerritories.Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
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
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetTerritoryOut()
        {
            var result = _context.StreetTerritories.Where(x =>
                x.Activity.Any(y =>
                    !y.CheckOutDate.HasValue && y.CheckInDate.HasValue
                )).Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
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
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetTerritoryIn()
        {
            var result = _context.StreetTerritories.Where(x =>
                !x.Activity.Any() || x.Activity.All(y => y.CheckOutDate.HasValue && y.CheckInDate.HasValue)).Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
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
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }


        [HttpGet]
        [Route("[action]/{id}")]
        public IEnumerable<Object> GetAddressBlocks(int id)
        {
            return _context.StreetTerritoryAddressBlocks.Where(x => x.TerritoryId == id).AsNoTracking().ToList();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IEnumerable<Object> GetLocked(int id)
        {
            return _context.InaccessibleTerritories.Where(x => !x.InActive && x.StreetTerritoryId == id).SelectMany(x => x.InaccessibleProperties);
        }
    }
}
