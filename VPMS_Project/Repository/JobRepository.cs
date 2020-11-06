using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Data;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public class JobRepository
    {
        private readonly EmpStoreContext _context = null;

        public JobRepository(EmpStoreContext context)
        {
            _context = context;
        }

        public async Task<List<JobModel>> GetJobs()
        {
            return await _context.Job.Select(x => new JobModel()
            {
                JobId = x.JobId,
                JobName = x.JobName,
                Description = x.Description

            }).ToListAsync();
        }


        public async Task<int> AddJob(JobModel jobModel)
        {
            var newjob = new Job()
            {
                JobName = jobModel.JobName,
            };

            await _context.Job.AddAsync(newjob);
            await _context.SaveChangesAsync();

            return newjob.JobId;
        }


        public async Task<bool> DeleteJob(int id)
        {

            var job = await _context.Job.FindAsync(id);
            _context.Entry(job).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
