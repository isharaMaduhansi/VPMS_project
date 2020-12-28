using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Data;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public class TimeTrackRepo
    {
        private readonly EmpStoreContext _context = null;

        public TimeTrackRepo(EmpStoreContext context)
        {
            _context = context;
        }

        public async Task<int> InTimeMark(TimeTrackerModel timeTrackerModel)
        {
            var timeTracker = new TimeTracker()
            {
                Date = DateTime.Now.Date,
               InTime=DateTime.Now,
                TotalHours =  0,
                WorkingHours =  0,
                BreakingHours =  0,
                EmpId= timeTrackerModel.EmpId,
                Status= "Work"

            };

            await _context.TimeTracker.AddAsync(timeTracker);
            await _context.SaveChangesAsync();

            var info = _context.MarkAttendence.SingleOrDefault(x => (x.EmpId == timeTrackerModel.EmpId) && (x.Date == DateTime.Now.Date));
            if (info == null)
            {
                var markAttendence = new MarkAttendence()
                {
                    Date = DateTime.Now.Date,
                    InTime = DateTime.Now,
                    EmpId = timeTrackerModel.EmpId,
                    Type = "Auto",
                    Status = "Absent"

                };

                await _context.MarkAttendence.AddAsync(markAttendence);
                await _context.SaveChangesAsync();

            }
            else
            {
                info.InTime = DateTime.Now;
                info.Type = "Auto";
                info.Status = "Absent";

                _context.Entry(info).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }

            return timeTracker.TrackId;
        }

        public async Task<bool> UpdateTrack(TimeTrackerModel timeTrackerModel)
        {
         
            var track = await _context.TimeTracker.FindAsync(timeTrackerModel.TrackId);
            track.OutTime = DateTime.Now;
            track.TotalHours = timeTrackerModel.TotalHours;
            track.WorkingHours = timeTrackerModel.WorkingHours;
            track.Status = "Finish";
            track.Type = "Auto";

   
            _context.Entry(track).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            if (timeTrackerModel.TotalHours>=10) 
            {
                var info = _context.MarkAttendence.SingleOrDefault(x => (x.EmpId == track.EmpId) && (x.Date == DateTime.Now.Date));
                if (info == null)
                {
                    var markAttendence = new MarkAttendence()
                    {
                        Date = DateTime.Now.Date,
                        InTime = track.InTime,
                        OutTime = DateTime.Now,
                        TotalHours = timeTrackerModel.TotalHours,
                        EmpId = track.EmpId,
                        Type = "Auto",
                        Status = "Present"

                    };

                    await _context.MarkAttendence.AddAsync(markAttendence);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    info.OutTime = DateTime.Now;
                    info.TotalHours = timeTrackerModel.TotalHours;
                    info.Type = "Auto";
                    info.Status = "Present";


                    _context.Entry(info).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
            }
            else {
                var info = _context.MarkAttendence.SingleOrDefault(x => (x.EmpId == track.EmpId) && (x.Date == DateTime.Now.Date));
                if (info == null)
                {
                    var markAttendence = new MarkAttendence()
                    {
                        Date = DateTime.Now.Date,
                        InTime = track.InTime,
                        OutTime = DateTime.Now,
                        TotalHours = timeTrackerModel.TotalHours,
                        EmpId = track.EmpId,
                        Type = "Auto",
                        Status = "Not Complete"

                    };

                    await _context.MarkAttendence.AddAsync(markAttendence);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    info.OutTime = DateTime.Now;
                    info.Type = "Auto";
                    info.Status = "Not Complete";
                    info.TotalHours = timeTrackerModel.TotalHours;

                    _context.Entry(info).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
            }


            

            return true;

        }

        public async Task<TimeTrackerModel> GetTime(int id)
        {
            return await (from a in _context.TimeTracker.Where(x => x.EmpId == id && x.Date == DateTime.Now.Date)
                          select new TimeTrackerModel()
                          {
                              TrackId=a.TrackId,
                              InTime = (DateTime)a.InTime,
                              OutTime = (DateTime)a.OutTime,
                              TotalHours=a.TotalHours,
                              BreakingHours=a.BreakingHours,
                              EmpId = a.EmpId,
                              Status=a.Status

                          })
                  .FirstOrDefaultAsync();

        }

        public bool CheckExist(int id)
        {
            bool result = _context.TimeTracker.ToList().Exists(x => (x.EmpId == id) && (x.Date == DateTime.Now.Date));
            return result;
        }

        public bool CheckIn(int id)
        {
            bool result = _context.TimeTracker.ToList().Exists(x => (x.EmpId == id) && (x.Date == DateTime.Now.Date) && (x.InTime != null));
            return result;
        }

        public bool CheckOut(int id)
        {
            bool result = _context.TimeTracker.ToList().Exists(x => (x.EmpId == id) && (x.Date == DateTime.Now.Date) && (x.OutTime == null));
            return result;
        }

        public async Task<bool> CheckOut1(int id)
        {
            var track = await _context.TimeTracker.FindAsync(id);
            if (track.OutTime == null && track.Status!="Break")
                return true;
            else
                return false;
        }

        public async Task<bool> CheckOut2(int id)
        {
            var track = await _context.TimeTracker.FindAsync(id);
            if (track.OutTime == null && track.Status == "Break")
                return true;
            else
                return false;
        }


        public bool CheckBreak(int id)
        {
            bool result = _context.TimeTracker.ToList().Exists(x => (x.EmpId == id) && (x.Date == DateTime.Now.Date) && (x.Status=="Break") );
            return result;
        }

        public async Task<bool> StartBreak(int id)
        {

            var track = await _context.TimeTracker.FindAsync(id);
            track.BreakStart = DateTime.Now;
            track.Status = "Break";

            _context.Entry(track).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> EndBreak(TimeTrackerModel timeTrackerModel)
        {

            var track = await _context.TimeTracker.FindAsync(timeTrackerModel.TrackId);
            track.BreakEnd = DateTime.Now;
            track.BreakingHours = timeTrackerModel.BreakingHours+track.BreakingHours;
            track.Status = "Work";

            _context.Entry(track).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<List<TimeTrackerModel>> TrackInfoById(int id)
        {
            return await (from a in _context.TimeTracker.Where(x => x.EmpId == id)
                          select new TimeTrackerModel()
                          {
                              TrackId = a.TrackId,
                              Date= (DateTime)a.Date,
                              InTime = (DateTime)a.InTime,
                              OutTime = (DateTime)a.OutTime,
                              TotalHours = a.TotalHours,
                              BreakingHours = a.BreakingHours,
                              WorkingHours=a.WorkingHours,
                              Status=a.Status

                          })
                  .ToListAsync();

        }

        public bool CheckExistAttendence(int id)
        {
            bool result = _context.MarkAttendence.ToList().Exists(x => (x.EmpId == id) && (x.Date == DateTime.Now.Date));
            return result;
        }



    }
}
