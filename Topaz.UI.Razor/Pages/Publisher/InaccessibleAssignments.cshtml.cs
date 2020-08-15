﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Topaz.UI.Razor.Pages.Publisher
{
    public class InaccessibleAssignmentsModel : PageModel
    {
        private readonly ILogger<InaccessibleAssignmentsModel> _logger;

        public InaccessibleAssignmentsModel(ILogger<InaccessibleAssignmentsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
