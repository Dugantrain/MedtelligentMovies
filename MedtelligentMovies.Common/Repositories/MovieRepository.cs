using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.Models;

namespace MedtelligentMovies.Common.Repositories
{
    public interface IMovieRepository //: IRepository
    {
        Movie GetMovieById(int movieId);
        Dictionary<int, List<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults);
        IEnumerable<Movie> GetMoviesByGenreId(int genreId);
        Movie AddMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        void DeleteMovie(int movieId);
        IEnumerable<Movie> GetMoviesBySearchText(string searchText);
    }
    public class MovieRepository : IMovieRepository
    {
        private readonly IMedtelligentMovieDbContext _medtelligentMovieContext;
        public MovieRepository(IMedtelligentMovieDbContext medtelligentMovieContext)
        {
            _medtelligentMovieContext = medtelligentMovieContext;
        }
        public Movie GetMovieById(int movieId)
        {
            return _medtelligentMovieContext.Movies.Find(movieId);
        }

        //This will send one SELECT to the server w/out performing an N+1 for each genre.
        //EF officially rocks my face.
        public Dictionary<int,List<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults)
        {
            return _medtelligentMovieContext.Movies
                .Where(m => genreIds.Contains(m.Genre.Id))
                .GroupBy(m => m.Genre.Id)
                .Select(group => new{genreId = group.Key,movies = group.OrderByDescending(mv => mv.ReleaseDate)
                    .Take(topResults)}).ToDictionary(k=>k.genreId,v=>v.movies.ToList());
        }

        public IEnumerable<Movie> GetMoviesByGenreId(int genreId)
        {
            return _medtelligentMovieContext.Movies.Where(m => m.Genre.Id == genreId);
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
            if (originalMovie == null) return movie;
            originalMovie = movie;
            _medtelligentMovieContext.SaveChanges();
            return movie;
        }

        public void DeleteMovie(int movieId)
        {
            var originalMovie = _medtelligentMovieContext.Movies.SingleOrDefault(m=>m.Id == movieId);
            if (originalMovie == null) return;
            _medtelligentMovieContext.Movies.Remove(originalMovie);
            _medtelligentMovieContext.SaveChanges();
        }

        public IEnumerable<Movie> GetMoviesBySearchText(string searchText)
        {
            //This would perform horribly if we were dealing with any amount of data that mattered.
            //Generally, we'd want to move this off into a read store like Eclipse or Mongo.
            return _medtelligentMovieContext.Movies.Where(m => m.Title.ToLower().StartsWith(searchText.ToLower()));
        }
    }
}
