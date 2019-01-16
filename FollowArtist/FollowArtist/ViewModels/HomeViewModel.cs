using FollowArtist.Models;
using System.Collections.Generic;

namespace FollowArtist.ViewModels
{

    public class HomeViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
    }
}