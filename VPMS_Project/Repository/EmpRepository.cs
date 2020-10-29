using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Data;
using VPMS_Project.Enums;
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
                     JobTitle=emp.JobTitle,
                     Email=emp.Email,
                     Mobile=emp.Mobile,
                     Dob=(DateTime)emp.Dob,
                     Address=emp.Address,
                     PhotoURL=emp.ProfilePhoto,
                     Gender=emp.Gender
                     
                    
                    });
                
                }
            }

            return employees;
        }
        
        public async Task<EmpModel> GetEmpById(int id)
        {

            return await _context.Employees.Where(x => x.EmpId == id).Select(employee => new EmpModel()
            {
                EmpId = employee.EmpId,
                EmpFName = employee.EmpFName,
                EmpLName = employee.EmpLName,
                JobTitle=employee.JobTitle,
                Email = employee.Email,
                Mobile = employee.Mobile,
                Address = employee.Address,
                Dob = (DateTime)employee.Dob,
                WorkSince = (DateTime)employee.WorkSince,
                PhotoURL = employee.ProfilePhoto,
                Gender=employee.Gender
            }).FirstOrDefaultAsync();

          }

        //public List<EmpModel> GetEmpByName(String name,String Job)
        //{
        //    return DataSource().Where(x => x.EmpFName.Contains(name) || x.Position.Contains(Job)).ToList();
        //}

        public async Task<int> AddEmp(EmpModel empModel)
        {
            var newEmp = new Employees() {

                EmpFName = empModel.EmpFName,
                EmpLName = empModel.EmpLName,
                Email = empModel.Email,
                JobTitle = empModel.JobTitle,
                Address = empModel.Address,
                Dob = empModel.Dob,
                WorkSince = DateTime.UtcNow,
                Mobile = empModel.Mobile.HasValue ? empModel.Mobile.Value : 0,
                ProfilePhoto = empModel.PhotoURL,
                Gender=empModel.Gender
                
             };

           await _context.Employees.AddAsync(newEmp);
           await _context.SaveChangesAsync();

            return newEmp.EmpId;


        }


       

        
    }
}
