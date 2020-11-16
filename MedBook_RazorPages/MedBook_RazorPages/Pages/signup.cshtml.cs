using MedBook_RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace MedBook_RazorPages.Pages
{
    public class signupModel : PageModel
    {
        [BindProperty]
        public Users users { get; set; }

        private DatabaseContext db;

        private readonly ILogger<signupModel> _logger;

        public signupModel(DatabaseContext _db) {
            db = _db;
        }

        public void OnGet()
        {
            users = new Users();
            _logger.LogInformation("Se face request de tip get");
        }

        public IActionResult OnPost()
        {
            _logger.LogInformation("Adaugarea informatiilor in baza de date");
            Console.WriteLine(users.Email);
            _logger.LogInformation("Se cripteaza parola");
            users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password);
            _logger.LogInformation("Se adauga informatiile culese din pagina");
            db.Users.Add(users);
            db.SaveChanges();
            _logger.LogInformation("Informatiile au fost salvate si se face redirect catre pagina index");
            return RedirectToPage("index");

        }
    }
}