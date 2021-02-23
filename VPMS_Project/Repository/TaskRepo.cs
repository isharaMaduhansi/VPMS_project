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

    }
}
