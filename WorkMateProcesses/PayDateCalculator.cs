using System;
using System.Collections.Generic;
using System.Linq;

using WorkMate.Models;

namespace WorkMate.Processes
{
    public class PayDateCalculator
    {
        public PayDateCalculator(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public void CalculatePayDates()
        {
            CalculatePayDatesFirstOfMonth();

            db.SaveChanges();
        }

        private void CalculatePayDatesFirstOfMonth()
        {
            // Get list of all Pay Dates currently existing within the given date range for this type

            List<PayDate> existingPayDates = db.PayDates
                .Where(p =>
                    p.PaymentSchedule == PaymentSchedule.FirstOfMonth
                    && p.Date >= StartDate
                    && p.Date <= EndDate)
                .ToList();

            // Find all 1sts of the month within given date range
            // If not already in existing PayDates, add it

            DateTime payDate = StartDate.Date;

            if (payDate.Day != 1)
            {
                payDate = payDate.AddMonths(1);
                payDate = new DateTime(payDate.Year, payDate.Month, 1);
            }

            while (payDate <= EndDate)
            {
                if (!existingPayDates.Any(p => p.Date == payDate))
                {
                    PayDate newPayDate = new PayDate()
                    {
                        PaymentSchedule = PaymentSchedule.FirstOfMonth,
                        Date = payDate,
                        StartDate = payDate.AddMonths(-1),
                        EndDate = payDate.AddDays(-1)
                    };

                    db.PayDates.Add(newPayDate);
                }

                payDate = payDate.AddMonths(1);
            }
        }

        private WorkMateDbContext db = new WorkMateDbContext();
        private DateTime EndDate { get; set; }
        private DateTime StartDate { get; set; }
    }
}
