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
            return await (from a in _context.Task.Where(x => (x.EmployeesId == id) && (x.TaskComplete == false))
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
            task.TaskComplete = true;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> NotCompleteTask(int id)
        {

            var task = await _context.Task.FindAsync(id);
            task.TaskComplete = false;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<List<TaskModel>> AddTaskList(int id)
        {
            return await (from a in _context.Task.Where(x => (x.EmployeesId == id) && (x.TaskComplete == true) && (x.TimeSheet == false))
                          select new TaskModel()
                          {
                              Id=a.Id,
                             Name=a.Name,
                             AllocatedHours=a.AllocatedHours,
                             EmpId=id

                          }).ToListAsync();
        }
    }
}
