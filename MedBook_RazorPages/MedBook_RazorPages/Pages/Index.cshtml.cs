using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedBook_RazorPages.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MedBook_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private DatabaseContext db;

        public string Msg;

        public IndexModel(DatabaseContext _db)
        {
            db = _db;
        }


        public void OnGet()
        {
            users = new Users();
        }
         
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToPage("Index");
        }


        [BindProperty]
        public Users users { get; set; }

        public IActionResult OnPost() {
            var acc = login(users.email, users.password);
            if(false)
            {
                Msg = "Invalid";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("email", "irinel@yahoo.com");
                return RedirectToPage("Welcome");
            }
        }

        private Users login(string email, string password)
        {
            /* var users = db.Users.SingleOrDefault(a => a.email.Equals(email));
            if(users != null)
            {
                if(BCrypt.Net.BCrypt.Verify(password, users.password))
                {
                    return users;
                }
            }*/
            return null;
        }
    }
}
