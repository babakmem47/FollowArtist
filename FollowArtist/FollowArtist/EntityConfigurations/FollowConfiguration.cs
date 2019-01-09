using System.Data.Entity.ModelConfiguration;
using FollowArtist.Models;

namespace FollowArtist.EntityConfigurations
{
    public class FollowConfiguration : EntityTypeConfiguration<Follow>
    {
        public FollowConfiguration()
        {
//            HasKey(f => f.FolloweeId).HasKey(f => f.FollowerId);
            HasRequired(aa => aa.Followee)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasRequired(f => f.Follower)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}