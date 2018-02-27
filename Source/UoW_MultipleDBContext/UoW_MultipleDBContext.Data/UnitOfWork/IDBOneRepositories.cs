using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.Repositories.Interface;
using UoW_MultipleDBContext.Data.UnitOfWork;

namespace UoW_MultipleDBContext.Data.UnitOfWork
{
    public interface IDBOneRepositories : IUnitOfWork<FirstDbContext>
    {
        ICategoryRepository CategoryRepository { get; }
    }
}
