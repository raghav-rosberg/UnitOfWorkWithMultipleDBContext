using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Data.Repositories.Interface;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Entity.Custom;

namespace UoW_MultipleDBContext.Data.Repositories
{
    public class CategoryRepository: RepositoryBase<Category>, ICategoryRepository
    {
        private readonly FirstDbContext _dbContext;
        public CategoryRepository(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = (_dbContext ?? (FirstDbContext)dbContext);
        }

        public IEnumerable<CategoryWithExpense> GetCategoryWithExpenses()
        {
            var category = GetAll().Join(_dbContext.Expenses, c => c.Id, e => e.CategoryId, (c, e) => new { c, e })
                    .GroupBy(z => new { z.c.Id, z.c.Name, z.c.Description }, z => z.e)
                    .Select(g => new CategoryWithExpense { CategoryId = g.Key.Id, CategoryName = g.Key.Name, Description = g.Key.Description, TotalExpenses = g.Sum(s => s.Amount) });
            return category;
        }
    }
}
