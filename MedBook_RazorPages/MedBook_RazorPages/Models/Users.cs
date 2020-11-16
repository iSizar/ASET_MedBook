using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedBook_RazorPages.Models
{
    [Serializable]
    [Table("Users")]
    public class Users
    {
        [Key]
        public int id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    
        public int UserType { get; set; }

        public List<Appointment> Appointments { get; private set; }

        public Users()
        {
            Appointments = new List<Appointment>();
        }

        public void OnNotification()
        {
            throw new NotImplementedException();
        }

        public void CancelAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }
        public bool AddReview()
        {
            throw new NotImplementedException();
        }
    }
}
