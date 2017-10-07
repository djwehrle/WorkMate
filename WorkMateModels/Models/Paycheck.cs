using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkMate.Models
{
    public class Paycheck
    {
        public Paycheck()
        {
            PaycheckDetails = new List<PaycheckDetail>();
        }

        public int ID { get; set; }

        public int JobID { get; set; }

        public virtual Job Job { get; set; }

        public int PayDateID { get; set; }

        public virtual PayDate PayDate { get; set; }

        [Display(Name = "Net Pay")]
        public decimal NetPay { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }

        public ICollection<PaycheckDetail> PaycheckDetails { get; set; }
    }
}