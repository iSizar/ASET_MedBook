using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedBook_RazorPages.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedBook_RazorPages.Pages
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public Users users { get; set; }

        private DatabaseContext db;

        public ProfileModel(DatabaseContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
            var email = HttpContext.Session.GetString("email");
            users = db.Users.SingleOrDefault(a => a.Email.Equals(email));
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(users.Password))
            {
                users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password);
            }
            else
            {
                users.Password = db.Users.AsNoTracking().SingleOrDefault(a => a.id == users.id).Password;
            }
            db.SaveChanges();
            db.Entry(users).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToPage("Profile");
        }
    }
}