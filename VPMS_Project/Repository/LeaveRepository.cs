using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Data;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public class LeaveRepository
    {
        private readonly EmpStoreContext _context = null;

        public LeaveRepository(EmpStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddLeave(LeaveApplyModel LeaveApplyModel)
        {
            var leaveApply = new LeaveApply()
            {

                LeaveType = LeaveApplyModel.LeaveType,
                Startdate = LeaveApplyModel.Startdate,
                EndDate = LeaveApplyModel.EndDate,
                Reason=LeaveApplyModel.Reason,
                EmpId=LeaveApplyModel.EmpId,
                AppliedDate=DateTime.UtcNow,
                NoOfDays=LeaveApplyModel.NoOfDays,
                Status="Waiting for Recommendation"
                
            };

            await _context.LeaveApply.AddAsync(leaveApply);
            await _context.SaveChangesAsync();

            return leaveApply.LeaveApplyId;
        }

     

        public async Task<LeaveApplyModel> GetEmpLeaveById(int id)
        {
            return await (from a in _context.Employees.Where(x => x.EmpId == id)
                          select new LeaveApplyModel()
                          {
                              FromDate = (DateTime)a.FromDate,
                              ToDate = (DateTime)a.Todate,
                          
                          })
                   .FirstOrDefaultAsync();
        }

        public async Task<List<LeaveApplyModel>> GetAllLeaveById(int id)
        {
            return await (from a in _context.LeaveApply.Where(x => x.EmpId == id)
                          select new LeaveApplyModel()
                          {
                              LeaveApplyId=a.LeaveApplyId,
                              LeaveType=a.LeaveType,
                              Startdate= (DateTime)a.Startdate,
                              EndDate= (DateTime)a.EndDate,
                              Reason=a.Reason,
                              AppliedDate= (DateTime)a.AppliedDate,
                              NoOfDays=a.NoOfDays,
                              EmpId=a.EmpId,
                              Status=a.Status

                         })
                   .ToListAsync();
        }

        public async Task<List<LeaveApplyModel>> GetLeaveRecommend()
        {
            return await (from a in _context.LeaveApply.Where(x => x.Status == "Waiting for Recommendation")
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          join c in _context.Job on b.JobTitleId equals c.JobId
                          select new LeaveApplyModel()
                          {
                              LeaveApplyId = a.LeaveApplyId,
                              LeaveType = a.LeaveType,
                              Startdate = (DateTime)a.Startdate,
                              EndDate = (DateTime)a.EndDate,
                              Reason = a.Reason,
                              AppliedDate = (DateTime)a.AppliedDate,
                              NoOfDays = a.NoOfDays,
                              EmpId = a.EmpId,
                              Status = a.Status,
                              EmpName=b.EmpFName+" "+b.EmpLName,
                              Designation=c.JobName


                          })
                   .ToListAsync();
        }

        public async Task<LeaveApplyModel> GetOneLeaveById(int id)
        {
            return await (from a in _context.LeaveApply.Where(x => x.LeaveApplyId == id)
                          select new LeaveApplyModel()
                          {
                              LeaveApplyId = a.LeaveApplyId,
                              LeaveType = a.LeaveType,
                              Startdate = (DateTime)a.Startdate,
                              EndDate = (DateTime)a.EndDate,
                              Reason = a.Reason,
                              AppliedDate = (DateTime)a.AppliedDate,
                              NoOfDays = a.NoOfDays,
                              EmpId = a.EmpId,
                              Status=a.Status

                          })
                  .FirstOrDefaultAsync();

        }
  

            public async Task<LeaveApplyModel> GetEmpLeaveJoinById(int id)
        {
            return await (from a in _context.LeaveApply.Where(x => x.LeaveApplyId == id)
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          select new LeaveApplyModel()
                          {
                              LeaveApplyId = a.LeaveApplyId,
                              LeaveType = a.LeaveType,
                              Startdate = (DateTime)a.Startdate,
                              EndDate = (DateTime)a.EndDate,
                              Reason = a.Reason,
                              AppliedDate = (DateTime)a.AppliedDate,
                              NoOfDays = a.NoOfDays,
                              EmpId = a.EmpId,
                              FromDate= (DateTime)b.FromDate,
                              ToDate= (DateTime)b.Todate

                          })
                  .FirstOrDefaultAsync();

        }

        public async Task<bool> UpdateLeave(LeaveApplyModel leaveApplyModel)
        {
            var leave = await _context.LeaveApply.FindAsync(leaveApplyModel.LeaveApplyId);

            leave.LeaveType = leaveApplyModel.LeaveType;
            leave.Startdate = leaveApplyModel.Startdate;
            leave.EndDate = leaveApplyModel.EndDate;
            leave.Reason = leaveApplyModel.Reason;
            leave.NoOfDays = leaveApplyModel.NoOfDays;

            _context.Entry(leave).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteLeave(int id)
        {

            var leave = await _context.LeaveApply.FindAsync(id);
            _context.Entry(leave).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
