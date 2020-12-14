using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models
{
    [Serializable]
    [Table("Review")]
    public class Review
    {
        [Key]
        public int id { get; set; }
        
        public int UserId { get; set; }
        public int MedicalServiceId { get; set; }
        MedicalService MedicalService { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public Appointment appointment { get; set; }
        public int stars { get; set; }
    }
}
