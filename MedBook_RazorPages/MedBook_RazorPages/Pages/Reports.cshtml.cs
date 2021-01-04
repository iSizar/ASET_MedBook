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
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MedBook_RazorPages.Pages
{
    public class ReportsModel : PageModel
    {
        private readonly MedBook_RazorPages.Models.DatabaseContext _context;
        private IEasyCachingProvider _easyCachingProvider;
        private IEasyCachingProviderFactory _easyCachingFactory;
        private DBDataAccess dataBase;
        
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [BindProperty]
        public List<Appointment> appointments { get; set; }
        public ReportsModel(MedBook_RazorPages.Models.DatabaseContext context, IEasyCachingProviderFactory easyCachingFactory)
        {
            /* DataBase */
            this._context = context;
            this.dataBase = new DBDataAccess(context);
            /* Chaching */
            this._easyCachingFactory = easyCachingFactory;
            this._easyCachingProvider = this._easyCachingFactory.GetCachingProvider("redis1");
        }

        [System.Web.Mvc.Route("~/GetMessage")]
        public ActionResult GetMessage()
        {
            string message = "Welcome";
            return new JsonResult(message);
        }

        public void OnGet()
        {
            appointments = dataBase.getAppointment().ToList();
           // await ShowAlertWindow();
           // return Page();
        }

 

        public void OnPostAsync()
        {
            appointments = dataBase.getAppointment().ToList();
            //return Page();
        }
    }
}
