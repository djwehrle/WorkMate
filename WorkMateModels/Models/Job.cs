using System.ComponentModel.DataAnnotations;

using WorkMate.Models.Validations;

namespace WorkMate.Models
{
    public class Job
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Job Name")]
        public string Name { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        [Display(Name = "Job Type")]
        public JobType JobType { get; set; }

        [Currency]
        [Display(Name = "Pay Rate")]
        public decimal PayRate { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Payment Schedule")]
        public PaymentSchedule PaymentSchedule { get; set; }
    }
}