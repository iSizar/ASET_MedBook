using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MedBook_RazorPages.Pages
{
    public class WelcomeModel : PageModel
    {
        private readonly ILogger<WelcomeModel> _logger;

        public WelcomeModel(ILogger<WelcomeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Am ajuns in ultima pagina pana acum");
        }
    }
}