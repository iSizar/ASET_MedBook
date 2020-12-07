using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models.CalendarModels
{
    public class CalendarDay
    {
        //public DateTime Day { get; set; }

        public List<Appointment> Appointments { get; set; }

        private CalendarDay(List<Appointment> appointments)
        { }

        private CalendarDay()
        {
            Appointments = new List<Appointment>();
        }

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
            if (appointments.Count == 0)
            {
                return new CalendarDay();
            }

            DateTime date = appointments[0].TimeOfAppointment.Date;
            foreach (var appointment in appointments)
            {
                if (appointment.TimeOfAppointment.Date != date)
                    throw new Exception("Appointments not in the same day");
            }

            var calendarDay = new CalendarDay();
            calendarDay.Appointments = appointments;

            return calendarDay;
        }
    }
}
