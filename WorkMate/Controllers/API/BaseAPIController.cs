using System;
using System.Web.Http;
using WorkMate.Models;

namespace WorkMate.Controllers.API
{
    public abstract class BaseAPIController : ApiController
    {
        protected BaseAPIController(WorkMateDbContext dbWorkMate)
        {
            if (dbWorkMate == null)
            {
                throw new ArgumentNullException("dbWorkMate");
            }

            this.dbWorkMate = dbWorkMate;
        }

        protected WorkMateDbContext dbWorkMate;
    }
}