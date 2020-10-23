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

        public List<EmpModel> GetEmpByName(String name,String Job)
        {
            return DataSource().Where(x => x.EmpFName.Contains(name) || x.Position.Contains(Job)).ToList();
        }

        private List<EmpModel> DataSource()
        {
            return new List<EmpModel>()
            { 
              new EmpModel(){ EmpId=1,EmpFName="Ishara",EmpLName="gyhbggh",Position="CEO",Email="ishara@gmail.com",Mobile=67},
               new EmpModel(){ EmpId=2,EmpFName="Hansi",EmpLName="vgvh",Position="MD",Email="hansi@gmail.com",Mobile=456},
                new EmpModel(){ EmpId=3,EmpFName="Maduh",EmpLName="bhj",Position="PM",Email="shbwh@gmail.com",Mobile=677},


            };
        }

        
    }
}
