using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }

        public string ProviderName { get; set; }

        public string Address { get; set; }

        public List<MedicalService> MedicalService { get; private set; }

        public bool AddMedicalService(DateTime appointmentDate, Users user)
        { 
            throw new NotImplementedException();
        }

        public void OnNotification()
        {
            throw new NotImplementedException();
        }
    }
}
