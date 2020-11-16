using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedBook_RazorPages.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MedBook_RazorPages.Services;
using Microsoft.Extensions.Logging;

namespace MedBook_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private DatabaseContext db;

        private readonly ILogger<IndexModel> _logger;

        public string Msg;

        public IndexModel(DatabaseContext _db, ILogger<IndexModel> logger)
        {
            db = _db;
            _logger = logger;
        }

       
        public DatabaseContext returnDB() {
            _logger.LogInformation("Se returneaza obiect al bazei de date");
            return db;
        }

        public void OnGet()
        {
            users = new Users();
            _logger.LogInformation("Se face request de tip get");
            /* _logger.LogTrace("This is a trace log");
             _logger.LogDebug("This is a debug log");
             _logger.LogInformation("This is an information log");
             _logger.LogError("This is an error log");
             _logger.LogCritical("This is an critical log");*/
        }
         
        public IActionResult OnGetLogout()
        {
            _logger.LogInformation("Actiune de tip LogOut");
            HttpContext.Session.Remove("email");
            return RedirectToPage("Index");
        }


        [BindProperty]
        public Users users { get; set; }

        public IActionResult OnPost() {
            var acc = login(users.Email, users.Password);
            if(acc == null)
            {
                Msg = "Invalid";
                _logger.LogError("Credentiale introduse gresit");
                return Page();
            }
            else
            {
                _logger.LogInformation("Credentiale introduse corect");
                HttpContext.Session.SetString("email", acc.Email);
                return RedirectToPage("Welcome");
            }
        }

        public Users login(string email, string password)
        {
             var users = db.Users.SingleOrDefault(a => a.Email.Equals(email));
            if(users != null)
            {

                if(BCrypt.Net.BCrypt.Verify(password, users.Password))
                {
                    _logger.LogInformation("Se cripteaza parola");
                    return users;
                }
            }
            _logger.LogCritical("Obiect user null");
            return null;
        }
    }

    public class LoggingId
    {
        public const int DemoCode = 1001;
    }
}
