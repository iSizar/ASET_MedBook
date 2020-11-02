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

        public void SignUpModel(DatabaseContext _db) {
            db = _db;
        }

        public void OnGet()
        {
            users = new Users();
        }

        public RedirectToPageResult OnPost()
        {
            Console.WriteLine(users.email);
            users.password = BCrypt.Net.BCrypt.HashPassword(users.password);
            db.Users.Add(users);
            db.SaveChanges();
            return RedirectToPage("index");

        }
    }
}