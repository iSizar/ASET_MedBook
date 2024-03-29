﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedBook_RazorPages.Models;
using Microsoft.AspNetCore.Http;

namespace MedBook_RazorPages.Pages
{
    public class NewMedicalServiceModel : PageModel
    {
        private readonly DatabaseContext _context;

        public Users user { get; set; }

        [BindProperty]
        public List<Location> locations { get; set; }

        public NewMedicalServiceModel(DatabaseContext context)
        {
            _context = context;
            var email = HttpContext.Session.GetString("email");
            user = context.Users.SingleOrDefault(a => a.Email.Equals(email));
            locations = context.Location.ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MedicalService MedicalService { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MedicalService.Add(MedicalService);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
