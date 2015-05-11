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
        IEnumerable<Genre> GetGenres(int startIndex, int numResults);
    }
    public class GenreRepository : IGenreRepository
    {
        private readonly IMedtelligentMovieDbContext _medtelligentMovieContext;
        public GenreRepository(IMedtelligentMovieDbContext medtelligentMovieContext)
        {
            _medtelligentMovieContext = medtelligentMovieContext;
        }
        public IEnumerable<Genre> GetGenres(int startIndex, int numResults)
        {
            return _medtelligentMovieContext.Genres.OrderBy(g => g.Title)
                //Page the Genres
                .Skip(startIndex).Take(numResults);
            //Pull back the n most recently created.
            //.Include(g => g.Movies.OrderByDescending(m => m.CreatedDate).Take(numTopMovies).Select(m => m));
        }
    }
}
