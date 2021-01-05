using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedBook_RazorPages.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MedBook_RazorPages.Services;
using Microsoft.Extensions.Logging;
using EasyCaching.Core;
using System;

namespace MedBook_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private DatabaseContext db;

        private readonly ILogger<IndexModel> _logger;

        private IEasyCachingProvider _easyCachingProvider;
        private IEasyCachingProviderFactory _easyCachingFactory;

        public string Msg;

        public IndexModel(DatabaseContext _db, IEasyCachingProviderFactory easyCachingFactory, ILogger<IndexModel> logger)
        {
            db = _db;
            _logger = logger;

            /* Chaching */
            this._easyCachingFactory = easyCachingFactory;
            this._easyCachingProvider = this._easyCachingFactory.GetCachingProvider("redis1");

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
                this._easyCachingProvider.Set("email", acc.Email, TimeSpan.FromDays(10));
                var cookieOptions = new CookieOptions
                {
                    // Set the secure flag, which Chrome's changes will require for SameSite none.
                    // Note this will also require you to be running on HTTPS
                    Secure = true,

                    // Set the cookie to HTTP only which is good practice unless you really do need
                    // to access it client side in scripts.
                    HttpOnly = true,

                    // Add the SameSite attribute, this will emit the attribute with a value of none.
                    // To not emit the attribute at all set the SameSite property to SameSiteMode.Unspecified.
                    SameSite = SameSiteMode.None
                    
                };

                // Add the cookie to the response cookie collection
                Response.Cookies.Append("email", "cookieValue", cookieOptions);
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
