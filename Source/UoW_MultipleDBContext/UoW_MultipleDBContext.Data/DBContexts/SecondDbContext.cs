using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Data.DBContexts
{
    public class SecondDbContext : DbContext
    {
        static SecondDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SecondDbContext>());
        }

        public IDbSet<Department> Departments { get; set; }
        public IDbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }
}
