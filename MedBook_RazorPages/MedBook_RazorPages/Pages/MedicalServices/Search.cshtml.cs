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

namespace MedBook_RazorPages.Pages
{
   /* public aspect MyAspect
    {
        pointcut SumPointCut void MainClass.Sum(int a, int b);

        before SumPointCut(int a, int b)
      {
            Console.WriteLine(
               "The sum of numbers {0} and {1} will be calculated.", a, b);
        }
    }*/
    public class SearchModel : PageModel
    {
        private readonly MedBook_RazorPages.Models.DatabaseContext _context;
        private IEasyCachingProvider _easyCachingProvider;
        private IEasyCachingProviderFactory _easyCachingFactory;
        private DBDataAccess dataBase;

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

        public SearchModel(MedBook_RazorPages.Models.DatabaseContext context, IEasyCachingProviderFactory easyCachingFactory)
        {
            /* DataBase */
            this._context = context;
            this.dataBase = new DBDataAccess(context);
            /* Chaching */
            this._easyCachingFactory = easyCachingFactory;
            this._easyCachingProvider = this._easyCachingFactory.GetCachingProvider("redis1");
            querryDecorator = new QuerryDecorator();
            allTheMedServices= dataBase.getMedicalService();
        }
        public void OnGet()
        {
            locations = dataBase.getLocation();
            var chacedList = this._easyCachingProvider.Get<List<MedicalService>>("all");
            if (chacedList.IsNull)
            {
                listToDisplay = dataBase.getMedicalService();
                this._easyCachingProvider.Set("all", listToDisplay, TimeSpan.FromDays(10));
            }
            else
            {
                var chacedList2 = this._easyCachingProvider.Get<List<MedicalService>>("filtred");
                if (!chacedList2.IsNull)
                {
                    listToDisplay = chacedList2.Value;
                }
                else
                {
                    listToDisplay = dataBase.getMedicalService();
                }
            }
        }

        public IActionResult OnPost()
        {
            listToDisplay = dataBase.getMedicalService(querryDecorator); 
            this._easyCachingProvider.Set("filtred", listToDisplay, TimeSpan.FromDays(10));
            return RedirectToPage("Search");
        }
    }
}
