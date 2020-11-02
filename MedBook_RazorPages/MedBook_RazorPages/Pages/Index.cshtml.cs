using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedBook_RazorPages.Models;

namespace MedBook_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        /*private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }*/

        public void OnGet()
        {
            users = new Users();
        }


        [BindProperty]
        public Users users { get; set; }
    }
}
