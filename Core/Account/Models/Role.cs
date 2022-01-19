using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Account.Models
{
    public class Role : IdentityRole
    {
        public Role(string role) : base(role)
        {
            NormalizedName = role.ToUpper();
        }
    }
}
