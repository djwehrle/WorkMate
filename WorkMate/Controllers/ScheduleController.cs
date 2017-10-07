using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorkMate.Models;
using WorkMate.ViewModels;

namespace WorkMate.Controllers
{
    [RoutePrefix("Schedule")]
    [Authorize]
    public class ScheduleController : BaseController
    {
        public ScheduleController(WorkMateDbContext dbWorkMate)
            : base(dbWorkMate)
        {
        }

        [Route]
        [HttpGet]
        public ViewResult Index()
        {
            // Create a list of days for current week
            DateTime now = DateTime.Now;
            DateTime endOfWeek = now.Date.AddDays(7);
            string userID = User.Identity.GetUserId();

            List<Schedule> schedules = dbWorkMate.Schedules
                .Include(s => s.Job)
                .Where(s =>
                    s.UserID == userID
                    && now <= s.EndTime
                    && endOfWeek >= s.StartTime
                )
                .ToList();

            Dictionary<DateTime, List<Schedule>> currentWeekSchedules = new Dictionary<DateTime, List<Schedule>>();

            // Create a dictionary of days of the week and add schedules that belong to each day
            for (int i = 0; i < 7; i++)
            {
                DateTime day = now.Date.AddDays(i);

                // TODO: Need to change this to support overnight work
                List<Schedule> dailySchedules = schedules.Where(s => s.StartTime.Date == day).OrderBy(s => s.StartTime).ToList();

                currentWeekSchedules.Add(day, dailySchedules);
            }

            return View(currentWeekSchedules);
        }

        [Route("Edit/{scheduleID}")]
        [HttpGet]
        public ActionResult Edit(int scheduleID)
        {
            Schedule schedule = dbWorkMate.Schedules.Find(scheduleID);

            if (schedule == null)
            {
                return HttpNotFound();
            }
            else
            {
                string userID = User.Identity.GetUserId();

                if (userID != schedule.UserID)
                {
                    return new HttpUnauthorizedResult();
                }
                else
                {
                    ScheduleFormViewModel scheduleFormVM = new ScheduleFormViewModel()
                    {
                        Schedule = schedule,
                        Date = schedule.StartTime.ToString("yyyy-MM-dd"),
                        StartTime = schedule.StartTime.ToString("HH:mm"),
                        EndTime = schedule.EndTime.ToString("HH:mm"),
                        Jobs = dbWorkMate.Jobs.Where(j => j.UserID == userID).ToList()
                    };

                    return View(scheduleFormVM);
                }
            }
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScheduleFormViewModel scheduleFormVM)
        {
            if (ModelState.IsValid)
            {
                Schedule existingSchedule = dbWorkMate.Schedules.Find(scheduleFormVM.Schedule.ID);

                if (existingSchedule == null)
                {
                    return HttpNotFound();
                }
                else if (existingSchedule.UserID != User.Identity.GetUserId())
                {
                    return new HttpUnauthorizedResult();
                }
                else
                {
                    existingSchedule.JobID = scheduleFormVM.Schedule.JobID;
                    existingSchedule.StartTime = Convert.ToDateTime(scheduleFormVM.Date + " " + scheduleFormVM.StartTime);
                    existingSchedule.EndTime = Convert.ToDateTime(scheduleFormVM.Date + " " + scheduleFormVM.EndTime);

                    dbWorkMate.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(scheduleFormVM);
            }
        }

        [Route("New")]
        [HttpGet]
        public ViewResult New()
        {
            string userID = User.Identity.GetUserId();

            ScheduleFormViewModel scheduleFormVM = new ScheduleFormViewModel()
            {
                Jobs = dbWorkMate.Jobs.Where(j => j.UserID == userID).ToList()
            };
            
            return View(scheduleFormVM);
        }

        [Route("New")]
        [HttpPost]
        public ActionResult New(ScheduleFormViewModel scheduleFormVM)
        {
            if (ModelState.IsValid)
            {
                Schedule schedule = scheduleFormVM.Schedule;

                schedule.UserID = User.Identity.GetUserId();
                schedule.StartTime = Convert.ToDateTime(scheduleFormVM.Date + " " + scheduleFormVM.StartTime);
                schedule.EndTime = Convert.ToDateTime(scheduleFormVM.Date + " " + scheduleFormVM.EndTime);

                dbWorkMate.Schedules.Add(schedule);
                dbWorkMate.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(scheduleFormVM);
            }
        }
    }
}