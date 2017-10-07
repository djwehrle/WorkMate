using System;
using System.Web.Mvc;
using WorkMate.Models;

namespace WorkMate.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController(WorkMateDbContext dbWorkMate)
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