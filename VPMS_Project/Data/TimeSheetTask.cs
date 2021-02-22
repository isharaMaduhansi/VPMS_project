using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class TimeSheetTask
    {
        [key]
        public int Id { get; set; }

        public DateTime AppliedDate { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public double TotalHours { get; set; }

        public int EmployeesId { get; set; }

        public int TaskId { get; set; }

        public Employees Employees { get; set; }

        public Task Task { get; set; }
    }
}
