using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Topaz.UI.Razor.Pages.Publisher
{
    public class InaccessibleTerritoriesModel : PageModel
    {
        private readonly ILogger<InaccessibleTerritoriesModel> _logger;

        public InaccessibleTerritoriesModel(ILogger<InaccessibleTerritoriesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
