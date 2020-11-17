using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedBook_RazorPages.Models;

namespace MedBook_RazorPages.Pages
{
    public class NewAppointmentModel : PageModel
    {
        private readonly DatabaseContext _context;

        public NewAppointmentModel(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MedicalServiceId"] = new SelectList(_context.MedicalService, "id", "id");
            return Page();
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
