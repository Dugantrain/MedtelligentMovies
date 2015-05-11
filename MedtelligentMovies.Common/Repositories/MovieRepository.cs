using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.Models;

namespace MedtelligentMovies.Common.Repositories
{
    public interface IMovieRepository : IRepository
    {
        Movie GetMovieById(int id);
        List<IEnumerable<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults);
        Movie AddMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        void DeleteMovie(int movieId);
    }
    public class MovieRepository : IMovieRepository
    {
        private readonly IMedtelligentMovieDbContext _medtelligentMovieContext;
        public MovieRepository(IMedtelligentMovieDbContext medtelligentMovieContext)
        {
            _medtelligentMovieContext = medtelligentMovieContext;
        }
        public Movie GetMovieById(int id)
        {
            return _medtelligentMovieContext.Movies.Find(id);
        }

        public List<IEnumerable<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults)
        {
            var ordering = genreIds.Select((id, index) => new {id, index});
            return _medtelligentMovieContext.Movies
                .Where(m => genreIds.Contains(m.Genre.Id))
                .GroupBy(m => m.Genre.Id)
                .Select(group => group.OrderByDescending(mv => mv.ReleaseDate)
                    .Take(topResults)).ToList();
        }

        public Movie AddMovie(Movie movie)
        {
            _medtelligentMovieContext.Movies.Add(movie);
            _medtelligentMovieContext.SaveChanges();
            return movie;
        }

        public Movie UpdateMovie(Movie movie)
        {
            var originalMovie = _medtelligentMovieContext.Movies.Find(movie.Id);
            if (originalMovie != null)
            {
                originalMovie = movie;
                _medtelligentMovieContext.SaveChanges();
            }
            return movie;
        }

        public void DeleteMovie(int movieId)
        {
            var originalMovie = _medtelligentMovieContext.Movies.SingleOrDefault(m=>m.Id == movieId);
            if (originalMovie == null) return;
            _medtelligentMovieContext.Movies.Remove(originalMovie);
            _medtelligentMovieContext.SaveChanges();
        }
    }
}
