using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Models
{
    public class LBalanceModel
    {
        public int EmpId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime Todate { get; set; }

        public int TotalLeaveGiven { get; set; }

        public int TotalLeaveTaken { get; set; }

        public int TotalLeaveBalance { get; set; }

        public int MedicalRemain { get; set; }
        public int MedicalTaken { get; set; }

        public int MedicalAllocated { get; set; }

        public int AnnualRemain { get; set; }
        public int AnnualTaken { get; set; }

        public int AnnualAllocated { get; set; }

        public int CasualRemain { get; set; }
        public int CasualTaken { get; set; }

        public int CasualAllocated { get; set; }

        public int ShortRemain { get; set; }
        public int ShortTaken { get; set; }

        public int ShortAllocated { get; set; }

        public int HalfRemain { get; set; }

        public int Halftaken{ get; set; }

        public int HalfAllocated { get; set; }

        public int SpecialTaken { get; set; }

        public int NoPayTaken { get; set; }

       
    }

}
