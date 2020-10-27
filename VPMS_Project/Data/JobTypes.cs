using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Data
{
    public class JobTypes
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public ICollection<Employees> Employees { get; set; }
    }
}
