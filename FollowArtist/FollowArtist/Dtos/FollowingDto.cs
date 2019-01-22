
namespace FollowArtist.Dtos
{
    public class FollowingDto
    {
        public UserDto Follower { get; set; }

        public string FollowerId { get; set; }

        public UserDto Artist { get; set; }

        public string ArtistId { get; set; }
    }
}