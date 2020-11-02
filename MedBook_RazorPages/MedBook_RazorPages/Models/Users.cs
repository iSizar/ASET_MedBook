using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedBook_RazorPages.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string password { get; set; }
    
        public string email { get; set; }

        public int accountType { get; set; }

        public List<Appointment> Appointments { get; private set; }

        public bool MakeAppointment(DateTime appointmentDate, Provider provider)
        {
            throw new NotImplementedException();
        }

        public void OnNotification()
        {
            throw new NotImplementedException();
        }

        public void CancelAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
