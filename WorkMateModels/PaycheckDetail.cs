using System;

namespace WorkMate.Models
{
    public class PaycheckDetail
    {
        public int ID { get; set; }

        public int PaycheckID { get; set; }

        public virtual Paycheck Paycheck { get; set; }

        public int ScheduleID { get; set; }

        public virtual Schedule Schedule { get; set; }

        public PayType PayType { get; set; }

        public decimal Hours { get; set; }

        public decimal PayRate { get; set; }

        public decimal Amount
        {
            get
            {
                return Math.Round(Hours * PayRate);
            }
        }
    }
}