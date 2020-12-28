using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Data;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public class AttendenceRepo
    {
        private readonly EmpStoreContext _context = null;

        public AttendenceRepo(EmpStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddRequest(AttendenceModel attendenceModel)
        {
            var attendence = new Attendence()
            {
              Date= attendenceModel.Date,
              InTime=attendenceModel.InTime,
              OutTime=attendenceModel.OutTime,
              TotalHours=attendenceModel.TotalHours,
              BreakingHours=attendenceModel.BreakingHours,
              WorkingHours=attendenceModel.WorkingHours,
              Explanation=attendenceModel.Explanation,
              EmpId=attendenceModel.EmpId,
              Type="Manual",
              AppliedDate=DateTime.UtcNow,
              Status="Pending"
              
            };

            await _context.Attendence.AddAsync(attendence);
            await _context.SaveChangesAsync();

            return attendence.AttendenceId;
        }

        public async Task<List<AttendenceModel>> GetRequest(int id)
        {
            return await (from a in _context.Attendence.Where(x => x.EmpId == id)
                          select new AttendenceModel()
                          {
                              Date = (DateTime)a.Date,
                              InTime= (DateTime)a.InTime,
                              OutTime= (DateTime)a.OutTime,
                              TotalHours=a.TotalHours,
                              BreakingHours=a.BreakingHours,
                              WorkingHours=a.WorkingHours,
                              Explanation=a.Explanation,
                              Status=a.Status
                          })
                  .ToListAsync();

        }

        //public async Task<List<AttendenceModel>> GetRequestSearch(int id, DateTime? search)
        //{
        //    return await (from a in _context.Attendence.Where(x => x.EmpId == id && (x.Date == (DateTime)search))
        //                  select new AttendenceModel()
        //                  {
        //                      Date = (DateTime)a.Date,
        //                      InTime = (DateTime)a.InTime,
        //                      OutTime = (DateTime)a.OutTime,
        //                      TotalHours = a.TotalHours,
        //                      BreakingHours = a.BreakingHours,
        //                      WorkingHours = a.WorkingHours,
        //                      Explanation = a.Explanation,
        //                      Status=a.Status

        //                  })
        //          .ToListAsync();

        //}

        public bool CheckExist(int id,DateTime date)
        {
            bool result = _context.Attendence.ToList().Exists(x => (x.EmpId == id) && (x.Date == date.Date));
            return result;
        }

        public async Task<List<AttendenceModel>> GetAttendenceApprove()
        {
            return await (from a in _context.Attendence.Where(x => x.Status == "Pending")
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          join c in _context.Job on b.JobTitleId equals c.JobId
                          select new AttendenceModel()
                          {
                              AttendenceId = a.AttendenceId,
                              Date = (DateTime)a.Date,
                              InTime = (DateTime)a.InTime,
                              OutTime = (DateTime)a.OutTime,
                              TotalHours = a.TotalHours,
                              BreakingHours = a.BreakingHours,
                              WorkingHours = a.WorkingHours,
                              Explanation = a.Explanation,
                              EmpName = b.EmpFName + " " + b.EmpLName,
                              Designation = c.JobName,
                              AppliedDate=(DateTime)a.AppliedDate
                      


                          })
                   .ToListAsync();
        }

        public async Task<bool> ApproveAttendence(int id, String name)
        {
            var attendence = await _context.Attendence.FindAsync(id);
            attendence.Status = "Approved";
            attendence.Approver = name;

            _context.Entry(attendence).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> NotApproveAttendence(int id, String name)
        {
            var attendence = await _context.Attendence.FindAsync(id);
            attendence.Status = "Not Approved";
            attendence.Approver = name;

            _context.Entry(attendence).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public bool CheckExistAttendence(int id)
        {
            bool result = _context.MarkAttendence.ToList().Exists(x => (x.EmpId == id) && (x.Date == DateTime.Now.Date));
            return result;
        }
    }
}
