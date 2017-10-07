using System;

namespace WorkMate.Models
{
    public class PayDate
    {
        public int ID { get; set; }

        public PaymentSchedule PaymentSchedule { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}