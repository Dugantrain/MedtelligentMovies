using MedtelligentMovies.Common.Repositories;
using Microsoft.Practices.Unity;

namespace MedtelligentMovies.Web.App_Data.Configuration
{
    public class Bootstrapper
    {
        public static UnityContainer ConfigureUnityContainer()
        {
            var container = new UnityContainer();
                container.RegisterTypes(
                   AllClasses.FromLoadedAssemblies(),
                   WithMappings.FromMatchingInterface,
                   WithName.Default,
                   WithLifetime.ContainerControlled);
            var movie = container.Resolve<IMovieRepository>().GetMoveById(1);
            return container;
        }
    }
}