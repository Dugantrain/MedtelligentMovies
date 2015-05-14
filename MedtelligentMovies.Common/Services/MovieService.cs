using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Repositories;

namespace MedtelligentMovies.Common.Services
{
    public interface IMovieService //: IService
    {
        Movie GetMovieById(int movieId);
        Dictionary<int, List<Movie>> GetTopMoviesByGenreIds(int[] genreIds, int topResults);
        IEnumerable<Movie> GetMoviesByGenreId(int genreId);
        Movie Create(Movie movie);
        Movie ChangeTitle(int movieId, string currentTitle);
        Movie ChangeDescription(int movieId, string currentDescription);
        Movie ChangeReleaseDate(int movieId, DateTime currentReleaseDate);
        void DeleteMovie(int movieId);
        IEnumerable<Movie> GetMoviesBySearchText(string searchText);
    }
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
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
            return _movieRepository.AddMovie(movie);
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

        public Movie ChangeReleaseDate(int movieId, DateTime currentReleaseDate)
        {
            var movie = _movieRepository.GetMovieById(movieId);
            movie.ReleaseDate = currentReleaseDate;
            _movieRepository.UpdateMovie(movie);
            return movie;
        }

        public void DeleteMovie(int movieId)
        {
            _movieRepository.DeleteMovie(movieId);
        }

        public IEnumerable<Movie> GetMoviesBySearchText(string searchText)
        {
            return _movieRepository.GetMoviesBySearchText(searchText);
        }
    }
}
