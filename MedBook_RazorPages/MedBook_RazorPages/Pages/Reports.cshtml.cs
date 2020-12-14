using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedBook_RazorPages.Models;
using EasyCaching.Core;
using MedBook_RazorPages.Resources;

namespace MedBook_RazorPages.Pages
{
    public class ReportsModel : PageModel
    {
        private readonly MedBook_RazorPages.Models.DatabaseContext _context;
        private IEasyCachingProvider _easyCachingProvider;
        private IEasyCachingProviderFactory _easyCachingFactory;
        private DBDataAccess dataBase;
        public ReportsModel(MedBook_RazorPages.Models.DatabaseContext context, IEasyCachingProviderFactory easyCachingFactory)
        {
            /* DataBase */
            this._context = context;
            this.dataBase = new DBDataAccess(context);
            /* Chaching */
            this._easyCachingFactory = easyCachingFactory;
            this._easyCachingProvider = this._easyCachingFactory.GetCachingProvider("redis1");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Review Review { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Review.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
