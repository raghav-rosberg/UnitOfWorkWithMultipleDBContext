using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Entity.Custom;

namespace UoW_MultipleDBContext.Service.CategoryService
{
    public interface ICategoryService
    {
        IEnumerable<CategoryWithExpense> GetCategoryWithExpenses();

        IEnumerable<Category> GetAll();
        Category GetById(int id);
        int Insert(Category entity);
        int Update(Category entity);
        int Delete(int id);
        int Delete(Category entity);
        int Delete(Expression<Func<Category, bool>> where);
        Category Get(Expression<Func<Category, bool>> where);
        IEnumerable<Category> GetMany(Expression<Func<Category, bool>> where);


        Task<Category> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task<Category> GetAsync(Expression<Func<Category, bool>> where);
        Task<List<Category>> GetManyAsync(Expression<Func<Category, bool>> where);
        Task<int> InsertAsync(Category entity);
        Task<int> UpdateAsync(Category entity);
        Task<int> DeleteAsync(Category entity);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(Expression<Func<Category, bool>> where);
    }
}