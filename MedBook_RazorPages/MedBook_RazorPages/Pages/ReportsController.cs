using System;
using System.Collections.Generic;
using System.Linq;
using EasyCaching.Core;
using MedBook_RazorPages.Models;
using MedBook_RazorPages.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MedBook_RazorPages.Pages
{

    public class AppointmentToJson
    {
        public DateTime date;
        public int nrAppointments;
    };
    public class ReportsController : Controller
    {
        private readonly MedBook_RazorPages.Models.DatabaseContext _context;
        private IEasyCachingProvider _easyCachingProvider;
        private IEasyCachingProviderFactory _easyCachingFactory;
        private FilterOfRows filterOfRows;

        [BindProperty]
        public List<Appointment> appointments { get; set; }
        public ReportsController(MedBook_RazorPages.Models.DatabaseContext context, IEasyCachingProviderFactory easyCachingFactory, ILogger<PageModel> logger)
        {

            /* Chaching */
            this._easyCachingFactory = easyCachingFactory;
            this._easyCachingProvider = this._easyCachingFactory.GetCachingProvider("redis1");

            /* DataBase */
            this._context = context;
            var aspect = new DBLoggingAspect<IDataAccess>(easyCachingFactory, logger);
            IDataAccess service = aspect.Build(new DBDataAccess(context));

            this.filterOfRows = new FilterOfRows(service);
        }

        [Route("~/GetAppointments")]
        [HttpGet]
        public ActionResult GetAppointments()
        {
            List<AppointmentToJson> listret = new List<AppointmentToJson>();
            List<IGrouping<DateTime, Appointment>> appList = filterOfRows.getAppointment().Where(app => app.MedicalServiceId == 1)
                .GroupBy(app => app.TimeOfAppointment)
                .ToList();
            foreach (var app in appList)
            {
                listret.Add(new AppointmentToJson { date = app.Key, nrAppointments = app.Count() });
            }
            listret.Add( new AppointmentToJson { date = new DateTime(2020, 05, 1), nrAppointments = 600 });
            listret.Add( new AppointmentToJson { date = new DateTime(2020, 06, 1), nrAppointments = 250 });
            listret.Add( new AppointmentToJson { date = new DateTime(2020, 07, 1), nrAppointments = 150 });
            listret.Add( new AppointmentToJson { date = new DateTime(2020, 08, 1), nrAppointments = 100 });
            listret.Add( new AppointmentToJson { date = new DateTime(2020, 09, 1), nrAppointments = 400 });
            // = app.(app => new AppointmentToJson(app.TimeOfAppointment));
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(listret);
            return new JsonResult(json);
        }

    }
}
