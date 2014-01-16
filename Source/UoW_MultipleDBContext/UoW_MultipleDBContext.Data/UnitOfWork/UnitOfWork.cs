using System.Data.Entity;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Data.UnitOfWork
{
    public class UnitOfWork<TContext> : Disposable, IUnitOfWork<TContext>
        where TContext : DbContext, new()
    {
        public virtual void Commit()
        {
            _dataContext.SaveChanges();
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
        private IRepository<Category> _categoryRepository;
        private IRepository<Department> _departmentRepository;

        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new RepositoryBase<Category>(_dataContext)); }
        }

        public IRepository<Department> DepartmentRepository
        {
            get
            {
                return _departmentRepository ?? (_departmentRepository = new RepositoryBase<Department>(_dataContext));
            }
        }
    }
}
