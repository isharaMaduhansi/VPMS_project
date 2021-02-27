using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class TimeSheetTaskModel
    {
        public int Id { get; set; }

        public DateTime AppliedDate { get; set; }

        public String Name { get; set; }

        public String EmpName { get; set; }

        public double AllocatedHours { get; set; }

        public DateTime ActualStartDateTime { get; set; }

        public DateTime ActualEndDateTime { get; set; }

        public double TotalHours { get; set; }

        public int EmployeesId { get; set; }

        public int TaskId { get; set; }
    }
}
