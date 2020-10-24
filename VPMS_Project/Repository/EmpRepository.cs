using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Data;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public class EmpRepository
    {
        private readonly EmpStoreContext _context = null;

        public EmpRepository(EmpStoreContext context) 
        {
            _context = context;
        }


        public async Task<List<EmpModel>> GetAllEmps() 
        {
            var employees = new List<EmpModel>();
            var AllEmp = await _context.Employees.ToListAsync();
            if (AllEmp?.Any()==true)
            {
                foreach (var emp in AllEmp)
                {
                    employees.Add(new EmpModel() 
                    {
                     EmpId=emp.EmpId,
                     EmpFName=emp.EmpFName,
                     EmpLName=emp.EmpLName,
                     Position=emp.Position,
                     Email=emp.Email,
                     Mobile=emp.Mobile,
                     Dob=emp.Dob
                    
                    });
                
                }
            }

            return employees;
        }

        public async Task<EmpModel> GetEmpById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee!=null)
            {
                var emp = new EmpModel() {
                    EmpId = employee.EmpId,
                    EmpFName = employee.EmpFName,
                    EmpLName = employee.EmpLName,
                    Position = employee.Position,
                    Email = employee.Email,
                    Mobile = employee.Mobile,
                    Dob = employee.Dob

           };

                return emp;
            
            }
            return null;
        }

        public List<EmpModel> GetEmpByName(String name,String Job)
        {
            return DataSource().Where(x => x.EmpFName.Contains(name) || x.Position.Contains(Job)).ToList();
        }

        public async Task<int> AddEmp(EmpModel empModel)
        {
            var newEmp = new Employees() {

                EmpFName=empModel.EmpFName,
                EmpLName=empModel.EmpLName,
                Position=empModel.Position,
                Email=empModel.Email,
                Mobile=empModel.Mobile,
             };

           await _context.Employees.AddAsync(newEmp);
           await _context.SaveChangesAsync();

            return newEmp.EmpId;


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
