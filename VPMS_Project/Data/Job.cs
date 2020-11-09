using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        public String JobName { get; set; }
        public String Description { get; set; }

        public int Casual { get; set; }
        public int Medical { get; set; }
        public int Annual { get; set; }
        public int ShortLeaves { get; set; }
        public int HalfDays { get; set; }
        public ICollection<Employees> Employees { get; set; }


    }
}
