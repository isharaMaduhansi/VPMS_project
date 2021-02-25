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
          return await (from a in _context.Employees.Where(x => (x.Status == "Active"))
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
                          .ToListAsync();
        }

        public async Task<List<EmpModel>> GetEmps()
        {
            return await (from a in _context.Employees.Where(x => (x.Status == "Active"))
                          join b in _context.Job on a.JobTitleId equals b.JobId
                          select new EmpModel()
                          {
                              EmpId = a.EmpId,
                              EmpFullName= a.EmpFName+" "+ a.EmpLName
                          })
                           .ToListAsync();
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
               return await (from a in _context.Employees.Where(x => x.EmpId == id)
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
                              Status = a.Status,
                              FromDate= (DateTime)a.FromDate,
                              Todate= (DateTime)a.Todate,
                              CasualAllocated=a.CasualAllocated,
                              MedicalAllocated=a.MedicalAllocated,
                              AnnualAllocated=a.AnnualAllocated,
                              HalfLeaveAllocated=a.HalfLeaveAllocated,
                              ShortLeaveAllocated=a.ShortLeaveAllocated,
                              TotalLeaveGiven=a.ShortLeaveAllocated+a.HalfLeaveAllocated+a.AnnualAllocated+a.MedicalAllocated+a.CasualAllocated
                              
                              

                          })
                     .FirstOrDefaultAsync();

        }


        public async Task<int> AddEmp(EmpModel empModel)
        {
            var job = await _context.Job.FindAsync(empModel.JobTitleId);
            var newEmp = new Employees()
            {

                EmpFName = empModel.EmpFName,
                EmpLName = empModel.EmpLName,
                Email = empModel.Email,
                JobTitleId = empModel.JobTitleId,
                Address = empModel.Address,
                PMId=0,
                Dob = empModel.Dob,
                WorkSince = empModel.WorkSince,
                Mobile = empModel.Mobile.HasValue ? empModel.Mobile.Value : 0,
                ProfilePhoto = empModel.PhotoURL,
                Gender = empModel.Gender,
                Status = empModel.Status,
                CasualAllocated = job.Casual,
                AnnualAllocated = job.Annual,
                MedicalAllocated = job.Medical,
                ShortLeaveAllocated = job.ShortLeaves,
                HalfLeaveAllocated = job.HalfDays,
                FromDate = empModel.WorkSince.AddMonths(1),
                Todate=empModel.WorkSince.AddMonths(1).AddYears(1)
                  
            };

            await _context.Employees.AddAsync(newEmp);
            await _context.SaveChangesAsync();

            return newEmp.EmpId;


        }

        public async Task<bool> UpdateEmp(EmpModel empModel)
        {
            var job = await _context.Job.FindAsync(empModel.JobTitleId);
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
            emp.CasualAllocated = job.Casual;
            emp.AnnualAllocated = job.Annual;
            emp.MedicalAllocated = job.Medical;
            emp.ShortLeaveAllocated = job.ShortLeaves;
            emp.HalfLeaveAllocated = job.HalfDays;
            emp.FromDate = empModel.WorkSince.AddMonths(1);
            emp.Todate = empModel.WorkSince.AddMonths(1).AddYears(1);

            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
         
        }

        public async Task<bool> UpdateEmpLeave(EmpModel empModel)
        {
            
            var emp = await _context.Employees.FindAsync(empModel.EmpId);
            emp.CasualAllocated = empModel.CasualAllocated;
            emp.AnnualAllocated = empModel.AnnualAllocated;
            emp.MedicalAllocated = empModel.MedicalAllocated;
            emp.ShortLeaveAllocated = empModel.ShortLeaveAllocated;
            emp.HalfLeaveAllocated = empModel.HalfLeaveAllocated;
            emp.FromDate = empModel.FromDate;
            emp.Todate = empModel.Todate;

            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteEmp(int id)
        {

            var emp = await _context.Employees.FindAsync(id);
            emp.LastDayWorked = DateTime.Now;
            emp.Status = "Inactive";

            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<EmpModel> GetEmp(int id)
        {
            return await (from a in _context.Employees.Where(x => x.EmpId == id)
                          join b in _context.Job on a.JobTitleId equals b.JobId
                          select new EmpModel()
                          {
                              EmpId = a.EmpId,
                              EmpFName = a.EmpFName,
                              EmpLName = a.EmpLName,
                              JobType = b.JobName,
                            
                          })
                  .FirstOrDefaultAsync();

        }

        





    }
}
