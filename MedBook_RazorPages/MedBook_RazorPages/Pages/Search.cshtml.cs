using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedBook_RazorPages.Models;
using MedBook_RazorPages.Resources;
using System.Runtime.CompilerServices;

namespace MedBook_RazorPages.Pages
{
    public class SearchModel : PageModel
    {
        private readonly MedBook_RazorPages.Models.DatabaseContext _context;

        [BindProperty]
        public MedicalService medicalService { get; set; }

        [BindProperty]
        public QuerryDecorator querryDecorator { get; set; }
        public SearchModel(MedBook_RazorPages.Models.DatabaseContext context)
        {
            _context = context;
        }

        public IList<MedicalService> MedicalService { get;set; }

        public async Task OnGetAsync()
        {
            MedicalService = await _context.MedicalService.ToListAsync();
        }
    }
}
