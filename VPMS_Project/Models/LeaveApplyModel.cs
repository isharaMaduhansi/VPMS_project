﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class LeaveApplyModel
    {
        public int LeaveApplyId { get; set; }

        [Required(ErrorMessage = "Leave type field is required")]
        public String LeaveType { get; set; }

        [Required(ErrorMessage = "Reason field is required")]
        public String Reason { get; set; }

      
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start date field is required")]
        public DateTime? Startdate { get; set; }

       
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End date field is required")]
         public DateTime? EndDate { get; set; }

        public int EmpId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
