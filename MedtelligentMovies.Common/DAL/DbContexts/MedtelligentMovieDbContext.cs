using System;
using System.CodeDom;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Services;

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
        private readonly IUserContextService _userContextService;
        public MedtelligentMovieDbContext(IUserContextService userContextService)
            : base("DefaultConnection")
        {
            _userContextService = userContextService;
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
            var userId = 0;
            var userName = _userContextService.GetUserName();
            if (userName != null)
            {
                var user = Users.SingleOrDefault(u => u.UserName == userName);
                if (user != null) userId = user.Id;
            }
            var createdAssets = ChangeTracker.Entries<IAuditable>().Where(e => e.State == EntityState.Added);
            foreach (var entity in createdAssets.Select(createdAsset => createdAsset.Entity))
            {
                entity.CreatedDate = System.DateTime.UtcNow;
                entity.LastUpdatedDate = System.DateTime.UtcNow;
                entity.CreatedByUserId = userId;
                entity.LastUpdatedByUserId = userId;
            }
            var modifiedAssets = ChangeTracker.Entries<IAuditable>().Where(e => e.State == EntityState.Modified);
            foreach (var entity in modifiedAssets.Select(modifiedAsset => modifiedAsset.Entity))
            {
                entity.LastUpdatedDate = System.DateTime.UtcNow;
                entity.LastUpdatedByUserId = userId;
            }
            return base.SaveChanges();
        }
    }
}