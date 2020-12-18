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
               
            };

            await _context.TimeTracker.AddAsync(timeTracker);
            await _context.SaveChangesAsync();

            return timeTracker.TrackId;
        }

        public async Task<bool> UpdateTrack(TimeTrackerModel timeTrackerModel)
        {

            var track = await _context.TimeTracker.FindAsync(timeTrackerModel.TrackId);
            track.OutTime = DateTime.Now;
            track.TotalHours = timeTrackerModel.TotalHours;
   
            _context.Entry(track).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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

        public async Task<bool> StartBreak(int id)
        {

            var track = await _context.TimeTracker.FindAsync(id);
            track.BreakStart = DateTime.Now;

            _context.Entry(track).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> EndBreak(TimeTrackerModel timeTrackerModel)
        {

            var track = await _context.TimeTracker.FindAsync(timeTrackerModel.TrackId);
            track.BreakEnd = DateTime.Now;
            track.BreakingHours = timeTrackerModel.BreakingHours+track.BreakingHours;

            _context.Entry(track).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
