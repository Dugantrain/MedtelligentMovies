﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Repositories;

namespace MedtelligentMovies.Common.Services
{
    public interface IUserService : IService
    {
        User Create(User user);
        User ChangeFirstName(int userId, string currentFirstName);
        User ChangeLastName(int userId, string currentLastName);
        User ChangePassword(int userId, string currentEncryptedPassword);
        User ChangeEmail(int userId, string currentEmail);
        User ChangeUserName(int userId, string currentUserName);
        User GetUserById(int userId);
        User GetUserByUsername(string userName);
        User GetUserByEmail(string email);
        IEnumerable<User> GetUsers(int startIndex, int numResults); 
        void DeleteUser(int userId);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public User Create(User user)
        {
            return _userRepository.AddUser(user);
        }

        public User ChangeFirstName(int userId, string currentFirstName)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.FirstName = currentFirstName;
                _userRepository.UpdateUser(user);
            }
            return user;
        }

        public User ChangeLastName(int userId, string currentLastName)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.LastName = currentLastName;
                _userRepository.UpdateUser(user);
            }
            return user;
        }

        public User ChangePassword(int userId, string currentEncryptedPassword)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.EncryptedPassword = currentEncryptedPassword;
                _userRepository.UpdateUser(user);
            }
            return user;
        }

        public User ChangeEmail(int userId, string currentEmail)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.Email = currentEmail;
                _userRepository.UpdateUser(user);
            }
            return user;
        }

        public User ChangeUserName(int userId, string currentUserName)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.UserName = currentUserName;
                _userRepository.UpdateUser(user);
            }
            return user;
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public User GetUserByUsername(string userName)
        {
            return _userRepository.GetUserByUsername(userName);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public IEnumerable<User> GetUsers(int startIndex, int numResults)
        {
            return _userRepository.GetUsers(startIndex, numResults);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }
    }
}
