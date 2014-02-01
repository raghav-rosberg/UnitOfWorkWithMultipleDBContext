using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Service.CategoryService;
using UoW_MultipleDBContext.Service.DepartmentService;
using UoW_MultipleDBContext.Web.API.Mappings;

namespace UoW_MultipleDBContext.Web.API
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
            var configuration = (HttpConfiguration)GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>));
            builder.RegisterType(typeof(CategoryService)).As(typeof(ICategoryService)).InstancePerDependency();
            builder.RegisterType(typeof(DepartmentService)).As(typeof(IDepartmentService)).InstancePerDependency();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}