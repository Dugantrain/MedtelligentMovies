using System.ComponentModel;
using System.Linq;
using MedtelligentMovies.Common.Repositories;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace MedtelligentMovies.Web.Configuration
{
    public class Bootstrapper
    {
        //Sets up Unity as our DI and provides a layer of abstraction between
        //it and ServiceLocator.
        public static void ConfigureServiceLocator()
        {
            var container = new UnityContainer();
                container.RegisterTypes(
                   AllClasses.FromLoadedAssemblies(),
                   WithMappings.FromMatchingInterface,
                   WithName.Default,
                   WithLifetime.Hierarchical);
            var unityServiceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }
    }
}