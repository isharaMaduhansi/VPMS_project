using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class AttendenceModel
    {

        public int AttendenceId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date field is required")]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "In-Time field is required")]
        public DateTime InTime { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Out-Time field is required")]
        public DateTime OutTime { get; set; }


        public double TotalHours { get; set; }

        public double BreakingHours { get; set; }

        [Required(ErrorMessage = "Break hours field is required")]
        public double BHours { get; set; }

        [Required(ErrorMessage = "Break minutes field is required")]
        public double BMinutes { get; set; }

        public double WorkingHours { get; set; }

        [Required(ErrorMessage = "Explanation field is required")]
        public String Explanation { get; set; }

        public int EmpId { get; set; }


        public String Status { get; set; }

        public DateTime AppliedDate { get; set; }

        public String Approver { get; set; }

        public String EmpName { get; set; }

        public String Designation { get; set; }

        public String Type { get; set; }

    }
}
