using FollowArtist.Models;

namespace FollowArtist.ViewModels
{
    public class GigDetailViewModel
    {
        public Gig Gig { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsAttending { get; set; }
        public string ButtonType { get; set; }
        public string ButtonText { get; set; }
    }
}