using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Topaz.UI.Razor.Pages.Publisher
{
    public class BusinessTerritoriesModel : PageModel
    {
        private readonly ILogger<BusinessTerritoriesModel> _logger;

        public BusinessTerritoriesModel(ILogger<BusinessTerritoriesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
