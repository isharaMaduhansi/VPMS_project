using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class MarkAttendence
    {
        [Key]
        public int MarkAttendenceId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public double TotalHours { get; set; }

        public int EmpId { get; set; }

        public String Status { get; set; }

        public String Type { get; set; }

        public Employees Employees { get; set; }
    }
}
