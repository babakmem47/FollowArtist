using System;

namespace FollowArtist.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public int GenreId { get; set; }

        public bool IsCanceled { get; set; }
    }
}