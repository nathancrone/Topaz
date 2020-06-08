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
                })
                .AsNoTracking()
                .ToList();
        }

        [HttpGet]
        [Route("[action]/{take?}")]
        public IEnumerable<Object> GetAvailableTerritory(int? take = 3)
        {
            var LinqResult = _context.StreetTerritories.Where(x => !x.InActive && !x.Activity.Any());

            LinqResult = LinqResult.Union(_context.StreetTerritories.Where(x => !x.InActive && !x.Activity.Any(y => y.CheckInDate == null)));

            return LinqResult
                .Select(x => new
                {
                    x.TerritoryId,
                    x.TerritoryCode,
                    CheckInDate = x.Activity.Max(y => y.CheckInDate)
                })
                .OrderBy(x => x.CheckInDate)
                .Take(take ?? 3)
                .AsNoTracking()
                .ToList();
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public int CurrentUserCheckout(int id)
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var UserId = Claims.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            //get the publisher by the user id
            var Publisher = _context.Publishers.AsNoTracking().FirstOrDefault(x => x.UserId == UserId);

            if (Publisher != null)
            {
                PublisherCheckout(id, Publisher.PublisherId);
            }

            return 0;
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public int CurrentUserCheckin(int id)
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var UserId = Claims.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            //get the publisher by the user id
            var Publisher = _context.Publishers.AsNoTracking().FirstOrDefault(x => x.UserId == UserId);

            if (Publisher != null)
            {
                PublisherCheckin(id, Publisher.PublisherId);
            }

            return 0;
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public int CurrentUserRework(int id)
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var UserId = Claims.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            //get the publisher by the user id
            var Publisher = _context.Publishers.AsNoTracking().FirstOrDefault(x => x.UserId == UserId);

            if (Publisher != null)
            {
                PublisherCheckin(id, Publisher.PublisherId);
                PublisherCheckout(id, Publisher.PublisherId);
            }

            return 0;
        }

        [HttpPost]
        [Route("[action]")]
        public int PublisherCheckout(int territoryId, int publisherId)
        {
            //only allow the check out if the territory isn't already checked out
            if (!IsCheckedOut(territoryId))
            {
                var activity = new TerritoryActivity()
                {
                    TerritoryId = territoryId,
                    PublisherId = publisherId,
                    CheckOutDate = DateTime.UtcNow
                };
                _context.TerritoryActivities.Add(activity);
                _context.SaveChanges();
            }

            return 0;
        }

        [HttpPost]
        [Route("[action]")]
        public int PublisherCheckin(int territoryId, int publisherId)
        {
            //only allow the check in if the territory is checked out
            var activity = _context.TerritoryActivities.Where(x =>
                x.TerritoryId == territoryId &&
                x.PublisherId == publisherId &&
                x.CheckOutDate != null &&
                x.CheckInDate == null).FirstOrDefault();

            if (activity != null)
            {
                activity.CheckInDate = DateTime.UtcNow;
                _context.SaveChanges();
            }

            return 0;
        }

        private bool IsCheckedOut(int territoryId)
        {
            return _context.TerritoryActivities.Any(x =>
                x.TerritoryId == territoryId &&
                x.CheckOutDate != null &&
                x.CheckInDate == null);
        }
    }
}
