using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Entity.Custom;

namespace UoW_MultipleDBContext.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork<FirstDbContext> _unitOfWork;

        public CategoryService(IUnitOfWork<FirstDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetAll()
        {
            var ds = _unitOfWork.CategoryRepository.GetAll();
            _unitOfWork.Commit();
            return ds;
        }

        public Category GetById(int id)
        {
            return _unitOfWork.CategoryRepository.GetById(id);
        }

        public IEnumerable<CategoryWithExpense> GetCategoryWithExpenses()
        {
            return _unitOfWork.CategoryRepository.GetCategoryWithExpenses();
        }

        public void Insert(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Insert(entity);
            _unitOfWork.Commit();
        }

        public void Update(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _unitOfWork.CategoryRepository.GetById(id);
        }

        public void Delete(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Delete(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Expression<Func<Category, bool>> @where)
        {
            _unitOfWork.CategoryRepository.Delete(@where);
        }

        public Category Get(Expression<Func<Category, bool>> @where)
        {
            return _unitOfWork.CategoryRepository.Get(@where);
        }

        public IEnumerable<Category> GetMany(Expression<Func<Category, bool>> @where)
        {
            return _unitOfWork.CategoryRepository.GetMany(@where);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.GetAsync(category => category.Id == id);
        }

        public Task<List<Category>> GetAllAsync()
        {
            return _unitOfWork.CategoryRepository.GetAllAsync();
        }

        public Task<Category> GetAsync(Expression<Func<Category, bool>> @where)
        {
            return _unitOfWork.CategoryRepository.GetAsync(@where);
        }

        public Task<List<Category>> GetManyAsync(Expression<Func<Category, bool>> @where)
        {
            return _unitOfWork.CategoryRepository.GetManyAsync(@where);
        }
    }
}
