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
        public static void ConfigureUnityContainer()
        {
            var container = new UnityContainer();
                container.RegisterTypes(
                   AllClasses.FromLoadedAssemblies(),
                   WithMappings.FromMatchingInterface,
                   WithName.Default,
                   WithLifetime.ContainerControlled);
            var unityServiceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
            var genres = ServiceLocator.Current.GetInstance<IGenreRepository>().GetGenres(0, 5);
            //var genres = container.Resolve<IGenreRepository>().GetGenres(0,5);
            var genreIds = genres.Select(g => g.Id).ToArray();
            var movieRepository = container.Resolve<IMovieRepository>();
            var moviesByGenre = movieRepository.GetTopMoviesByGenreIds(genreIds, 5);
        }
    }
}