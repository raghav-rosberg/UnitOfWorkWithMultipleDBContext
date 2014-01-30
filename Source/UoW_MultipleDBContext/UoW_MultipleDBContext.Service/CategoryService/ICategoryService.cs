using System.Collections.Generic;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Entity.Custom;

namespace UoW_MultipleDBContext.Service.CategoryService
{
    public interface ICategoryService : IRepository<Category>
    {
        IEnumerable<CategoryWithExpense> GetCategoryWithExpenses();
    }
}
