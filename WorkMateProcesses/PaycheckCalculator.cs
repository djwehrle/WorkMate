using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WorkMate.Models;

namespace WorkMate.Processes
{
    public class PaycheckCalculator
    {
        public PaycheckCalculator(WorkMateDbContext dbWorkMate, PayDate payDate)
        {
            if (dbWorkMate == null)
            {
                throw new ArgumentNullException("dbWorkMate");
            }

            this.dbWorkMate = dbWorkMate;
            this.payDate = payDate;
        }

        public void CalculatePaycheck()
        {
            // Clear out any existing paychecks that should be recalculated
            dbWorkMate.Paychecks.RemoveRange(dbWorkMate.Paychecks.Where(p => p.PayDateID == payDate.ID));
            dbWorkMate.SaveChanges();

            newPaychecks = new List<Paycheck>();

            CalculatePaycheckFirstOfMonth();

            dbWorkMate.Paychecks.AddRange(newPaychecks);
            dbWorkMate.SaveChanges();
        }

        private void CalculatePaycheckFirstOfMonth()
        {
            // 1) Get all work between start and end date
            // 2) Calculate pay based on hours and pay

            List<Schedule> schedules = dbWorkMate.Schedules
                .Include(s => s.Job)
                .Where(s =>
                        s.Job.PaymentSchedule == PaymentSchedule.FirstOfMonth
                        && s.StartTime >= payDate.StartDate
                        && s.StartTime <= payDate.EndDate)
                .ToList();

            foreach(Schedule schedule in schedules)
            {
                // Create a PaycheckDetails record for each day for each job for each pay type
                // e.g. the hours worked at one job during one day at Regular pay
                // e.g. the hours worked at one job during one day at Overtime pay

                // Create a Paycheck if one doesn't already exist for this job and pay date
                Paycheck newPaycheck = newPaychecks.SingleOrDefault(p => p.JobID == schedule.JobID && p.PayDateID == payDate.ID);

                if (newPaycheck == null)
                {
                    newPaycheck = new Paycheck()
                    {
                        JobID = schedule.JobID,
                        PayDateID = payDate.ID,
                        NetPay = 0m,
                        UserID = schedule.UserID
                    };

                    newPaychecks.Add(newPaycheck);
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

        private WorkMateDbContext dbWorkMate;
        private List<Paycheck> newPaychecks;
        private PayDate payDate;
    }
}