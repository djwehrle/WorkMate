using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorkMate.Models;
using WorkMate.ViewModels;

namespace WorkMate.Controllers
{
    [Route("Jobs")]
    [Authorize]
    public class JobsController : BaseController
    {
        public JobsController(WorkMateDbContext dbWorkMate)
            : base(dbWorkMate)
        {
        }

        [Route]
        [HttpGet]
        public ViewResult Index()
        {
            string userID = User.Identity.GetUserId();

            JobsViewModel jobsVM = new JobsViewModel()
            {
                UserID = userID,
                Jobs = dbWorkMate.Jobs.Where(j => j.User.Id == userID).ToList()
            };
            
            return View(jobsVM);
        }

        [Route("Edit/{jobID}")]
        [HttpGet]
        public ActionResult Edit(int jobID)
        {
            Job job = dbWorkMate.Jobs.Find(jobID);

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

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                Job existingJob = dbWorkMate.Jobs.Find(job.ID);

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

                    dbWorkMate.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(job);
            }
        }

        [Route("New")]
        [HttpGet]
        public ViewResult New()
        {
            Job job = new Job();
            return View(job);
        }

        [Route("New")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Job job)
        {
            if (ModelState.IsValid)
            {
                job.UserID = User.Identity.GetUserId();

                dbWorkMate.Jobs.Add(job);
                dbWorkMate.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(job);
            }
        }
    }
}