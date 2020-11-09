using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class JobModel
    {
        public int JobId { get; set; }

        [Required(ErrorMessage = "Job Title name field is required")]
        public string JobName { get; set; }

      
        public string Description { get; set; }

        [Required(ErrorMessage = "Casual Leave field is required")]
        public int? Casual { get; set; }

        [Required(ErrorMessage = "Medical Leave field is required")]
        public int? Medical { get; set; }

        [Required(ErrorMessage = "Annual Leave field is required")]
        public int? Annual { get; set; }

        [Required(ErrorMessage = "Short Leave field is required")]
        public int? ShortLeaves { get; set; }

        [Required(ErrorMessage = "HalfDay Leave field is required")]
        public int? HalfDays { get; set; }

        public int? TotalLeaves { get; set; }
    }
}
