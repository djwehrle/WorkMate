using System.Web.Http;
using Microsoft.AspNet.Identity;
using WorkMate.Models;

namespace WorkMate.Controllers.API
{
    [RoutePrefix("api/Schedule")]
    public class ScheduleController : BaseAPIController
    {
        public ScheduleController(WorkMateDbContext dbWorkMate)
            : base(dbWorkMate)
        {
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Schedule schedule = dbWorkMate.Schedules.Find(id);

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
                    dbWorkMate.Schedules.Remove(schedule);
                    dbWorkMate.SaveChanges();

                    return Ok();
                }
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}