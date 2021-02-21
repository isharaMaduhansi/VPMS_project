using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class TaskModel
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

        public DateTime TaskCompletedOn { get; set; }

        public Boolean? TimeSheet { get; set; }

        public int EmpId { get; set; }

    }
}
