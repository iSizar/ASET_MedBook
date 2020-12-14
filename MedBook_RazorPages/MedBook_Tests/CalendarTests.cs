using MedBook_RazorPages.Models;
using MedBook_RazorPages.Models.CalendarModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace MedBook_Tests
{
    public class CalendarTests
    {
        private Users user;
        private MedicalService provider;
        private Appointment appointment;

        public CalendarTests()
        {

        }

        void Initialize() 
        {
            user = new Users();
            user.FirstName = "Prenume";
            user.LastName = "Nume";
            user.Email = "nume@email.com";

            provider = new MedicalService();
            provider.Description = "medical service description";

            appointment = new Appointment();
            appointment.Description = "casual medical appointment";
            appointment.TimeOfAppointment = DateTime.Now;

            appointment.Attach(user, provider);
        }

        [Fact]
        public void TestAttach()
        {
            Initialize();

            Assert.True(appointment.User.FirstName == "Prenume");
            Assert.True(appointment.MedicalService.Description == "medical service description");

            Assert.True(user.Appointments.Count != 0);
        }

        [Fact]
        public void TestCalendarDay()
        {
            Initialize();

            var calendarDay = CalendarDay.CreateCalendarDay(new List<Appointment>() { appointment });

            Assert.Contains(calendarDay.Appointments, appointment => appointment.Description == "casual medical appointment");
        }

        [Fact]
        public void TestCalendarWeek()
        {
            Initialize();

            var calendarWeek = CalendarWeek.CreateCalendarWeek(new List<Appointment>() { appointment });

            Assert.Contains(calendarWeek.CalendarDays,
                CalendarDay => CalendarDay.Appointments.Any(app => app.Description == "casual medical appointment"));
        }

        [Fact]
        public void TestCalendarMonth()
        {
            Initialize();

            var calendarMonth = CalendarMonth.CreateCalendarMonth(new List<Appointment> { appointment });

            Assert.Contains(calendarMonth.CalendarWeeks,
                calendarWeek => calendarWeek.CalendarDays.Any(
                    calendarDay => calendarDay.Appointments.Any(app => app.Description == "casual medical appointment")));
        }

    }
}
