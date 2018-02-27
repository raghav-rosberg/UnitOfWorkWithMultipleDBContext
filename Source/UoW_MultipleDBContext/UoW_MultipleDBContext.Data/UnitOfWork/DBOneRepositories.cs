using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.Repositories;
using UoW_MultipleDBContext.Data.Repositories.Interface;

namespace UoW_MultipleDBContext.Data.UnitOfWork
{
    public class DBOneRepositories : UnitOfWork<FirstDbContext>, IDBOneRepositories
    {
        private ICategoryRepository categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                return this.categoryRepository ?? (this.categoryRepository = new CategoryRepository(base.DataContext));
            }
        }
    }
}
