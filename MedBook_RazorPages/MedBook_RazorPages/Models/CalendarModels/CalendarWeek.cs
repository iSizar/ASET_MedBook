using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models.CalendarModels
{
    public class CalendarWeek
    {
        public List<CalendarDay> CalendarDays { get; set; }

        private CalendarWeek()
        {
            this.CalendarDays = new List<CalendarDay>();
        }

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

        public static CalendarWeek CreateCalendarWeek(List<Appointment> appointments)
        {
            if (appointments.Count == 0)
            {
                return new CalendarWeek();
            }

            var dayOfWeek = appointments[0].TimeOfAppointment.DayOfWeek;
            DateTime firstDayOfWeek;

            if (dayOfWeek != DayOfWeek.Monday)
            {
                firstDayOfWeek = appointments[0].TimeOfAppointment.AddDays(dayOfWeek - DayOfWeek.Monday).Date;
            }
            else
            {
                firstDayOfWeek = appointments[0].TimeOfAppointment.Date;
            }

            if (appointments.Any(x => x.TimeOfAppointment.Date < firstDayOfWeek || x.TimeOfAppointment > firstDayOfWeek.AddDays(7)))
            {
                throw new Exception("Appointments not in the same calendar week");
            }

            var calendarWeek = new CalendarWeek();
            foreach (DayOfWeek dayOfTheWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                calendarWeek.CalendarDays.Add(
                    CalendarDay.CreateCalendarDay(
                        appointments.Where(app => app.TimeOfAppointment.DayOfWeek == dayOfTheWeek).ToList()));
            }
            return calendarWeek;
        }

        private bool IsFirstDayOfTheWeek(DateTime firstDayOfTheWeek)
        {
            throw new NotImplementedException();
        }
    }
}
