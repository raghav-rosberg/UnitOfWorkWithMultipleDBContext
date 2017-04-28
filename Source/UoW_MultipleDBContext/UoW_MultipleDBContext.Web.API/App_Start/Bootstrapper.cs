using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Data.UnitOfWork;

namespace UoW_MultipleDBContext.Web.API
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacWebAPI();
        }

        private static void SetAutofacWebAPI()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>));
            IContainer container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}