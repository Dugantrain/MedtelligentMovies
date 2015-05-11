using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.Models;

namespace MedtelligentMovies.Common.Repositories
{
    public interface IMovieRepository : IRepository
    {
        Movie GetMoveById(int id);
        List<IEnumerable<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults);
        IEnumerable<Movie> GetMovies();
    }
    public class MovieRepository : IMovieRepository
    {
        private readonly IMedtelligentMovieDbContext _medtelligentMovieContext;
        public MovieRepository(IMedtelligentMovieDbContext medtelligentMovieContext)
        {
            _medtelligentMovieContext = medtelligentMovieContext;
        }
        public Movie GetMoveById(int id)
        {
            return _medtelligentMovieContext.Movies.Find(id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _medtelligentMovieContext.Movies;
        }

        public List<IEnumerable<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults)
        {
            var ordering = genreIds.Select((id, index) => new { id, index });
            return _medtelligentMovieContext.Movies
                .Where(m => genreIds.Contains(m.Genre.Id))
                .GroupBy(m => m.Genre.Id)
                .Select(group => group.OrderByDescending(mv => mv.ReleaseDate)
                    .Take(topResults)).ToList();

        }
    }
}
