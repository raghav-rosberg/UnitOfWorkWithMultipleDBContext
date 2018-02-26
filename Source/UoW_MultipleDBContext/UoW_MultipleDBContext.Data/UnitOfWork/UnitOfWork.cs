using System.Data.Entity;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Data.DBContexts;
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
            return DataContext.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            return DataContext.SaveChangesAsync();
        }

        protected readonly DbContext DataContext;

        public UnitOfWork()
        {
            DataContext = new TContext();
        }

        protected override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }

   
    
}