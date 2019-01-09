using System.Data.Entity.ModelConfiguration;
using FollowArtist.Models;

namespace FollowArtist.EntityConfigurations
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            HasKey(gg => gg.Id);

            Property(gg => gg.DateTime)
                .IsRequired();

            Property(gg => gg.Venue)
                .IsRequired()
                .HasMaxLength(200);

            //// Relatin ////
            HasRequired(gg => gg.Genre)
                .WithMany(gn => gn.Gigs)
                .HasForeignKey(gg => gg.GenreId);

            HasRequired(gg => gg.Atrist)
                .WithMany()
                .HasForeignKey(gg => gg.ArtistId)
                .WillCascadeOnDelete(false);
        }        
    }
}