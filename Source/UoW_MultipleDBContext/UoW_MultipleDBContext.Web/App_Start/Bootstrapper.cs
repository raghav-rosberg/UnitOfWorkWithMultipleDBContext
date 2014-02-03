using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Service.DepartmentService;
using UoW_MultipleDBContext.Service.CategoryService;
using UoW_MultipleDBContext.Web.Mappings;

namespace UoW_MultipleDBContext.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterGeneric(typeof (UnitOfWork<>)).As(typeof (IUnitOfWork<>));
            builder.RegisterGeneric(typeof (RepositoryBase<>)).As(typeof (IRepository<>));
            builder.RegisterType(typeof (CategoryService)).As(typeof (ICategoryService)).InstancePerDependency();
            builder.RegisterType(typeof (DepartmentService)).As(typeof (IDepartmentService)).InstancePerDependency();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}