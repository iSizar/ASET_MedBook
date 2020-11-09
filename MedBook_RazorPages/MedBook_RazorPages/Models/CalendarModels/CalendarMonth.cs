using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models.CalendarModels
{
    public class CalendarMonth
    {
        public List<CalendarWeek> CalendarWeeks { get; private set; }

        private CalendarMonth(List<Appointment> appointments)
        { }

        private CalendarMonth()
        {
            CalendarWeeks = new List<CalendarWeek>();
        }

        public CalendarMonth CreateCalendarMonth(int month, int? year)
        {
            throw new NotImplementedException();
        }

        public static CalendarMonth CreateCalendarMonth(List<Appointment> appointments)
        {
            var firstDayOfTheMonth = new DateTime(appointments[0].TimeOfAppointment.Year, appointments[0].TimeOfAppointment.Month, 1);
            var daysInCalendarMonth = DateTime.DaysInMonth(firstDayOfTheMonth.Year, firstDayOfTheMonth.Month);

            if (appointments.Any(x => x.TimeOfAppointment.Date < firstDayOfTheMonth || x.TimeOfAppointment > firstDayOfTheMonth.AddDays(daysInCalendarMonth)))
            {
                throw new Exception("Appointments not in the same calendar month");
            }

            CalendarMonth calendarMonth = new CalendarMonth();

            for (int i=0; i < Math.Ceiling(daysInCalendarMonth/7d); i++)
            {
                calendarMonth.CalendarWeeks.Add(CalendarWeek.CreateCalendarWeek(appointments.Where(app =>
                    app.TimeOfAppointment >= firstDayOfTheMonth.AddDays(i * 7) &&
                    app.TimeOfAppointment < firstDayOfTheMonth.AddDays(i * 7 + 7)).ToList()));
            }
            return calendarMonth;
        }
    }
}
