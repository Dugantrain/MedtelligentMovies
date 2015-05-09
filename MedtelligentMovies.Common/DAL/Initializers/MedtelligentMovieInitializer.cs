using System.Collections.Generic;
using System.Linq;
using MedtelligentMovies.Common.DAL.DbContexts;
using Models;

namespace MedtelligentMovies.Common.DAL.Initializers
{
    public class MedtelligentMovieInitializer : System.Data.Entity.CreateDatabaseIfNotExists<MedtelligentMovieDbContext>
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
                new Genre{Title = "Comedy",Description = "Funny Stuff.  Tee hee."}
            };
            genres.ForEach(g=>medtelligentMovieContext.Genres.Add(g));

            medtelligentMovieContext.SaveChanges();

            //Movies
            var movies = new List<Movie>
            {
                new Movie{Title = "Stardust Memories",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Woody Allen plays Sandy Bates, a film-maker attending a retrospective of his works."},
                new Movie{Title = "Ghostbusters",Genre = genres.FirstOrDefault(g=>g.Title == "Comedy"),Description="Madcap undead antics in New York.  Some sticky shit, man."}
            };
            movies.ForEach(m=>medtelligentMovieContext.Movies.Add(m));
            medtelligentMovieContext.SaveChanges();
        }
    }
}
