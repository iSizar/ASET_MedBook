using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models.CalendarModels
{
    public class CalendarWeek
    {
        public CalendarDay[] CalendarDays { get; private set; }

        private CalendarWeek(List<Appointment> appointments)
        { }

        public CalendarWeek CreateCalendarWeek(DateTime firstDayOfTheWeek)
        { 
            throw new NotImplementedException();
        }

        public CalendarWeek CreateCalendarWeek(int firstDayOfTheWeek, int month, int year)
        {
            throw new NotImplementedException();
        }

        private bool IsFirstDayOfTheWeek(DateTime firstDayOfTheWeek)
        {
            throw new NotImplementedException();
        }
    }
}
