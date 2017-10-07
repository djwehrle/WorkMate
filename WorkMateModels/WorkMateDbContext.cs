using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WorkMate.Models
{
    public class WorkMateDbContext : IdentityDbContext<User>
    {
        public WorkMateDbContext()
            : base("WorkMateDbContext", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Paycheck> Paychecks { get; set; }
        public virtual DbSet<PaycheckDetail> PaycheckDetails { get; set; }
        public virtual DbSet<PayDate> PayDates { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }

        public static WorkMateDbContext Create()
        {
            return new WorkMateDbContext();
        }
    }
}