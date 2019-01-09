using System.Data.Entity;
using FollowArtist.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FollowArtist.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FollowConfiguration());
            modelBuilder.Configurations.Add(new GigConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}