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
    public class TerritoryController : ControllerBase
    {
        private readonly Topaz.Data.TopazDbContext _context;
        private readonly ILogger<TerritoryController> _logger;

        public TerritoryController(Topaz.Data.TopazDbContext context, ILogger<TerritoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public int CurrentUserCheckout(int id)
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var PublisherId = int.Parse(Claims.FindFirst("PublisherId").Value);

            //get the publisher by the user id
            var Publisher = _context.Publishers.AsNoTracking().FirstOrDefault(x => x.PublisherId == PublisherId);

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
            var PublisherId = int.Parse(Claims.FindFirst("PublisherId").Value);

            //get the publisher by the user id
            var Publisher = _context.Publishers.AsNoTracking().FirstOrDefault(x => x.PublisherId == PublisherId);

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
            var PublisherId = int.Parse(Claims.FindFirst("PublisherId").Value);

            //get the publisher by the user id
            var Publisher = _context.Publishers.AsNoTracking().FirstOrDefault(x => x.PublisherId == PublisherId);

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
