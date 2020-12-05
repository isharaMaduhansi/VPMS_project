using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class RemainLeaveModel
    {
        public int EmpId { get; set; }
        public int MedicalRemain { get; set; }

        public int AnnualRemain { get; set; }

        public int CasualRemain { get; set; }
   
        public int ShortRemain { get; set; }
       
        public int HalfRemain { get; set; }
    }
}
