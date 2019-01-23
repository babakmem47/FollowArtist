using System.Data.Entity.ModelConfiguration;
using FollowArtist.Models;

namespace FollowArtist.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(un => new {un.UserId, un.GigId});

            Property(un => un.UserId)
                .IsRequired()
                .HasColumnOrder(1);

            Property(un => un.GigId)
                .IsRequired()
                .HasColumnOrder(2);
            
            HasRequired(un => un.User)
                .WithMany()
                .HasForeignKey(un => un.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(un => un.Gig)
                .WithMany()
                .HasForeignKey(un => un.GigId);
        }
    }
}