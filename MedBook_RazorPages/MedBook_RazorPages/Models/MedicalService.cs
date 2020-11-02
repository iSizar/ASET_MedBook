using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models
{
    public class MedicalService
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Provider provider { get; set; }
        public DateTime beginingOfProgram { get; set; }
        public DateTime endOfProgram { get; set; }
        public DateTime appointmentTime { get; set; }
    }
}
