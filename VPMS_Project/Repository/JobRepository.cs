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
                Description = x.Description,
                Medical=x.Medical,
                Annual=x.Annual,
                Casual=x.Casual,
                HalfDays=x.HalfDays,
                ShortLeaves=x.ShortLeaves,
                TotalLeaves=x.Medical+x.Annual+x.Casual+x.ShortLeaves+x.HalfDays
            }).ToListAsync();
        }

        public async Task<JobModel> GetJobById(int id)
        {
            return await _context.Job.Where(x => x.JobId == id).Select(job => new JobModel()
            {
                JobId = job.JobId,
                JobName = job.JobName,
                Description = job.Description,
                Medical = job.Medical,
                Annual = job.Annual,
                Casual = job.Casual,
                HalfDays = job.HalfDays,
                ShortLeaves = job.ShortLeaves,

            }).FirstOrDefaultAsync();
        }

        public async Task<bool> Updatejob(JobModel jobModel)
        {

            var job = await _context.Job.FindAsync(jobModel.JobId);
            job.JobName = jobModel.JobName;
            job.Casual = jobModel.Casual.HasValue ? jobModel.Casual.Value : 0;
            job.Annual = jobModel.Annual.HasValue ? jobModel.Annual.Value : 0;
            job.Medical = jobModel.Medical.HasValue ? jobModel.Medical.Value : 0;
            job.ShortLeaves = jobModel.ShortLeaves.HasValue ? jobModel.ShortLeaves.Value : 0;
            job.HalfDays = jobModel.HalfDays.HasValue ? jobModel.HalfDays.Value : 0;
           

            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<int> AddJob(JobModel jobModel)
        {
            var newjob = new Job()
            {
                JobName = jobModel.JobName,
                Medical = jobModel.Medical.HasValue ? jobModel.Medical.Value : 0,
                Annual =jobModel.Annual.HasValue ? jobModel.Annual.Value : 0,
                Casual =jobModel.Casual.HasValue ? jobModel.Casual.Value : 0,
                HalfDays =jobModel.HalfDays.HasValue ? jobModel.HalfDays.Value : 0,
                ShortLeaves =jobModel.ShortLeaves.HasValue ? jobModel.ShortLeaves.Value : 0
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
