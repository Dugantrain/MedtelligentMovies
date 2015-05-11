using System.Linq;
using MedtelligentMovies.Common.Repositories;
using Microsoft.Practices.Unity;

namespace MedtelligentMovies.Web.Configuration
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
            var genres = container.Resolve<IGenreRepository>().GetGenres(0,5);
            var genreIds = genres.Select(g => g.Id).ToArray();
            var movieRepository = container.Resolve<IMovieRepository>();
            var moviesByGenre = movieRepository.GetTopMoviesByGenreIds(genreIds, 5);
            return container;
        }
    }
}