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
        private DBDataAccess dataBase;

        [BindProperty]
        public List<MedicalService> listToDisplay { get; set; }

        [BindProperty]
        public List<Location> locations { get; set; }


        [BindProperty]
        public QuerryDecorator querryDecorator { get; set; }
        [BindProperty]
        public MedicalService MedicalService { get; set; }

        public SearchModel(MedBook_RazorPages.Models.DatabaseContext context)
        {
            _context = context;
            dataBase = new DBDataAccess(context);
            querryDecorator = new QuerryDecorator();
        }
        public void OnGet()
        {
            locations = dataBase.getLocation();
            listToDisplay = dataBase.getMedicalService();
        }

        public void OnPost()
        {
            listToDisplay = dataBase.getMedicalService(querryDecorator);
        }
    }
}
