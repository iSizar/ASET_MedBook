using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models.CalendarModels
{
    public class CalendarMonth
    {
        public CalendarWeek[] CalendarWeeks { get; private set; }

        private CalendarMonth(List<Appointment> appointments)
        { }

        public CalendarMonth CreateCalendarMonth(int month, int? year)
        {
            throw new NotImplementedException();
        }
    }
}
