using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models.CalendarModels
{
    public class CalendarDay
    {
        public DateTime Day { get; set; }

        public List<Appointment> Appointments { get; set; }

        private CalendarDay(List<Appointment> appointments)
        { }

        private CalendarDay()
        { }

        public CalendarDay CreateCalendarDay(DateTime day)
        {
            throw new NotImplementedException();
        }

        public CalendarDay CreateCalendarDay(int day, int month, int year)
        {
            throw new NotImplementedException();
        }

        public static CalendarDay CreateCalendarDay(List<Appointment> appointments)
        {
            throw new NotImplementedException();
        }
    }
}
