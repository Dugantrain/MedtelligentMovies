using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Common.DAL.Initializers
{
    public interface IMedtelligentMovieDbInitializer
    {
        void Seed(MedtelligentMovieDbContext medtelligentMovieContext);
    }
    public class MedtelligentMovieDbInitializer : 
        System.Data.Entity.CreateDatabaseIfNotExists<MedtelligentMovieDbContext>
    {
        private readonly IUserService _userService;
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;

        public MedtelligentMovieDbInitializer(IUserService userService,IGenreService genreService, IMovieService movieService)
        {
            _userService = userService;
            _genreService = genreService;
            _movieService = movieService;
        }
        protected override void Seed(MedtelligentMovieDbContext medtelligentMovieContext)
        {
            //Users
            var users = new List<User>
            {
                new User{
                        FirstName = "Chester",
                        LastName = "Administrator",
                        UserName = "MedMoviesAdmin",
                        EncryptedPassword = "FBqHWZ3qHqsAx4UYbUUE0RWn6s4JKal1GbYT0ad+nr4=",
                        Email = "Admin@medtelligentmovies.com",
                        IsAdministrator = true
                        }
            };
            users.ForEach(u=>_userService.Create(u));

            //Genres
            var comedyGenre = new Genre{Title = "Comedy", Description = "Tee hee."};
            var dramaGenre = new Genre { Title = "Drama", Description = "Dramatic stuff." };
            comedyGenre = _genreService.Create(comedyGenre);
            dramaGenre = _genreService.Create(dramaGenre);

            //Movies
            var comedyMovies = new List<Movie>
            {
                new Movie
                {
                    Title = "Stardust Memories",
                    Genre = comedyGenre,
                    Description = "Woody Allen plays Sandy Bates, a film-maker attending a retrospective of his works.",
                    ReleaseDate = new DateTime(1980, 9, 26)
                },
                new Movie
                {
                    Title = "Ghostbusters",
                    Genre = comedyGenre,
                    Description = "Madcap undead antics in New York.",
                    ReleaseDate = new DateTime(1984, 6, 1)
                },
                new Movie
                {
                    Title = "Dr. Strangelove",
                    Genre = comedyGenre,
                    Description = "Something about a bomb.",
                    ReleaseDate = new DateTime(1964, 1, 29)
                },
                new Movie
                {
                    Title = "Bill & Ted's Excellent Adventure",
                    Genre = comedyGenre,
                    Description = "Folks crowd into a phone booth.",
                    ReleaseDate = new DateTime(1989, 7, 14)
                },
                new Movie
                {
                    Title = "b",
                    Genre = comedyGenre,
                    Description = "Something about a bomb.",
                    ReleaseDate = new DateTime(1922, 1, 29)
                },
                new Movie
                {
                    Title = "c",
                    Genre = comedyGenre,
                    Description = "Something about a bomb.",
                    ReleaseDate = new DateTime(1933, 1, 29)
                }
            };
            comedyMovies.ForEach(m =>_movieService.Create(m));
           var dramaMovies = new List<Movie>
           {    new Movie{Title = "1",Genre = dramaGenre,Description="1",ReleaseDate = new DateTime(1980,9,26)},
                new Movie{Title = "2",Genre = dramaGenre,Description="2",ReleaseDate = new DateTime(1970,9,26)},
                new Movie{Title = "3",Genre = dramaGenre,Description="3",ReleaseDate = new DateTime(1960,9,26)},
                new Movie{Title = "4",Genre = dramaGenre,Description="4",ReleaseDate = new DateTime(1950,9,26)},
                new Movie{Title = "5",Genre = dramaGenre,Description="5",ReleaseDate = new DateTime(1940,9,26)},
                new Movie{Title = "6",Genre = dramaGenre,Description="6",ReleaseDate = new DateTime(1930,9,26)},
                new Movie{Title = "7",Genre = dramaGenre,Description="7",ReleaseDate = new DateTime(1920,9,26)}
            };
           dramaMovies.ForEach(m =>_movieService.Create(m));
        }
    }
}
