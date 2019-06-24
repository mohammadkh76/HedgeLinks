using HedgeLinks.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context,
           IFunctional functional)
        {
            context.Database.EnsureCreated();

            //check for users
            if (context.ApplicationUser.Any())
            {
                return; //if user is not empty, DB has been seed
            }
       
            await functional.CreateDefaultSuperAdmin();

            //init app data
            await functional.InitAppData();

        }
    }
}
