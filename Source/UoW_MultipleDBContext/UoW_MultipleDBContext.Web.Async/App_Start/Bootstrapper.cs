using System.Reflection;
using LightInject;
using UoW_MultipleDBContext.Web.Core;

namespace UoW_MultipleDBContext.Web.Async
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetLightInjectContainer();
            //Configure AutoMapper
        }

        private static void SetLightInjectContainer()
        {
            var container = new ServiceContainer();
            container.RegisterControllers(Assembly.GetExecutingAssembly());
            container.Register<IApiPath, ApiPath>(new PerScopeLifetime());
            container.Register<IApiHelper, ApiHelper>(new PerScopeLifetime());
            container.EnableMvc();
        }
    }
}