using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Data.UnitOfWork
{
    public class DBTwoRepositories<TContext> : UnitOfWork<SecondDbContext>, IDBTwoRepositories
    {
        private IRepository<Department> departmentRepository;

        public IRepository<Department> DepartmentRepository
        {
            get
            {
                return this.departmentRepository ?? (this.departmentRepository = new RepositoryBase<Department>(base.DataContext));
            }
        }

        private IRepository<Employee> employeeRepository;
        public IRepository<Employee> EmployeeRepository
        {
            get
            {
                return this.employeeRepository ?? (this.employeeRepository = new RepositoryBase<Employee>(base.DataContext));
            }
        }
    }
}
