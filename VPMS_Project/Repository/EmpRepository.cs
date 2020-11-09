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
          return await (from a in _context.Employees
                          join b in _context.Job on a.JobTitleId equals b.JobId
                          select new EmpModel()
                          {
                              EmpId = a.EmpId,
                              EmpFName = a.EmpFName,
                              EmpLName = a.EmpLName,
                              JobTitleId = a.JobTitleId,
                              JobType = b.JobName,
                              Email = a.Email,
                              Mobile = a.Mobile,
                              Dob = (DateTime)a.Dob,
                              Address = a.Address,
                              PhotoURL = a.ProfilePhoto,
                              Gender = a.Gender,
                              Status = a.Status
                           })
                          .Where(a => a.Status == "Active").ToListAsync();
        }
        

        public async Task<List<EmpModel>> GetSearchEmps(String name)
        {
            return await (from a in _context.Employees
                          join b in _context.Job on a.JobTitleId equals b.JobId
                          select new EmpModel()
                          {
                              EmpId = a.EmpId,
                              EmpFName = a.EmpFName,
                              EmpLName = a.EmpLName,
                              JobTitleId = a.JobTitleId,
                              JobType = b.JobName,
                              Email = a.Email,
                              Mobile = a.Mobile,
                              Dob = (DateTime)a.Dob,
                              Address = a.Address,
                              PhotoURL = a.ProfilePhoto,
                              Gender = a.Gender,
                              Status = a.Status
                          })
                        .Where(x => (x.Status == "Active") && (x.EmpFName.Contains(name) || x.JobType.Contains(name) || x.EmpLName.Contains(name))).ToListAsync();
        }


        public async Task<List<EmpModel>> GetDeletedEmps()
        {
            return await (from a in _context.Employees
                          join b in _context.Job on a.JobTitleId equals b.JobId
                          select new EmpModel()
                          {
                              EmpId = a.EmpId,
                              EmpFName = a.EmpFName,
                              EmpLName = a.EmpLName,
                              JobTitleId = a.JobTitleId,
                              JobType = b.JobName,
                              Email = a.Email,
                              Mobile = a.Mobile,
                              Dob = (DateTime)a.Dob,
                              Address = a.Address,
                              PhotoURL = a.ProfilePhoto,
                              Gender = a.Gender,
                              Status = a.Status
                          })
                       .Where(x => x.Status == "Inactive").ToListAsync();


        }


        public async Task<List<EmpModel>> GetEmpListAsync(string name, string job,int id)
        {
            return await (from a in _context.Employees
                          join b in _context.Job on a.JobTitleId equals b.JobId
                          select new EmpModel()
                          {
                              EmpId = a.EmpId,
                              EmpFName = a.EmpFName,
                              EmpLName = a.EmpLName,
                              JobTitleId = a.JobTitleId,
                              JobType = b.JobName,
                              Email = a.Email,
                              Mobile = a.Mobile,
                              Dob = (DateTime)a.Dob,
                              Address = a.Address,
                              PhotoURL = a.ProfilePhoto,
                              Gender = a.Gender,
                              Status = a.Status
                          })
                      .Where(x => (x.Status == "Active") && (x.EmpId != id) && (x.EmpFName.Contains(name) || x.JobType.Contains(job))).ToListAsync();
        }


        public async Task<EmpModel> GetEmpById(int id)
        {
               return await (from a in _context.Employees
                          join b in _context.Job on a.JobTitleId equals b.JobId
                          select new EmpModel()
                          {
                              EmpId = a.EmpId,
                              EmpFName = a.EmpFName,
                              EmpLName = a.EmpLName,
                              JobTitleId = a.JobTitleId,
                              JobType = b.JobName,
                              Email = a.Email,
                              Mobile = a.Mobile,
                              Dob = (DateTime)a.Dob,
                              WorkSince = (DateTime)a.WorkSince,
                              LastDayWorked=(DateTime)a.LastDayWorked,
                              Address = a.Address,
                              PhotoURL = a.ProfilePhoto,
                              Gender = a.Gender,
                              Status = a.Status
                          })
                     .Where(x => x.EmpId == id).FirstOrDefaultAsync();

        }



        public async Task<int> AddEmp(EmpModel empModel)
        {
            var newEmp = new Employees()
            {

                EmpFName = empModel.EmpFName,
                EmpLName = empModel.EmpLName,
                Email = empModel.Email,
                JobTitleId = empModel.JobTitleId,
                Address = empModel.Address,
                Dob = empModel.Dob,
                WorkSince = empModel.WorkSince,
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
            emp.JobTitleId = empModel.JobTitleId;
            emp.WorkSince = empModel.WorkSince;
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
