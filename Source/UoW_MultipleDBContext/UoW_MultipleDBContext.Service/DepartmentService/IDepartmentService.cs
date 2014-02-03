using System.Threading.Tasks;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Data.Repositories.Interface;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Service.DepartmentService
{
    public interface IDepartmentService : IRepository<Department>
    {
        Task<Department> GetByIdAsync(int id);
        Task<int> InsertAsync(Department entity);
    }
}
