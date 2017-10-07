using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using WorkMate.Models;

namespace WorkMate.ViewModels
{
    public class PaycheckViewModel
    {
        public Paycheck Paycheck { get; set; }

        [Display(Name = "Pay Date")]
        public string PayDate
        {
            get
            {
                return Paycheck.PayDate.Date.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "Start Date")]
        public string StartDate
        {
            get
            {
                return Paycheck.PayDate.StartDate.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "End Date")]
        public string EndDate
        {
            get
            {
                return Paycheck.PayDate.EndDate.ToString("yyyy-MM-dd");
            }
        }
    }
}