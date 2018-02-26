using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Service.IService;

namespace UoW_MultipleDBContext.Service
{
    public class DepartmentService : IDepartmentService
    {
        private IDBTwoRepositories departmentRepositories;
        public DepartmentService(IDBTwoRepositories departmentRepositories)
        {
            this.departmentRepositories = departmentRepositories;
        }
        public void CreateNewDepartment(string name)
        {
            this.departmentRepositories.DepartmentRepository.Insert(new Entity.Department { Name = name });
            this.departmentRepositories.Commit();
        }
    }

}
