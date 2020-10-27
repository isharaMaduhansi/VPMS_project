using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class EmpStoreContext :DbContext
    {
        public  EmpStoreContext(DbContextOptions<EmpStoreContext> options)
            :base(options)
        {

        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<JobTypes> JobTypes { get; set; }

    }
}
