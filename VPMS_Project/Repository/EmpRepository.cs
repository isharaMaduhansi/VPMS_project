using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public class EmpRepository
    {
        public List<EmpModel> GetAllEmps() 
        {
            return DataSource();
        }

        public EmpModel GetEmpById(int id)
        {
            return DataSource().Where(x => x.EmpId == id).FirstOrDefault();
        }

        private List<EmpModel> DataSource()
        {
            return new List<EmpModel>()
            { 
              new EmpModel(){ EmpId=1,EmpName="Ishara",Position="CEO"},
               new EmpModel(){ EmpId=2,EmpName="Hansi",Position="MD"},
                new EmpModel(){ EmpId=3,EmpName="Madu",Position="PM"},


            };
        }
    }
}
