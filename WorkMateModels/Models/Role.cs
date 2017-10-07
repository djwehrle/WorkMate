using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;

namespace WorkMate.Models.Models
{
    public class Role : IdentityRole
    {
        public Role()
            : base()
        {
        }

        public Role(string name)
            : base(name)
        {
        }
    }
}