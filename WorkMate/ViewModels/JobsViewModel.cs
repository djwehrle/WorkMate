using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WorkMate.Models;

namespace WorkMate.ViewModels
{
    public class JobsViewModel
    {
        public JobsViewModel()
        {
            Jobs = new List<Job>();
        }

        public string UserID { get; set; }

        public List<Job> Jobs { get; set; }
    }
}