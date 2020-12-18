using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class TimeTracker
    {
        [Key]
        public int TrackId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public double TotalHours { get; set; }

        public DateTime? BreakStart { get; set; }
        

        public DateTime? BreakEnd { get; set; }

        public double BreakingHours { get; set; }

        public double WorkingHours { get; set; }

        public int EmpId { get; set; }

        public Employees Employees { get; set; }

    }
}
