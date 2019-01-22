using System;

namespace FollowArtist.Dtos
{
    public class GigDto
    {
        public int GigId { get; set; }

        public UserDto Performer { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public GenreDto Genre { get; set; }

    }
}