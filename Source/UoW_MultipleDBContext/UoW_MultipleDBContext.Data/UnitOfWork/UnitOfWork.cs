using System.Data.Entity;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Data.Repositories;
using UoW_MultipleDBContext.Data.Repositories.Interface;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Data.UnitOfWork
{
    public class UnitOfWork<TContext> : Disposable, IUnitOfWork<TContext>
        where TContext : DbContext, new()
    {
        public virtual int Commit()
        {
            return _dataContext.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        private readonly DbContext _dataContext;

        public UnitOfWork()
        {
            _dataContext = new TContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }

        /// <summary>
        /// Define Repositories
        /// </summary>
        private ICategoryRepository _categoryRepository;

        public ICategoryRepository CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new CategoryRepository(_dataContext)); }
        }

        private IRepository<Department> _departmentRepository;

        public IRepository<Department> DepartmentRepository
        {
            get
            {
                return _departmentRepository ?? (_departmentRepository = new RepositoryBase<Department>(_dataContext));
            }
        }
    }
}