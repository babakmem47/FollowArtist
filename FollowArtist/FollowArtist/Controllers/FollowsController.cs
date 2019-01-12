using FollowArtist.Dtos;
using FollowArtist.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace FollowArtist.Controllers
{
    [Authorize]
    public class FollowsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult AddFollower(FollowDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            var alreadyExist = _context.Follows.Any(fl => fl.FolloweeId == dto.ArtistId && fl.FollowerId == currentUserId);
            if (alreadyExist)
            {
                return BadRequest("Follower already exist!");
            }

            var newFollow = new Follow
            {
                FolloweeId = dto.ArtistId,
                FollowerId = currentUserId
            };

            _context.Follows.Add(newFollow);
            _context.SaveChanges();

            return Ok();
        }

        public IHttpActionResult GetAllFollows()
        {
            var allFollows = _context.Follows.ToList();

            return Ok(allFollows);
        }
    }
}
