﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class Employees
    {
        [Key]
        public int EmpId { get; set; }
      

        public String EmpFName { get; set; }
        public String EmpLName { get; set; }

        public int JobId { get; set; }

        public String Email { get; set; }

        public int Mobile { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? WorkSince{ get; set; }
        public String Address { get; set; }

        public JobTypes JobTypes { get; set; }


    }
}
