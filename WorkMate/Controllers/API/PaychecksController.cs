using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WorkMate.DTOs;
using WorkMate.Mappers;
using WorkMate.Models;
using WorkMate.Processes;

namespace WorkMate.Controllers.API
{
    [RoutePrefix("api/Paychecks")]
    public class PaychecksController : BaseAPIController
    {
        public PaychecksController(WorkMateDbContext dbWorkMate)
            : base(dbWorkMate)
        {
        }

        [Route]
        [HttpGet]
        public IHttpActionResult Paychecks()
        {
            try
            {
                // Calculate Pay Dates
                DateTime now = DateTime.Now;

                PayDateCalculator payDateCalculator = new PayDateCalculator(now, now.AddMonths(1));
                payDateCalculator.CalculatePayDates();

                List<PayDate> payDates = dbWorkMate.PayDates.ToList();

                foreach (PayDate payDate in payDates)
                {
                    PaycheckCalculator paycheckCalculator = new PaycheckCalculator(dbWorkMate, payDate);
                    paycheckCalculator.CalculatePaycheck();
                }

                string userID = User.Identity.GetUserId();

                List<Paycheck> paychecks = dbWorkMate.Paychecks
                    .Include(p => p.Job)
                    .Include(p => p.PayDate)
                    .Where(p => p.UserID == userID)
                    .ToList();

                List<PaycheckDTO> paycheckDTOs = paychecks
                    .Select(p => PaycheckMapper.MapPaycheck(p))
                    .ToList();

                return Ok(paycheckDTOs);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}