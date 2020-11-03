using System;
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

        public DateTime AppointmentDate { get; set; }

        public Users User { get; set; }
        public MedicalService MedicalService { get; set; }

        public void Attach(Users? user, MedicalService? medicalService)
        {
            throw new NotImplementedException();
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
