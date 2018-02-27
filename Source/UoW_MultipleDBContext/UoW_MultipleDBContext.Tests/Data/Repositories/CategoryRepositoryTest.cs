using Autofac;
using AutofacContrib.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Data.Repositories;
using UoW_MultipleDBContext.Data.Repositories.Interface;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Tests.Unit.Data.Repositories
{
    public class CategoryRepositoryTest
    {
        public AutoSubstitute AutoSubstitute { get; private set; }
        [SetUp]
        public void Init()
        {
            AutoSubstitute = new AutoSubstitute();

            // Register commonlog module
            AutoSubstitute = new AutoSubstitute((builder) =>
            {
                //builder.RegisterModule<CommonLoggingModule>();
                //builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
                builder.RegisterType<FirstDbContext>().InstancePerLifetimeScope();
            });
            //AutoSubstitute.Resolve<IDBOneRepositories>();
            //AutoSubstitute.Resolve<ICategoryRepository>();
        }

        [Test]
        public void GetCategoryWithExpenses_EmptyDB_CountIsZero()
        {
            AutoSubstitute.Resolve<ICategoryRepository>();

            var data = new List<Category>().AsEnumerable();
            var repoBase = this.AutoSubstitute.SubstituteFor<IRepository<Category>>();
            repoBase.GetAll().Returns(data);
            // Act
            var action = this.AutoSubstitute.Resolve<CategoryRepository>().GetCategoryWithExpenses();

            // Assert 
            Assert.AreEqual(0, action.ToList().Count());
        }
    }
}
