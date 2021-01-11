using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class KeyModel
    {
        public int EmpId { get; set; }

        public String EmpFullName { get; set; }

        public String EmpFName { get; set; }

        public int LeaveApplyId { get; set; }

        public String PhotoURL { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime SearchDate { get; set; }
    }
}
