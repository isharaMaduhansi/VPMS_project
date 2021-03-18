using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Data;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public class TaskRepo
    {
        private readonly EmpStoreContext _context = null;

        public TaskRepo(EmpStoreContext context)
        {
            _context = context;
        }

        public async Task<List<TaskModel>> GetTaskListAsync(int id)
        {
            return await (from a in _context.Task.Where(x => (x.EmployeesId == id) && (x.TaskComplete == false) || (x.TimeSheet==false))
                          select new TaskModel()
                          {
                              Id=a.Id,
                              Name=a.Name,
                              AllocatedHours=a.AllocatedHours,
                              EmpId = id,
                              StartDate=a.StartDate,
                              EndDate=a.EndDate,
                              LastUpdate=a.LastUpdate,
                              CreatedDate=a.CreatedDate,
                              Description=a.Description
   
                          }).ToListAsync();
        }

        public async Task<List<TaskModel>> GetAllTaskListAsync(int id)
        {
            return await (from a in _context.Task.Where(x => (x.EmployeesId == id))
                          join b in _context.TimeSheetTask on a.Id equals b.TaskId
                          select new TaskModel()
                          {
                              Id = a.Id,
                              Name = a.Name,
                              AllocatedHours = a.AllocatedHours,
                              EmpId = id,
                              StartDate = a.StartDate,
                              EndDate = a.EndDate,
                              ActualStartDateTime=b.StartDateTime,
                              ActualEndDateTime=b.EndDateTime,
                              TakenHours=b.TotalHours,
                              LastUpdate = a.LastUpdate,
                              CreatedDate = a.CreatedDate,
                              Description = a.Description

                          }).ToListAsync();
        }

        public async Task<bool> CompleteTask(int id)
        {

            var task = await _context.Task.FindAsync(id);
            task.TimeSheet = true;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> AddTaskFromList(int id)
        {

            var task = await _context.Task.FindAsync(id);
            task.TaskComplete = true;
            task.TimeSheet = false;


            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> NotCompleteTask(int id)
        {

            var task = await _context.Task.FindAsync(id);
            task.TaskComplete = false;
            task.TimeSheet = true;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> CancelAddingTask(int id)
        {

            var task = await _context.Task.FindAsync(id);
            task.TaskComplete = false;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<List<TimeSheetTaskModel>> AddTaskList(int id)
        {
            return await (from a in _context.TimeSheetTask.Where(x => (x.EmployeesId == id) && (x.AppliedDate == DateTime.Now.Date))
                          join b in _context.Task on a.TaskId equals b.Id
                          select new TimeSheetTaskModel()
                          {
                             Id=a.Id,
                             Name=b.Name,
                             AppliedDate=a.AppliedDate,
                             ActualStartDateTime=a.StartDateTime,
                             ActualEndDateTime=a.EndDateTime,
                             TotalHours=a.TotalHours,
                             EmployeesId=a.EmployeesId,
                             TaskId=a.TaskId,
                             AllocatedHours=b.AllocatedHours,
                             

                          }).ToListAsync();
        }

        public async Task<int> TImeSheetTaskInsert(int id, DateTime Start, DateTime End, Double TotalHours)
        {

            var taskInfo = await _context.Task.FindAsync(id);
            var task = new TimeSheetTask()
            {
                AppliedDate=Start.Date,
                StartDateTime=Start,
                EndDateTime=End,
                TotalHours=TotalHours,
                EmployeesId=taskInfo.EmployeesId,
                TaskId=id

            };

            await _context.TimeSheetTask.AddAsync(task);
            await _context.SaveChangesAsync();

            return task.Id;


        }

        public async Task<TaskModel> GetTaskAsync(int id)
        {
            return await (from a in _context.Task.Where(x => (x.Id == id))
                          select new TaskModel()
                          {
                               Id=id,
                               Name=a.Name,
                               EmpId=a.EmployeesId

                          })
                  .FirstOrDefaultAsync();

        }

        public async Task<List<TimeSheetTaskModel>> GetTimeSheet(int id, DateTime date)
        {
            return await (from a in _context.TimeSheetTask.Where(x => (x.EmployeesId==id)&& (x.AppliedDate.Date==date.Date))
                          join b in _context.Task on a.TaskId equals b.Id
                          select new TimeSheetTaskModel()
                          {
                            Name=b.Name,
                            AllocatedHours=b.AllocatedHours,
                            ActualStartDateTime=a.StartDateTime,
                            ActualEndDateTime=a.EndDateTime,
                            TotalHours=a.TotalHours,


                          })
                  .ToListAsync();

        }

        public double GetTotalHours(int id, DateTime date)
        {
            double total = _context.TimeSheetTask.Where(x => (x.EmployeesId == id) && (x.AppliedDate.Date == date.Date)).Select(x => x.TotalHours).Sum();
            return total;

        }

        public async Task<TodayWorkModel> GetEmpData(int id)
        {
            return await (from a in _context.Employees.Where(x => (x.EmpId == id ))
                          select new TodayWorkModel()
                          {
                              EmpName = a.EmpFName + " " + a.EmpLName,
                              PhotoURL = a.ProfilePhoto
                          })
                      .FirstOrDefaultAsync();

        }

        public async Task<List<TimeSheetTaskModel>> GetWorkSheet(DateTime date)
        {
            return await (from a in _context.TimeSheetTask.Where(x =>(x.AppliedDate.Date == date.Date))
                          join b in _context.Task on a.TaskId equals b.Id
                          join c in _context.Employees on a.EmployeesId equals c.EmpId
                          select new TimeSheetTaskModel()
                          {
                              Name = b.Name,
                              AllocatedHours = b.AllocatedHours,
                              EmpName=c.EmpFName+" "+c.EmpLName,
                              ActualStartDateTime = a.StartDateTime,
                              ActualEndDateTime = a.EndDateTime,
                              TotalHours = a.TotalHours,


                          })
                  .ToListAsync();

        }

    }
}
