using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models
{
    public class Review
    {
        [Key]
        public int id { get; set; }
        public Appointment appointment { get; set; }
        public int stars { get; set; }
    }
}
