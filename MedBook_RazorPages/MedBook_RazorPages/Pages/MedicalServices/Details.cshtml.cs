using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedBook_RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedBook_RazorPages.Pages
{
    public class MedicalServiceModel : PageModel
    {
        private DatabaseContext db;
        private int medicalServiceId;

        [BindProperty]
        public MedicalService medicalService { get; set; }
        
        [BindProperty]
        public List<Appointment> appointments { get; set; }

        public MedicalServiceModel(DatabaseContext _db)
        {
            db = _db;
        }

        //[Route("/MedicalService/{id:int}")]
        public IActionResult OnGet(int id)
        {
            medicalServiceId = id;
            medicalService = db.MedicalService.SingleOrDefault(x => x.id == id);
            appointments = db.Appointment.Where(a => a.MedicalServiceId == id).ToList();
            return Page();
        }
    }
}