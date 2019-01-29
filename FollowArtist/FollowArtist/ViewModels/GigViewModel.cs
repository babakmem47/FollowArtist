using System.Collections.Generic;
using FollowArtist.Models;

namespace FollowArtist.ViewModels
{
    public class GigViewModel
    {
        public IEnumerable<Gig> Gigs { get; set; }
        public string Heading { get; set; }
    }
}