using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.Models;

namespace MedtelligentMovies.Common.Repositories
{
    public interface IGenreRepository : IRepository
    {
        Genre GetGenreById(int genreId);
        IEnumerable<Genre> GetGenres(int startIndex, int numResults);
        Genre AddGenre(Genre genre);
        Genre UpdateGenre(Genre genre);
        void DeleteGenre(int genreId);
    }
    public class GenreRepository : IGenreRepository
    {
        private readonly IMedtelligentMovieDbContext _medtelligentMovieContext;
        public GenreRepository(IMedtelligentMovieDbContext medtelligentMovieContext)
        {
            _medtelligentMovieContext = medtelligentMovieContext;
        }

        public Genre GetGenreById(int genreId)
        {
            return _medtelligentMovieContext.Genres.Find(genreId);
        }

        public IEnumerable<Genre> GetGenres(int startIndex, int numResults)
        {
            return _medtelligentMovieContext.Genres.OrderBy(g => g.Title)
                //Page the Genres
                .Skip(startIndex).Take(numResults);
            //Pull back the n most recently created.
            //.Include(g => g.Movies.OrderByDescending(m => m.CreatedDate).Take(numTopMovies).Select(m => m));
        }

        public Genre AddGenre(Genre genre)
        {
            _medtelligentMovieContext.Genres.Add(genre);
            _medtelligentMovieContext.SaveChanges();
            return genre;
        }

        public Genre UpdateGenre(Genre genre)
        {
            var originalGenre = _medtelligentMovieContext.Genres.Find(genre.Id);
            if (originalGenre == null) return genre;
            originalGenre = genre;
            _medtelligentMovieContext.SaveChanges();
            return genre;
        }

        public void DeleteGenre(int genreId)
        {
            var originalGenre = _medtelligentMovieContext.Genres.SingleOrDefault(g => g.Id == genreId);
            if (originalGenre == null) return;
            _medtelligentMovieContext.Genres.Remove(originalGenre);
            _medtelligentMovieContext.SaveChanges();
        }
    }
}
