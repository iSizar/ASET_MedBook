﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public int PacientId { get; set; }
        public int MedicalServiceId { get; set; }
        public string Description { get; set; }
        public DateTime TimeOfAppointment { get; set; }
        public TimeSpan AppointmentDuration { get; set; }
        public Users User { get; set; }
        public MedicalService MedicalService { get; set; }

        public void Attach(Users? user, MedicalService? medicalService)
        {
            User = user;
            MedicalService = medicalService;

            user.Appointments.Add(this);
            medicalService.Appointments.Add(this);
        }

        public void Detach(Users? user, MedicalService? medicalService)
        {
            throw new NotImplementedException();
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
