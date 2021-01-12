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
                              EmpId=a.EmpId,
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

            DateTime date = (DateTime)attendence.Date;
            var info = _context.MarkAttendence.SingleOrDefault(x => (x.EmpId == attendence.EmpId) && (x.Date == date.Date));
            if (info == null)
            {
                var markAttendence = new MarkAttendence()
                {
                    Date = attendence.Date,
                    InTime = attendence.InTime,
                    OutTime=attendence.OutTime,
                    TotalHours=attendence.TotalHours,
                    EmpId = attendence.EmpId,
                    Type = "Manual",
                    Status = "Present"

                };

                await _context.MarkAttendence.AddAsync(markAttendence);
                await _context.SaveChangesAsync();

            }
            else
            {
                info.InTime = attendence.InTime;
                info.OutTime = attendence.OutTime;
                info.TotalHours = attendence.TotalHours;
                info.Type = "Manual";
                info.Status = "Present";

                _context.Entry(info).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }

            return true;
        }

        public async Task<bool> NotApproveAttendence(int id, String name)
        {
            var attendence = await _context.Attendence.FindAsync(id);
            attendence.Status = "Not Approved";
            attendence.Approver = name;

            _context.Entry(attendence).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            DateTime date = (DateTime)attendence.Date;
            var info = _context.MarkAttendence.SingleOrDefault(x => (x.EmpId == attendence.EmpId) && (x.Date == date.Date));
            if (info == null)
            {
                var markAttendence = new MarkAttendence()
                {
                    Date = attendence.Date,
                    InTime = attendence.InTime,
                    OutTime = attendence.OutTime,
                    TotalHours = attendence.TotalHours,
                    EmpId = attendence.EmpId,
                    Type = "Manual",
                    Status = "Absent"

                };

                await _context.MarkAttendence.AddAsync(markAttendence);
                await _context.SaveChangesAsync();

            }
            else
            {
                info.InTime = attendence.InTime;
                info.OutTime = attendence.OutTime;
                info.TotalHours = attendence.TotalHours;
                info.Type = "Manual";
                info.Status = "Absent";

                _context.Entry(info).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }

            return true;

        }

        public async Task<List<MarkAttendenceModel>> GetAttInfo(int id)
        {
            return await (from a in _context.MarkAttendence.Where(x => x.EmpId == id)
                          select new MarkAttendenceModel()
                          {
                              Date = (DateTime)a.Date,
                              InTime = (DateTime)a.InTime,
                              OutTime = (DateTime)a.OutTime,
                              TotalHours = a.TotalHours,
                              Type=a.Type,
                              Status = a.Status
                          })
                  .OrderBy(x => x.Date).ToListAsync();

        }

        public async Task<List<TodayWorkModel>> TodayWorkersAsync()
        {
            return await (from a in _context.MarkAttendence.Where(x => (x.Date==DateTime.Now.Date) && (x.Status != "Leave On Day"))
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          select new TodayWorkModel()
                          {
                              EmpName = b.EmpFName + " " + b.EmpLName,
                              PhotoURL=b.ProfilePhoto
                          })
                      .ToListAsync();

        }

        public async Task<List<Key2Model>> SearchLeave1(int id)
        {
            return await (from a in _context.MarkAttendence.Where(x => (x.EmpId == id))
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          select new Key2Model()
                          {
                              AttendenceId = a.MarkAttendenceId,
                              EmpId = a.EmpId,
                              EmpFName = b.EmpFName,
                              EmpFullName = b.EmpFName + " " + b.EmpLName,
                              PhotoURL = b.ProfilePhoto,
                              RelavantDate = (DateTime)a.Date,
                              Status=a.Status,


                          })
                   .ToListAsync();
        }

        public async Task<List<Key2Model>> SearchLeave2(int id, DateTime date)
        {
            return await (from a in _context.MarkAttendence.Where(x => (x.EmpId == id) && (x.Date == date.Date))
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          select new Key2Model()
                          {
                              AttendenceId = a.MarkAttendenceId,
                              EmpId = a.EmpId,
                              EmpFName = b.EmpFName,
                              EmpFullName = b.EmpFName + " " + b.EmpLName,
                              PhotoURL = b.ProfilePhoto,
                              RelavantDate = (DateTime)a.Date,
                              Status = a.Status,


                          })
                   .ToListAsync();
        }

        public async Task<List<Key2Model>> SearchLeave3(DateTime date)
        {
            return await (from a in _context.MarkAttendence.Where(x =>  (x.Date == date.Date))
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          select new Key2Model()
                          {
                              AttendenceId = a.MarkAttendenceId,
                              EmpId = a.EmpId,
                              EmpFName = b.EmpFName,
                              EmpFullName = b.EmpFName + " " + b.EmpLName,
                              PhotoURL = b.ProfilePhoto,
                              RelavantDate = (DateTime)a.Date,
                              Status = a.Status,
                          })
                   .ToListAsync();
        }

        public async Task<MarkAttendenceModel> GetSearchAttendenceAsync(int id)
        {
            return await (from a in _context.MarkAttendence.Where(x => x.MarkAttendenceId == id)
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          select new MarkAttendenceModel()
                          {
                              Date = (DateTime)a.Date,
                              InTime = (DateTime)a.InTime,
                              OutTime = (DateTime)a.OutTime,
                              TotalHours = a.TotalHours,
                              Type = a.Type,
                              Status = a.Status,
                              EmpId = a.EmpId,
                              EmpName = b.EmpFName + " " + b.EmpLName,
                          })
                  .FirstOrDefaultAsync();

        }

        public async Task<List<MarkAttendenceModel>> EmpNotUpdate()
        {
            return await (from a in _context.MarkAttendence.Where(x => (x.Status == "Not Complete") || ((x.Status == "Absent") && (x.Type=="Auto")) && (x.Date< DateTime.Now.Date))
                          join b in _context.Employees on a.EmpId equals b.EmpId
                          select new MarkAttendenceModel()
                          {
                              EmpId = a.EmpId,
                              EmpName = b.EmpFName + " " + b.EmpLName,
                              PhotoURL = b.ProfilePhoto,
                              Date = (DateTime)a.Date,
                              Status = a.Status,
                              Type=a.Type
                          })
                   .ToListAsync();
        }

        public async Task<WorkHourModel> GetStandardWorkHours()
        {
            return await (from a in _context.StandardWorkHours
                          select new WorkHourModel()
                          {
                              HourId=a.HourId,
                              NoOfHours=a.NoOfHours
                          })
                  .FirstOrDefaultAsync();

        }

        public async Task<bool> UpdateHour(WorkHourModel workHourModel)
        {

            var hour = await _context.StandardWorkHours.FindAsync(workHourModel.HourId);
            hour.NoOfHours = workHourModel.NoOfHours;
            _context.Entry(hour).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }


    }
}
