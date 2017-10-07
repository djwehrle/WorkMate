using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using WorkMate.Models;

namespace WorkMate.Processes
{
    public class PaycheckCalculator
    {
        public PaycheckCalculator(PayDate payDate)
        {
            PayDate = payDate;
        }

        public void CalculatePaycheck()
        {
            // Clear out any existing paychecks that should be recalculated
            db.Paychecks.RemoveRange(db.Paychecks.Where(p => p.PayDateID == PayDate.ID));
            db.SaveChanges();

            NewPaychecks = new List<Paycheck>();

            CalculatePaycheckFirstOfMonth();

            db.Paychecks.AddRange(NewPaychecks);
            db.SaveChanges();
        }

        private void CalculatePaycheckFirstOfMonth()
        {
            // 1) Get all work between start and end date
            // 2) Calculate pay based on hours and pay

            List<Schedule> schedules = db.Schedules
                .Include(s => s.Job)
                .Where(s =>
                        s.Job.PaymentSchedule == PaymentSchedule.FirstOfMonth
                        && s.StartTime >= PayDate.StartDate
                        && s.StartTime <= PayDate.EndDate)
                .ToList();

            foreach(Schedule schedule in schedules)
            {
                // Create a PaycheckDetails record for each day for each job for each pay type
                // e.g. the hours worked at one job during one day at Regular pay
                // e.g. the hours worked at one job during one day at Overtime pay

                // Create a Paycheck if one doesn't already exist for this job and pay date
                Paycheck newPaycheck = NewPaychecks.SingleOrDefault(p => p.JobID == schedule.JobID && p.PayDateID == PayDate.ID);

                if (newPaycheck == null)
                {
                    newPaycheck = new Paycheck()
                    {
                        JobID = schedule.JobID,
                        PayDateID = PayDate.ID,
                        NetPay = 0m,
                        UserID = schedule.UserID
                    };

                    NewPaychecks.Add(newPaycheck);
                }

                PaycheckDetail newPaycheckDetail = new PaycheckDetail()
                {
                    PaycheckID = newPaycheck.ID,
                    ScheduleID = schedule.ID,
                    PayType = PayType.BasePay, // TODO: Make this dynamic
                    Hours = schedule.Hours, // TODO: Make this dynamic
                    PayRate = schedule.Job.PayRate // TODO: Make this dynamic
                };

                newPaycheck.PaycheckDetails.Add(newPaycheckDetail);
                newPaycheck.NetPay += newPaycheckDetail.Amount;
            }
        }

        private WorkMateDbContext db = new WorkMateDbContext();
        private List<Paycheck> NewPaychecks { get; set; }
        private PayDate PayDate { get; set; }
    }
}