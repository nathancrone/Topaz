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
    public class PublisherController : ControllerBase
    {
        private readonly Topaz.Data.TopazDbContext _context;
        private readonly ILogger<TerritoryController> _logger;

        public PublisherController(Topaz.Data.TopazDbContext context, ILogger<TerritoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]/{token?}")]
        public IEnumerable<Object> GetPublisherSelectOptions(string token)
        {
            return _context.Publishers
                .Where(x => !x.IsHidden && x.FirstName.ToLower().Contains(token.ToLower()) || x.LastName.ToLower().Contains(token.ToLower()))
                .Select(x => new { id = x.PublisherId, name = $"{x.LastName}, {x.FirstName}" })
                .AsNoTracking()
                .ToList();
        }
    }
}
