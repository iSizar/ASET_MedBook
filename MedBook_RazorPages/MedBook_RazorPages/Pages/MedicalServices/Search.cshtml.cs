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
using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using MedBook_RazorPages.Resources.MOP;

namespace MedBook_RazorPages.Pages
{
   
    [PageSecurityMonitor]
    public class SearchModel : PageModel
    {
        private readonly DatabaseContext _context;
        private IEasyCachingProvider _easyCachingProvider;
        private IEasyCachingProviderFactory _easyCachingFactory;
        private FilterOfRows filterOfRows;

        [BindProperty]
        public List<MedicalService> listToDisplay { get; set; }

        [BindProperty]
        public List<Location> locations { get; set; }
        [BindProperty]
        public List<MedicalService> allTheMedServices { get; set; }


        [BindProperty]
        public QuerryDecorator querryDecorator { get; set; }
        [BindProperty]
        public MedicalService MedicalService { get; set; }

        public SearchModel(MedBook_RazorPages.Models.DatabaseContext context, IEasyCachingProviderFactory easyCachingFactory, ILogger<SearchModel> logger)
        {
            /* Chaching */
            this._easyCachingFactory = easyCachingFactory;
            this._easyCachingProvider = this._easyCachingFactory.GetCachingProvider("redis1");

            /* DataBase */
            this._context = context;
            var aspect = new DBLoggingAspect<IDataAccess>(easyCachingFactory, logger);
            IDataAccess service = aspect.Build(new DBDataAccess(context));

            this.filterOfRows = new FilterOfRows(service);
            
            querryDecorator = new QuerryDecorator();
            allTheMedServices = filterOfRows.getMedicalService();
        }
        public void OnGet()
        {
            locations = filterOfRows.getLocation();
            var chacedList = this._easyCachingProvider.Get<List<MedicalService>>("filtred");
            if (!chacedList.IsNull)
            {
                listToDisplay = chacedList.Value;
                this._easyCachingProvider.Remove("filtred");
            }
            else
            {
                listToDisplay = filterOfRows.getMedicalService();
            }
        }

        public IActionResult OnPost()
        {
            listToDisplay = filterOfRows.getMedicalService(querryDecorator); 
            this._easyCachingProvider.Set("filtred", listToDisplay, TimeSpan.FromDays(10));
            return RedirectToPage("Search");
        }
    }
}
