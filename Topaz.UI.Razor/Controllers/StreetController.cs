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
        public IEnumerable<Object> Get()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var UserId = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            return _context.TerritoryActivities
                .Where(x => x.Publisher.UserId == UserId && x.CheckInDate == null && x.StreetTerritory != null)
                .Select(x => new { x.TerritoryActivityId, x.StreetTerritory.TerritoryCode, x.CheckOutDate }).ToList();
        }
    }
}
