﻿using System;
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
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public TimeSpan DayStartTime { get; set; }
        public TimeSpan DayEndTime { get; set; }
        public TimeSpan AppDefaultMinutes { get; set; }
    }
}
