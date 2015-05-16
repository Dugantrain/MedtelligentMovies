using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.Models;

namespace MedtelligentMovies.Common.Repositories
{
    public interface IUserRepository : IRepository
    {
        User AddUser(User user);
        User UpdateUser(User user);
        User GetUserById(int userId);
        User GetUserByUsername(string userName);
        void DeleteUser(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IMedtelligentMovieDbContext _medtelligentMovieContext;
        public UserRepository(IMedtelligentMovieDbContext medtelligentMovieContext)
        {
            _medtelligentMovieContext = medtelligentMovieContext;
        }
        public User AddUser(User user)
        {
            _medtelligentMovieContext.Users.Add(user);
            _medtelligentMovieContext.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            _medtelligentMovieContext.Users.Attach(user);
            _medtelligentMovieContext.SaveChanges();
            return user;
        }

        public User GetUserById(int userId)
        {
            return _medtelligentMovieContext.Users.Find(userId);
        }

        public User GetUserByUsername(string userName)
        {
            return _medtelligentMovieContext.Users.SingleOrDefault(u => u.UserName.ToLower() == userName);
        }

        public void DeleteUser(int userId)
        {
            var userToDelete = _medtelligentMovieContext.Users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete == null) return;
            _medtelligentMovieContext.Users.Remove(userToDelete);
            _medtelligentMovieContext.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return _medtelligentMovieContext.Users.SingleOrDefault(u => u.Email.ToLower() == email);
        }
    }
}
