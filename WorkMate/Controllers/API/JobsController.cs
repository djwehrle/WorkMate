using System.Web.Http;
using Microsoft.AspNet.Identity;
using WorkMate.Models;

namespace WorkMate.Controllers.API
{
    [RoutePrefix("api/Jobs")]
    public class JobsController : BaseAPIController
    {
        public JobsController(WorkMateDbContext dbWorkMate)
            : base(dbWorkMate)
        {
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteJob(int id)
        {
            try
            {
                Job job = dbWorkMate.Jobs.Find(id);

                if (job == null)
                {
                    return NotFound();
                }
                else if (job.UserID != User.Identity.GetUserId())
                {
                    return Unauthorized();
                }
                else
                {
                    dbWorkMate.Jobs.Remove(job);
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