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
    public class EmpRepository : IEmpRepository
    {
        private readonly EmpStoreContext _context = null;

        public EmpRepository(EmpStoreContext context)
        {
            _context = context;
        }


        public async Task<List<EmpModel>> GetActiveEmps()
        {
            return await _context.Employees.Where(x => x.Status=="Active").Select(emp => new EmpModel()
            {

                EmpId = emp.EmpId,
                EmpFName = emp.EmpFName,
                EmpLName = emp.EmpLName,
                JobTitle = emp.JobTitle,
                Email = emp.Email,
                Mobile = emp.Mobile,
                Dob = (DateTime)emp.Dob,
                Address = emp.Address,
                PhotoURL = emp.ProfilePhoto,
                Gender = emp.Gender,
                Status=emp.Status
               

            }).ToListAsync();
        }
        

        public async Task<List<EmpModel>> GetSearchEmps(String name)
        {
            return await _context.Employees.Where(x => x.EmpFName.Contains(name) || x.JobTitle.Contains(name) || x.EmpLName.Contains(name))
                .Select(emp => new EmpModel()
            {

                EmpId = emp.EmpId,
                EmpFName = emp.EmpFName,
                EmpLName = emp.EmpLName,
                JobTitle = emp.JobTitle,
                Email = emp.Email,
                Mobile = emp.Mobile,
                Dob = (DateTime)emp.Dob,
                Address = emp.Address,
                PhotoURL = emp.ProfilePhoto,
                Gender = emp.Gender,
                Status = emp.Status


            }).ToListAsync();
        }


        public async Task<List<EmpModel>> GetDeletedEmps()
        {
            return await _context.Employees.Where(x => x.Status == "Inactive").Select(emp => new EmpModel()
            {

                EmpId = emp.EmpId,
                EmpFName = emp.EmpFName,
                EmpLName = emp.EmpLName,
                JobTitle = emp.JobTitle,
                Email = emp.Email,
                Mobile = emp.Mobile,
                Dob = (DateTime)emp.Dob,
                Address = emp.Address,
                PhotoURL = emp.ProfilePhoto,
                Gender = emp.Gender,
                Status = emp.Status


            }).ToListAsync();
        }


        public async Task<List<EmpModel>> GetEmpListAsync(string name, string job,int id)
        {
            return await _context.Employees.Where(x =>(x.Status=="Active") && (x.EmpId != id) && (x.EmpFName.Contains(name) || x.JobTitle.Contains(job)))
                .Select(emp => new EmpModel()
                {

                    EmpId = emp.EmpId,
                    EmpFName = emp.EmpFName,
                    EmpLName = emp.EmpLName,
                    JobTitle = emp.JobTitle,
                    Email = emp.Email,
                    Mobile = emp.Mobile,
                    Dob = (DateTime)emp.Dob,
                    Address = emp.Address,
                    PhotoURL = emp.ProfilePhoto,
                    Gender = emp.Gender,
                    Status=emp.Status

                }).ToListAsync();
        }


        public async Task<EmpModel> GetEmpById(int id)
        {

            return await _context.Employees.Where(x => x.EmpId == id).Select(employee => new EmpModel()
            {
                EmpId = employee.EmpId,
                EmpFName = employee.EmpFName,
                EmpLName = employee.EmpLName,
                JobTitle = employee.JobTitle,
                Email = employee.Email,
                Mobile = employee.Mobile,
                Address = employee.Address,
                Dob = (DateTime)employee.Dob,
                WorkSince = (DateTime)employee.WorkSince,
                LastDayWorked=(DateTime)employee.LastDayWorked,
                PhotoURL = employee.ProfilePhoto,
                Gender = employee.Gender,
                Status=employee.Status
            }).FirstOrDefaultAsync();

        }

        

        public async Task<int> AddEmp(EmpModel empModel)
        {
            var newEmp = new Employees()
            {

                EmpFName = empModel.EmpFName,
                EmpLName = empModel.EmpLName,
                Email = empModel.Email,
                JobTitle = empModel.JobTitle,
                Address = empModel.Address,
                Dob = empModel.Dob,
                WorkSince = DateTime.UtcNow,
                Mobile = empModel.Mobile.HasValue ? empModel.Mobile.Value : 0,
                ProfilePhoto = empModel.PhotoURL,
                Gender = empModel.Gender,
                Status=empModel.Status

            };

            await _context.Employees.AddAsync(newEmp);
            await _context.SaveChangesAsync();

            return newEmp.EmpId;


        }

        public async Task<bool> UpdateEmp(EmpModel empModel)
        {
            
            var emp =await _context.Employees.FindAsync(empModel.EmpId);
            emp.EmpFName = empModel.EmpFName;
            emp.EmpLName = empModel.EmpLName;
            emp.Email=empModel.Email;
            emp.JobTitle = empModel.JobTitle;
            emp.Gender = empModel.Gender;
            emp.Mobile = empModel.Mobile.HasValue ? empModel.Mobile.Value : 0;
            emp.Dob = empModel.Dob;
            emp.Address = empModel.Address;
            emp.ProfilePhoto = empModel.PhotoURL;
            emp.Status = empModel.Status;

            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
         
        }

        public async Task<bool> DeleteEmp(int id)
        {

            var emp = await _context.Employees.FindAsync(id);
            emp.LastDayWorked = DateTime.UtcNow;
            emp.Status = "Inactive";

            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }




    }
}
