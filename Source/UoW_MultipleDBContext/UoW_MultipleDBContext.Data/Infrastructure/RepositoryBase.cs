using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace UoW_MultipleDBContext.Data.Infrastructure
{
    public class RepositoryBase<TEntity> : Disposable, IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext _dataContext;
        private IDbSet<TEntity> Dbset
        {
            get { return _dataContext.Set<TEntity>(); }
        }

        public RepositoryBase(DbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Dbset.AsEnumerable();
        }

        public TEntity GetById(int id)
        {
            return Dbset.Find(id);
        }

        public void Insert(TEntity entity)
        {
            Dbset.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            Dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            Dbset.Remove(entity);
            _dataContext.SaveChanges();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
