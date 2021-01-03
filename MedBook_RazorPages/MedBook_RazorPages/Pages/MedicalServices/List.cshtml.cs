using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedBook_RazorPages.Models;

namespace MedBook_RazorPages.Pages.MedicalServices
{
    public class ListModel : PageModel
    {
        private readonly DatabaseContext _context;

        public ListModel(DatabaseContext context)
        {
            _context = context;
        }

        public List<MedicalService> MedicalService { get;set; }

        public IActionResult OnGetAsync()
        {
            MedicalService = _context.MedicalService.ToList();
            return Page();
        }
    }
}
