using System.CodeDom;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using MedtelligentMovies.Common.Models;

namespace MedtelligentMovies.Common.DAL.DbContexts
{
    public interface IMedtelligentMovieDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
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

        //Intercepts all saved assets, checks for IAuditable, and populates the IAuditable properties.
        public new int SaveChanges()
        {
            var createdAssets = ChangeTracker.Entries<IAuditable>().Where(e => e.State == EntityState.Added);
            foreach (var entity in createdAssets.Select(createdAsset => createdAsset.Entity))
            {
                entity.CreatedDate = System.DateTime.UtcNow;
                entity.LastUpdatedDate = System.DateTime.UtcNow;
            }
            var modifiedAssets = ChangeTracker.Entries<IAuditable>().Where(e => e.State == EntityState.Modified);
            foreach (var entity in modifiedAssets.Select(modifiedAsset => modifiedAsset.Entity))
            {
                entity.LastUpdatedDate = System.DateTime.UtcNow;
            }
            return base.SaveChanges();
        }
    }
}