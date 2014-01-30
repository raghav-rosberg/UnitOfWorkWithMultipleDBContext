using System;
using System.Collections.Generic;
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

        public void Delete(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
