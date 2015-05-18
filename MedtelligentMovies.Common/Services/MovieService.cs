using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Repositories;

namespace MedtelligentMovies.Common.Services
{
    public interface IMovieService : IService
    {
        Movie GetMovieById(int movieId);
        Dictionary<int, List<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults);
        IEnumerable<Movie> GetMoviesByGenreId(int genreId);
        Movie Create(Movie movie);
        Movie ChangeTitle(int movieId, string currentTitle);
        Movie ChangeDescription(int movieId, string currentDescription);
        void DeleteMovie(int movieId);
        IEnumerable<Movie> GetMoviesBySearchText(string searchText);
        IList<Movie> GetMovies(int startIndex, int numResults);
        IList<Movie> GetMoviesWithGenre(int startIndex, int numResults);
    }
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }
        public Movie GetMovieById(int movieId)
        {
            return _movieRepository.GetMovieById(movieId);
        }

        public Dictionary<int, List<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults)
        {
            return _movieRepository.GetTopMoviesByGenreIds(genreIds, topResults);
        }

        public IEnumerable<Movie> GetMoviesByGenreId(int genreId)
        {
            return _movieRepository.GetMoviesByGenreId(genreId);
        }

        public Movie Create(Movie movie)
        {
            var genreId = movie.GenreId;
            var genre = _genreRepository.GetGenreById(genreId);
            movie = _movieRepository.AddMovie(movie);
            var numMovies = GetMoviesByGenreId(genreId).Count();
            genre.NumMovies = numMovies;
            _genreRepository.UpdateGenre(genre);
            return movie;
        }

        public Movie ChangeTitle(int movieId, string currentTitle)
        {
            var movie = _movieRepository.GetMovieById(movieId);
            movie.Title = currentTitle;
            _movieRepository.UpdateMovie(movie);
            return movie;
        }

        public Movie ChangeDescription(int movieId, string currentDescription)
        {
            var movie = _movieRepository.GetMovieById(movieId);
            movie.Description = currentDescription;
            _movieRepository.UpdateMovie(movie);
            return movie;
        }

        public void DeleteMovie(int movieId)
        {
            var movie = GetMovieById(movieId);
            _movieRepository.DeleteMovie(movieId);
            if (movie != null)
            {
                var genre = _genreRepository.GetGenreById(movie.GenreId);
                genre.NumMovies = GetMoviesByGenreId(genre.Id).Count();
                _genreRepository.UpdateGenre(genre);
            }
        }

        public IEnumerable<Movie> GetMoviesBySearchText(string searchText)
        {
            return _movieRepository.GetMoviesBySearchText(searchText);
        }

        public IList<Movie> GetMovies(int startIndex, int numResults)
        {
            return _movieRepository.GetMovies(startIndex, numResults).ToList();
        }

        public IList<Movie> GetMoviesWithGenre(int startIndex, int numResults)
        {
            return _movieRepository.GetMoviesWithGenre(startIndex, numResults).ToList();
        }
    }
}
