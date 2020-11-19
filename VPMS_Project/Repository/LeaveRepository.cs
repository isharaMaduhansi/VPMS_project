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

    }
}
