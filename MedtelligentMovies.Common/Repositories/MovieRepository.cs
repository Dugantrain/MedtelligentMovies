using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MedtelligentMovies.Common.DAL.DbContexts;
using Models;

namespace MedtelligentMovies.Common.Repositories
{
    public interface IMovieRepository : IRepository
    {
        Movie GetMoveById(int id);
        IEnumerable<Movie> GetMoviesByGenreId(int genreId);
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

        //TODO:  Paging
        public IEnumerable<Movie> GetMoviesByGenreId(int genreId)
        {
            return _medtelligentMovieContext.Movies.Where(m => m.Genre.Id == genreId);
        }
    }
}
