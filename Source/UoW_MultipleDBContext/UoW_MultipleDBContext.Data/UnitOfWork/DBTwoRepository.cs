using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Data.UnitOfWork
{
    public class DBTwoRepositories<TContext> : UnitOfWork<SecondDbContext>, IDBTwoRepositories
    {
        private IRepository<Department> departmentRepositories;

        public IRepository<Department> DepartmentRepository
        {
            get
            {
                return this.departmentRepositories ?? (this.departmentRepositories = new RepositoryBase<Department>(base.DataContext));
            }
        }
    }
}
