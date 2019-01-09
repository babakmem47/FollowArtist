using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowArtist.Models
{
    public class Follow
    {
        public ApplicationUser Follower { get; set; }

        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }

        public ApplicationUser Followee { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FolloweeId { get; set; }
    }
}