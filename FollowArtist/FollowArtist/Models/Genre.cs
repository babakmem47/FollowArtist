using System.Collections.Generic;

namespace FollowArtist.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Gig> Gigs { get; set; }

    }
}