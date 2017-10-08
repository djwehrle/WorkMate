using WorkMate.DTOs;
using WorkMate.Models;

namespace WorkMate.Mappers
{
    public static class PaycheckMapper
    {
        public static PaycheckDTO MapPaycheck(Paycheck row)
        {
            if (row == null)
            {
                return null;
            }
            else
            {
                return new PaycheckDTO()
                {
                    ID = row.ID,
                    PayDate = row.PayDate.Date.ToShortDateString(),
                    JobName = row.Job.Name,
                    StartDate = row.PayDate.StartDate.ToShortDateString(),
                    EndDate = row.PayDate.EndDate.ToShortDateString(),
                    NetPay = row.NetPay
                };
            }
        }
    }
}