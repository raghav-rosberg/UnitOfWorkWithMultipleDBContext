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

        public int Insert(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Insert(entity);
            return _unitOfWork.Commit();
        }

        public int Update(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Update(entity);
            return _unitOfWork.Commit();
        }

        public int Delete(int id)
        {
            _unitOfWork.CategoryRepository.Delete(id);
            return _unitOfWork.Commit();
        }

        public int Delete(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Delete(entity);
            return _unitOfWork.Commit();
        }

        public int Delete(Expression<Func<Category, bool>> @where)
        {
            _unitOfWork.CategoryRepository.Delete(@where);
            return _unitOfWork.Commit();
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

        public async Task<int> InsertAsync(Category entity)
        {
            _unitOfWork.CategoryRepository.Insert(entity);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> UpdateAsync(Category entity)
        {
            _unitOfWork.CategoryRepository.Update(entity);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(Category entity)
        {
            _unitOfWork.CategoryRepository.Delete(entity);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            _unitOfWork.CategoryRepository.Delete(id);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<Category, bool>> @where)
        {
            _unitOfWork.CategoryRepository.Delete(where);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<Category> GetAsync(Expression<Func<Category, bool>> @where)
        {
            return await _unitOfWork.CategoryRepository.GetAsync(where);
        }

        public async Task<List<Category>> GetManyAsync(Expression<Func<Category, bool>> @where)
        {
            return await _unitOfWork.CategoryRepository.GetManyAsync(where);
        }
    }
}