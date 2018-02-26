using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Data.DBContexts
{
    public class FirstDbContext : DbContext
    {
        //static FirstDbContext()
        //{
        //    //Database.SetInitializer(new CreateDatabaseIfNotExists<FirstDbContext>());
        //}
        //private string _nameOrConnectionStrfing;
        public FirstDbContext() : base("Name=FirstDbContext")
        {
            //InstanceId = Guid.NewGuid();
            //_nameOrConnectionString = nameOrConnectionString;
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Expense> Expenses { get; set; }
        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}