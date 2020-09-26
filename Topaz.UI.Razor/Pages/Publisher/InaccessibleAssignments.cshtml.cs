using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Topaz.UI.Razor.Extensibility;
using Microsoft.Extensions.Options;

namespace Topaz.UI.Razor.Pages.Publisher
{
    public class InaccessibleAssignmentsModel : PageModel
    {
        private readonly ILogger<InaccessibleAssignmentsModel> _logger;
        private readonly AppSettingsOptions _options;

        public InaccessibleAssignmentsModel(ILogger<InaccessibleAssignmentsModel> logger, IOptions<AppSettingsOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public void OnGet()
        {

        }
    }
}
