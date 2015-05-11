using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                new Genre{Title = "Comedy",Description = "Tee hee."},
                new Genre{Title="Drama",Description="Dramatic stuff."}
            };
            genres.ForEach(g=>medtelligentMovieContext.Genres.Add(g));

            medtelligentMovieContext.SaveChanges();

            //Movies
            var movies = new List<Movie>
            {
                new Movie{Title = "Stardust Memories",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Woody Allen plays Sandy Bates, a film-maker attending a retrospective of his works.",ReleaseDate = new DateTime(1980,9,26)},
                new Movie{Title = "Ghostbusters",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Madcap undead antics in New York.",ReleaseDate = new DateTime(1984,6,1)},
                new Movie{Title = "Dr. Strangelove",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Something about a bomb.",ReleaseDate = new DateTime(1964,1,29)},
                new Movie{Title = "a",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Something about a bomb.",ReleaseDate = new DateTime(1954,1,29)},
                new Movie{Title = "b",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Something about a bomb.",ReleaseDate = new DateTime(1922,1,29)},
                new Movie{Title = "c",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Something about a bomb.",ReleaseDate = new DateTime(1933,1,29)},


                new Movie{Title = "1",Genre = genres.FirstOrDefault(g=>g.Title == "Drama"),Description="1",ReleaseDate = new DateTime(1980,9,26)},
                new Movie{Title = "2",Genre = genres.FirstOrDefault(g=>g.Title == "Drama"),Description="2",ReleaseDate = new DateTime(1970,9,26)},
                new Movie{Title = "3",Genre = genres.FirstOrDefault(g=>g.Title == "Drama"),Description="3",ReleaseDate = new DateTime(1960,9,26)},
                new Movie{Title = "4",Genre = genres.FirstOrDefault(g=>g.Title == "Drama"),Description="4",ReleaseDate = new DateTime(1950,9,26)},
                new Movie{Title = "5",Genre = genres.FirstOrDefault(g=>g.Title == "Drama"),Description="5",ReleaseDate = new DateTime(1940,9,26)},
                new Movie{Title = "6",Genre = genres.FirstOrDefault(g=>g.Title == "Drama"),Description="6",ReleaseDate = new DateTime(1930,9,26)},
                new Movie{Title = "7",Genre = genres.FirstOrDefault(g=>g.Title == "Drama"),Description="7",ReleaseDate = new DateTime(1920,9,26)}
            };
            movies.ForEach(m=>medtelligentMovieContext.Movies.Add(m));
            medtelligentMovieContext.SaveChanges();
        }
    }
}
