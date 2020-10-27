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

        public async Task<List<JobModel>> GetJobTypes()
        {
            return await _context.JobTypes.Select(x => new JobModel()
            {

                Id = x.Id,
                Name=x.Name,
                Description=x.Description

            }).ToListAsync();
        }


    }
}
