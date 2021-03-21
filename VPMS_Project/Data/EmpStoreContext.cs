using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class EmpStoreContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public EmpStoreContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Employees> Employees { get; set; }

        public DbSet<Job> Job { get; set; }
        public DbSet<LeaveApply> LeaveApply { get; set; }

        public DbSet<TimeTracker> TimeTracker { get; set; }

        public DbSet<Attendence> Attendence { get; set; }

        public DbSet<MarkAttendence> MarkAttendence { get; set; }

        public DbSet<StandardWorkHours> StandardWorkHours { get; set; }

        public DbSet<Task> Task { get; set; }

        public DbSet<TimeSheetTask> TimeSheetTask { get; set; }

    }
}
