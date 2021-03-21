using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        public String Name { get; set; }
      
        public DateTime AppliedDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Start Time field is required")]
        public DateTime ActualStartDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "End Time field is required")]
        public DateTime ActualEndDateTime { get; set; }

        public double AllocatedHours { get; set; }

        public double TakenHours { get; set; }

        public double BreakHours { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdate { get; set; }

        public String Description { get; set; }

        public Boolean? TaskComplete { get; set; }

        public DateTime TaskCompletedOn { get; set; }

        public Boolean? TimeSheet { get; set; }

        public int EmpId { get; set; }

    }
}
