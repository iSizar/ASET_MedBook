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
            var acc = login(users.Email, users.Password);
            if(acc == null)
            {
                Msg = "Invalid";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("email", acc.Email);
                return RedirectToPage("Welcome");
            }
        }

        private Users login(string email, string password)
        {
            var users = db.Users.SingleOrDefault(a => a.Email.Equals(email));
            if(users != null)
            {
                if(BCrypt.Net.BCrypt.Verify(password, users.Password))
                {
                    return users;
                }
            }
            return null;
        }
    }
}
