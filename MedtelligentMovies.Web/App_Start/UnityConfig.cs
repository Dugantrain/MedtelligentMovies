using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace MedtelligentMovies.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}