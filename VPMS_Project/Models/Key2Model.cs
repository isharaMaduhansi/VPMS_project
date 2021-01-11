using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class Key2Model
    {
        public int EmpId { get; set; }

        public String EmpFullName { get; set; }

        public String EmpFName { get; set; }

        public int AttendenceId { get; set; }

        public String PhotoURL { get; set; }

        public String Status { get; set; }

        public DateTime RelavantDate { get; set; }

        public DateTime SearchDate { get; set; }
    }
}

