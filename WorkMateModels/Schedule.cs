using System;
using System.ComponentModel.DataAnnotations;

namespace WorkMate.Models
{
    public class Schedule
    {
        public int ID { get; set; }

        public int JobID { get; set; }

        public virtual Job Job { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        
        [Required]
        public DateTime EndTime { get; set; }

        public decimal Hours
        {
            get
            {
                TimeSpan hours = EndTime - StartTime;
                return (decimal)Math.Round(hours.TotalHours, 2, MidpointRounding.AwayFromZero);
            }
        }
    }
}