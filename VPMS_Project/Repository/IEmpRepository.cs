using System.Collections.Generic;
using System.Threading.Tasks;
using VPMS_Project.Models;

namespace VPMS_Project.Repository
{
    public interface IEmpRepository
    {
        Task<int> AddEmp(EmpModel empModel);
        Task<List<EmpModel>> GetAllEmps();

        Task<List<EmpModel>> GetDeletedEmps();
        Task<EmpModel> GetEmpById(int id);
        Task<List<EmpModel>> GetEmpListAsync(string name, string job,int id);
        Task<bool> UpdateEmp(EmpModel empModel);

        Task<bool> DeleteEmp(int id);
    }
}