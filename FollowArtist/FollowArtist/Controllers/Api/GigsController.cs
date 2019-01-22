using FollowArtist.Dtos;
using FollowArtist.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace FollowArtist.Controllers.Api
{
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<GigDto> GetAllGigs()
        {
            var gigs = _context.Gigs
                .Include(gg => gg.Atrist)
                .Include(gg => gg.Genre)
                .ToList();

            return gigs.Select(g => new GigDto()
            {
                Performer = new UserDto
                {
                    Name = g.Atrist.Name
                },
                Genre = new GenreDto
                {
                    Name = g.Genre.Name
                },
                DateTime = g.DateTime,
                Venue = g.Venue
            });
        }
    }
}
