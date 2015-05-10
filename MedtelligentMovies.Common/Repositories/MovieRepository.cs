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
    }
}
