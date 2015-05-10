using System;
using System.Collections.Generic;
using System.Linq;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.Models;

namespace MedtelligentMovies.Common.DAL.Initializers
{
    public class MedtelligentMovieDbInitializer : 
        System.Data.Entity.CreateDatabaseIfNotExists<MedtelligentMovieDbContext>
    {
        protected override void Seed(MedtelligentMovieDbContext medtelligentMovieContext)
        {
            //Users
            var users = new List<User>
            {
                new User{
                        FirstName = "Chester",
                        LastName = "Administrator",
                        UserName = "MedMoviesAdmin",
                        EncryptedPassword = "TODO:  Figure this out.",
                        Email = "Admin@medtelligentmovies.com",
                        IsAdministrator = true
                        }
            };
            users.ForEach(u=>medtelligentMovieContext.Users.Add(u));
            medtelligentMovieContext.SaveChanges();

            //Genres
            var genres = new List<Genre>
            {
                new Genre{Title = "Comedy",Description = "Tee hee."}
            };
            genres.ForEach(g=>medtelligentMovieContext.Genres.Add(g));

            medtelligentMovieContext.SaveChanges();

            //Movies
            var movies = new List<Movie>
            {
                new Movie{Title = "Stardust Memories",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Woody Allen plays Sandy Bates, a film-maker attending a retrospective of his works.",ReleaseDate = new DateTime(1980,9,26)},
                new Movie{Title = "Ghostbusters",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Madcap undead antics in New York.",ReleaseDate = new DateTime(1984,6,1)}
            };
            movies.ForEach(m=>medtelligentMovieContext.Movies.Add(m));
            medtelligentMovieContext.SaveChanges();

            medtelligentMovieContext.Dispose();
        }
    }
}
