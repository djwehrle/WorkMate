using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using WorkMate.Models;
using WorkMate.ViewModels;

namespace WorkMate.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        // GET: Jobs
        [HttpGet]
        public ViewResult Index()
        {
            string userID = User.Identity.GetUserId();

            JobsViewModel jobsVM = new JobsViewModel()
            {
                UserID = userID,
                Jobs = db.Jobs.Where(j => j.User.Id == userID).ToList()
            };
            
            return View(jobsVM);
        }

        // GET: Jobs/Edit
        [HttpGet]
        public ActionResult Edit(int jobID)
        {
            Job job = db.Jobs.Find(jobID);

            if (job == null)
            {
                return HttpNotFound();
            }
            else if (job.UserID != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }
            else
            {
                return View(job);
            }
        }

        // POST: Jobs/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                Job existingJob = db.Jobs.Find(job.ID);

                if (existingJob == null)
                {
                    return HttpNotFound();
                }
                else if (existingJob.UserID != User.Identity.GetUserId())
                {
                    return new HttpUnauthorizedResult();
                }
                else
                {
                    existingJob.Name = job.Name;
                    existingJob.Company = job.Company;
                    existingJob.Position = job.Position;
                    existingJob.PayRate = job.PayRate;
                    existingJob.PaymentSchedule = job.PaymentSchedule;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(job);
            }
        }

        // GET: Jobs/New
        [HttpGet]
        public ViewResult New()
        {
            Job job = new Job();
            return View(job);
        }

        // POST: Jobs/New
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Job job)
        {
            if (ModelState.IsValid)
            {
                job.UserID = User.Identity.GetUserId();

                db.Jobs.Add(job);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(job);
            }
        }

        private WorkMateDbContext db = new WorkMateDbContext();
    }
}