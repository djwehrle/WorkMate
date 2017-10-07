using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.AspNet.Identity;

using WorkMate.Models;

namespace WorkMate.Controllers.API
{
    public class ScheduleController : ApiController
    {
        // DELETE: api/schedule/{id}
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Schedule schedule = db.Schedules.Find(id);

                if (schedule == null)
                {
                    return NotFound();
                }
                else if (schedule.UserID != User.Identity.GetUserId())
                {
                    return Unauthorized();
                }
                else
                {
                    db.Schedules.Remove(schedule);
                    db.SaveChanges();

                    return Ok();
                }
            }
            catch
            {
                return InternalServerError();
            }
        }

        private WorkMateDbContext db = new WorkMateDbContext();
    }
}