using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                throw new ObjectNotFoundException("entity");
            Dbset.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            Dbset.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> @where)
        {
            IEnumerable<TEntity> objects = Dbset.Where<TEntity>(where).AsEnumerable();
            foreach (TEntity obj in objects)
                Dbset.Remove(obj);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> @where)
        {
            return Dbset.Where(where).FirstOrDefault();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return Dbset.ToListAsync();
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where)
        {
            return Dbset.Where(where).FirstOrDefaultAsync();
        }

        public Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> @where)
        {
            return Dbset.Where(where).ToListAsync();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return Dbset.Where(where).ToList();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
