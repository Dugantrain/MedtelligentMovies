using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.Extensions;
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
            var user = new User{
                        FirstName = "Chester",
                        LastName = "Administrator",
                        UserName = "MedMoviesAdmin",
                        EncryptedPassword = "M3dAdm1n".Encrypt(),
                        Email = "Admin@medtelligentmovies.com",
                        IsAdministrator = true
                        };

            _userService.Create(user);

            //Genres
            var comedyGenre = new Genre{Title = "Comedy", Description = "Tee hee."};
            var actionGenre = new Genre { Title = "Action", Description = "Actionable stuff." };
            comedyGenre = _genreService.Create(comedyGenre);
            actionGenre = _genreService.Create(actionGenre);

            //Movies
            var comedyMovies = new List<Movie>
            {
                new Movie
                {
                    Title = "Stardust Memories",
                    GenreId = comedyGenre.Id,
                    Description = "Woody Allen plays Sandy Bates, a film-maker attending a retrospective of his works.",
                },
                new Movie
                {
                    Title = "Ghostbusters",
                    GenreId = comedyGenre.Id,
                    Description = "Madcap undead antics in New York.",
                },
                new Movie
                {
                    Title = "Dr. Strangelove",
                    GenreId = comedyGenre.Id,
                    Description = "Something about a bomb.",
                },
                new Movie
                {
                    Title = "Bill & Ted's Excellent Adventure",
                    GenreId = comedyGenre.Id,
                    Description = "Folks crowd into a phone booth.",
                },
                new Movie
                {
                    Title = "Amazon Women on the Moon",
                    GenreId = comedyGenre.Id,
                    Description = "Amazon Women.  Not on Mars.",
                },
                new Movie
                {
                    Title = "Leonard Part 6",
                    GenreId = comedyGenre.Id,
                    Description = "Bill Cosby does it again!",
                }
            };
            comedyMovies.ForEach(m =>_movieService.Create(m));
           var actionMovies = new List<Movie>
           {    new Movie{Title = "Commando",GenreId = actionGenre.Id,Description="Arnold is back."},
                new Movie{Title = "Star Wars",GenreId = actionGenre.Id,Description="It's Star Wars."},
                new Movie{Title = "The Empire Strikes Back",GenreId = actionGenre.Id,Description="All Han."},
                new Movie{Title = "Life of Pi",GenreId = actionGenre.Id,Description=""},
                new Movie{Title = "Die Hard",GenreId = actionGenre.Id,Description=""},
                new Movie{Title = "Pulp Fiction",GenreId = actionGenre.Id,Description=""},
                new Movie{Title = "Kill Bill",GenreId = actionGenre.Id,Description=""}
            };
           actionMovies.ForEach(m =>_movieService.Create(m));
        }
    }
}
