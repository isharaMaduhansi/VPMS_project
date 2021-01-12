using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class StandardWorkHours
    {
        [Key]
        public int HourId { get; set; }

        public int NoOfHours { get; set; }
    }
}
