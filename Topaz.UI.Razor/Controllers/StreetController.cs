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

namespace MyApi.Controllers
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
            var UserId = Claims.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            return _context.TerritoryActivities
                .Where(x => x.Publisher.UserId == UserId && x.CheckInDate == null && x.StreetTerritory != null)
                .Select(x => new
                {
                    x.TerritoryActivityId,
                    x.StreetTerritory.TerritoryId,
                    x.StreetTerritory.TerritoryCode,
                    x.CheckOutDate
                }).ToList();
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetAvailableTerritory()
        {
            var LinqResult = _context.StreetTerritories.Where(x => !x.InActive && !x.Activity.Any());

            LinqResult = LinqResult.Union(_context.StreetTerritories.Where(x => !x.InActive && !x.Activity.Any(y => y.CheckInDate == null)));

            return LinqResult.Select(x => new
            {
                x.TerritoryId,
                x.TerritoryCode,
                CheckInDate = x.Activity.Max(y => y.CheckInDate)
            }).OrderBy(x => x.CheckInDate).Take(3).ToList();
        }

        [HttpPost]
        [Route("[action]")]
        public int CheckOutTerritory(int territoryId)
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var UserId = Claims.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            //only allow the check out if the territory isn't already checked out
            if (!_context.TerritoryActivities.Any(x => x.TerritoryId == territoryId && x.CheckOutDate != null && x.CheckInDate == null))
            {
                //get the publisher by the user id
                var Publisher = _context.Publishers.FirstOrDefault(x => x.UserId == UserId);

                if (Publisher != null)
                {
                    var activity = new TerritoryActivity()
                    {
                        PublisherId = Publisher.PublisherId,
                        CheckOutDate = DateTime.UtcNow
                    };
                    _context.TerritoryActivities.Add(activity);
                    _context.SaveChanges();
                }
            }

            return 0;
        }
    }
}
