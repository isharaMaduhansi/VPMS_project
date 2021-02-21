using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class Employees
    {
        [Key]
        public int EmpId { get; set; }
      
        public String EmpFName { get; set; }
        public String EmpLName { get; set; }

        public int JobTitleId { get; set; }

        public int PMId { get; set; }

        public String Email { get; set; }

        public int Mobile { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? WorkSince{ get; set; }
        public String Address { get; set; }

        public String ProfilePhoto { get; set; }

        public String Gender { get; set; }

   
        public String Status { get; set; }

        public DateTime? LastDayWorked { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? Todate { get; set; }
        public int MedicalAllocated { get; set; }
        public int CasualAllocated { get; set; }
        public int AnnualAllocated { get; set; }
        public int ShortLeaveAllocated { get; set; }
        public int HalfLeaveAllocated { get; set; }
        

        public Job Job { get; set; }

        public ICollection<LeaveApply> LeaveApply { get; set; }

        public ICollection<TimeTracker> TimeTracker { get; set; }

        public ICollection<Attendence> Attendence { get; set; }

        public ICollection<MarkAttendence> MarkAttendence { get; set; }

        public ICollection<Task> Task { get; set; }






    }
}
