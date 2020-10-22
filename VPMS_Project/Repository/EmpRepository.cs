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
              new EmpModel(){ EmpId=1,EmpName="Ishara",Position="CEO",Email="ishara@gmail.com",Mobile=67},
               new EmpModel(){ EmpId=2,EmpName="Hansi",Position="MD",Email="hansi@gmail.com",Mobile=456},
                new EmpModel(){ EmpId=3,EmpName="Maduh",Position="PM",Email="shbwh@gmail.com",Mobile=677},


            };
        }
    }
}
