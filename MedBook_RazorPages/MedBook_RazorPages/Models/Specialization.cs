using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models
{
    [Serializable]
    [Table("Specialization")]
    public class Specialization
    {

        [Key]
        public int id { get; set; }
        public string Description { get; set; }
    }
}
