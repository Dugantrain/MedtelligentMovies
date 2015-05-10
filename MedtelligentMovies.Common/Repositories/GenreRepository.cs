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
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenresWithTopMovies(int startIndex, int numResults, int numTopMovies);
    }
    public class GenreRepository : IRepository
    {
        private readonly IMedtelligentMovieDbContext _medtelligentMovieContext;
        public GenreRepository(IMedtelligentMovieDbContext medtelligentMovieContext)
        {
            _medtelligentMovieContext = medtelligentMovieContext;
        }
        public IEnumerable<Genre> GetGenresWithTopMovies(int startIndex, int numResults, int numTopMovies)
        {
            return _medtelligentMovieContext.Genres.OrderBy(g => g.Title)
                //Page the Genres
                .Skip(startIndex).Take(numResults)
                //Pull back the n most recently created.
                .Include(g => g.Movies.Select(m => m).OrderByDescending(m => m.CreatedDate).Take(numTopMovies));
        }
    }
}
