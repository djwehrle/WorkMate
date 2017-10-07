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
    public class JobsController : ApiController
    {
        // DELETE: api/jobs/{id}
        [HttpDelete]
        public IHttpActionResult DeleteJob(int id)
        {
            try
            {
                Job job = db.Jobs.Find(id);

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
                    db.Jobs.Remove(job);
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