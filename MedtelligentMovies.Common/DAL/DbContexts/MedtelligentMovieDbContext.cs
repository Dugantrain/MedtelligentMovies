using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Models;

namespace MedtelligentMovies.Common.DAL.DbContexts
{
    public interface IMedtelligentMovieDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<User> Users { get; set; }
    }
    /// <summary>
    /// Summary description for MedtelligentMovieContext
    /// </summary>
    public class MedtelligentMovieDbContext : DbContext, IMedtelligentMovieDbContext
    {
        public MedtelligentMovieDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MedtelligentMovieDbContext>());
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}