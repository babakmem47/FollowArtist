using System.Data.Entity.ModelConfiguration;
using FollowArtist.Models;

namespace FollowArtist.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            HasKey(gn => gn.Id);

            Property(gn => gn.Name)
                .IsRequired()
                .HasMaxLength(40);

            //// Relation ////
            HasMany(gn => gn.Gigs)
                .WithRequired(gg => gg.Genre)
                .WillCascadeOnDelete(false);

        }
    }
}