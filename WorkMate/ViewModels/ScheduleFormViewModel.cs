using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using WorkMate.Models;

namespace WorkMate.ViewModels
{
    public class ScheduleFormViewModel
    {
        public ScheduleFormViewModel()
        {
            Schedule = new Schedule();
            Jobs = new List<Job>();
        }

        public Schedule Schedule { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public string EndTime { get; set; }

        public List<Job> Jobs { get; set; }
    }
}