using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class LeaveApply
    {
        [Key]
        public int LeaveApplyId { get; set; }

        public String LeaveType { get; set; }
        public String Reason { get; set; }

        public DateTime? Startdate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? AppliedDate { get; set; }

        public int NoOfDays { get; set; }

        public int EmpId { get; set; }

        public Employees Employees { get; set; }

      
    }
}
