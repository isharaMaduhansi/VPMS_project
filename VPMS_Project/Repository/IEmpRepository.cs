using System.Collections.Generic;
using System.Threading.Tasks;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public interface IEmpRepository
    {
        Task<int> AddEmp(EmpModel empModel);
        Task<List<EmpModel>> GetAllEmps();
        Task<EmpModel> GetEmpById(int id);
        Task<List<EmpModel>> GetEmpListAsync(string name, string job);
        Task<bool> UpdateEmp(EmpModel empModel);
    }
}