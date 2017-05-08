using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TestASP.NetIdentity.DataAccess;
using TestASP.NetIdentity.DomainEntities;

namespace TestASP.NetIdentity.Biz
{
    public class Biz
    {
        public void GetAll()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>();
            options.UseSqlServer("Data Source=.;Initial Catalog=TextIdentityDb;AttachDbFilename=E:\\Projects\\TestASP.NetIdentity\\TestASP.NetIdentity.UI\\App_Data\\TextIdentityDb.mdf;User Id=sa;Password=1;Trusted_Connection=True;MultipleActiveResultSets=true");
            var _context = new MainDbContext(options.Options);
            
        }
    }
}
