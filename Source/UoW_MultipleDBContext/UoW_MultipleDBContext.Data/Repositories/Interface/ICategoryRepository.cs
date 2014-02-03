using System.Collections.Generic;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Entity.Custom;

namespace UoW_MultipleDBContext.Data.Repositories.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<CategoryWithExpense> GetCategoryWithExpenses();
    }
}