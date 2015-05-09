using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Models;

namespace MedtelligentMovies.Common.DAL.DbContexts
{
    /// <summary>
    /// Summary description for MedtelligentMovieContext
    /// </summary>
    public class MedtelligentMovieContext : DbContext
    {
        public MedtelligentMovieContext()
            : base("MainConnectionString")
        {
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