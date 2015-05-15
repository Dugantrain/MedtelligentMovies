using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Repositories;

namespace MedtelligentMovies.Common.Services
{
    public interface IGenreService : IService
    {
        Genre GetGenreById(int genreId);
        IList<Genre> GetGenres(int startIndex, int numResults);
        Genre Create(Genre genre);
        Genre ChangeTitle(int genreId, string title);
        Genre ChangeDescription(int genreId, string description);
        void DeleteGenre(int genreId);
    }
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieService _movieService;
        public GenreService(IGenreRepository genreRepository, IMovieService movieService)
        {
            _genreRepository = genreRepository;
            _movieService = movieService;
        }

        public Genre GetGenreById(int genreId)
        {
            return _genreRepository.GetGenreById(genreId);
        }

        public IList<Genre> GetGenres(int startIndex, int numResults)
        {
            return _genreRepository.GetGenres(startIndex, numResults).ToList();
        }

        public Genre Create(Genre genre)
        {
            return _genreRepository.AddGenre(genre);
        }

        //This is where the services layer shines:  The commands as they are exposed to the web layer
        //convey meaning.
        public Genre ChangeTitle(int genreId, string currentTitle)
        {
            var genre = _genreRepository.GetGenreById(genreId);
            genre.Title = currentTitle;
            _genreRepository.UpdateGenre(genre);
            return genre;
        }

        public Genre ChangeDescription(int genreId, string currentDescription)
        {
            var genre = _genreRepository.GetGenreById(genreId);
            genre.Description = currentDescription;
            _genreRepository.UpdateGenre(genre);
            return genre;
        }

        public void DeleteGenre(int genreId)
        {
            //Delete all movies within the genre.
            var moviesToDelete = _movieService.GetMoviesByGenreId(genreId);
            foreach (var movieToDelete in moviesToDelete)
            {
                _movieService.DeleteMovie(movieToDelete.Id);
            }
            //Delete the genre.
            _genreRepository.DeleteGenre(genreId);
        }
    }
}
