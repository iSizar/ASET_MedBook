using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Models
{
    [Serializable]
    [Table("MedicalService")]
    public class MedicalService
    {
        [Key]
        public int id { get; set; }
        public Users User  { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TargetBodySystem { get; set; }
        public List<Appointment> Appointments { get; set; }
        public TimeSpan DayStartTime { get; set; }
        public TimeSpan DayEndTime { get; set; }
        public TimeSpan AppDefaultMinutes { get; set; }
    }
}
