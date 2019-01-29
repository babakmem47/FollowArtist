namespace FollowArtist.Models
{
    public class UserNotification
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public Gig Gig { get; set; }
        public int GigId { get; set; }

        public bool IsRead { get; private set; }

        public void Read()
        {
            this.IsRead = true;
        }
    }
}