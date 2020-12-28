using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// Attendence Request

namespace VPMS_Project.Data
{
    public class Attendence
    {
        [Key]
        public int AttendenceId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public double TotalHours { get; set; }

        public double BreakingHours { get; set; }

        public double WorkingHours { get; set; }

        public String Explanation { get; set; }

        public int EmpId { get; set; }

        public String Status { get; set; }

        public String Type { get; set; }

        public DateTime? AppliedDate { get; set; }

        public String Approver { get; set; }

        public Employees Employees { get; set; }


    }
}

