using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class Task
    {
        
        public int Id { get; set; }

        public String Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double AllocatedHours { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdate { get; set; }

        public String Description { get; set; }

        public Boolean? TaskComplete { get; set; }

        public Boolean? TimeSheet { get; set; }

        public int EmployeesId { get; set; }

        public Employees Employees { get; set; }
    }
}
