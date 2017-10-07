using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using WorkMate.ViewModels;
using WorkMate.Models;

namespace WorkMate.Controllers
{
    [Authorize]
    public class PaychecksController : Controller
    {
        // GET: Paychecks
        [HttpGet]
        public ViewResult Index()
        {
            string userID = User.Identity.GetUserId();

            List<Paycheck> paychecks = db.Paychecks
                .Include(p => p.PayDate)
                .Include(p => p.Job)
                .Where(p => p.UserID == userID)
                .ToList();

            return View(paychecks);
        }

        // GET: Paychecks/Paycheck
        [HttpGet]
        public ActionResult Paycheck(int paycheckID)
        {
            Paycheck paycheck = db.Paychecks.Find(paycheckID);

            if (paycheck == null)
            {
                return HttpNotFound();
            }
            else if (paycheck.UserID != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }
            else
            {
                paycheck.PaycheckDetails = db.PaycheckDetails
                    .Include(p => p.Paycheck)
                    .Include(p => p.Paycheck.Job)
                    .Include(p => p.Paycheck.PayDate)
                    .Include(p => p.Schedule)
                    .Where(e => e.PaycheckID == paycheck.ID)
                    .ToList();

                PaycheckViewModel paycheckVM = new PaycheckViewModel()
                {
                    Paycheck = paycheck
                };

                return View(paycheckVM);
            }
        }

        private WorkMateDbContext db = new WorkMateDbContext();
    }
}