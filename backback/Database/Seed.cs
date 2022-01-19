using Core;
using Core.Account.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backback.Database
{
    public class Seed
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ChatDbContext>();

                var roles = new List<string>();

                foreach (var item in Enum.GetValues(typeof(ENV.Roles)))
                    roles.Add(item.ToString());

                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        context.Roles.Add(new Role(role));
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
