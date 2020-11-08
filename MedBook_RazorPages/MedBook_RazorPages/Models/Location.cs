using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int id { get; set; }
        public string City { get; set; }
        public string StreenName { get; set; }
        public string StreenNr { get; set; }
    }
}
