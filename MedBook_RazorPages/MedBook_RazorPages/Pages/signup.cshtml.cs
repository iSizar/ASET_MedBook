using MedBook_RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace MedBook_RazorPages.Pages
{
    public class signupModel : PageModel
    {
        [BindProperty]
        public Users users { get; set; }

        private DatabaseContext db;

        public signupModel(DatabaseContext _db) {
            db = _db;
        }

        public void OnGet()
        {
            users = new Users();
        }

        public IActionResult OnPost()
        {
            Console.WriteLine(users.Email);
            users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password);
            db.Users.Add(users);
            db.SaveChanges();
            return RedirectToPage("index");

        }
    }
}