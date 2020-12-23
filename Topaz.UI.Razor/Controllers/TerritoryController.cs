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

namespace Topaz.UI.Razor.Controllers
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
                PublisherCheckout(id, new PublisherCheckoutDto()
                {
                    PublisherId = Publisher.PublisherId,
                });
            }

            return 0;
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public int UserCheckin(int id, [FromBody] UserCheckinDto dto)
        {
            var Claims = (ClaimsIdentity)User.Identity;
            var PublisherId = int.Parse(Claims.FindFirst("PublisherId").Value);

            //get the publisher by the user id
            var Publisher = _context.Publishers.AsNoTracking().FirstOrDefault(x => x.PublisherId == PublisherId);

            if (Publisher != null)
            {
                var activity = _context.TerritoryActivities.Where(x =>
                    x.TerritoryId == id &&
                    x.CheckOutDate != null &&
                    x.CheckInDate == null).FirstOrDefault();

                // only allow the check in if the territory is checked out
                if (activity != null)
                {

                    if (User.IsInRole("Administrator") || (!User.IsInRole("Administrator") && activity.PublisherId == PublisherId))
                    {
                        activity.CheckInDate = (dto.CheckinDate.HasValue) ? dto.CheckinDate.Value : DateTime.UtcNow;
                        _context.SaveChanges();
                    }
                }
            }

            return 0;
        }
        public class UserCheckinDto
        {
            public DateTime? CheckinDate { get; set; }
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
                UserCheckin(id, new UserCheckinDto() { });
                PublisherCheckout(id, new PublisherCheckoutDto { PublisherId = Publisher.PublisherId });
            }

            return 0;
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public int PublisherCheckout(int id, [FromBody] PublisherCheckoutDto dto)
        {
            //only allow the check out if the territory isn't already checked out
            if (!IsCheckedOut(id))
            {
                var activity = new TerritoryActivity()
                {
                    TerritoryId = id,
                    PublisherId = dto.PublisherId,
                    CheckOutDate = (dto.CheckoutDate.HasValue) ? dto.CheckoutDate.Value : DateTime.UtcNow
                };
                _context.TerritoryActivities.Add(activity);
                _context.SaveChanges();
            }

            return 0;
        }
        public class PublisherCheckoutDto
        {
            public int PublisherId { get; set; }
            public DateTime? CheckoutDate { get; set; }
        }

        // [HttpPost]
        // [Route("[action]")]
        // public int PublisherCheckin(int territoryId)
        // {
        //     //only allow the check in if the territory is checked out
        //     var activity = _context.TerritoryActivities.Where(x =>
        //         x.TerritoryId == territoryId &&
        //         x.CheckOutDate != null &&
        //         x.CheckInDate == null).FirstOrDefault();

        //     activity.CheckInDate = DateTime.UtcNow;
        //     _context.SaveChanges();

        //     return 0;
        // }

        private bool IsCheckedOut(int territoryId)
        {
            return _context.TerritoryActivities.Any(x =>
                x.TerritoryId == territoryId &&
                x.CheckOutDate != null &&
                x.CheckInDate == null);
        }
    }
}
