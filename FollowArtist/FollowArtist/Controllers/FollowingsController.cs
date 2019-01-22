using FollowArtist.Dtos;
using FollowArtist.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace FollowArtist.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult AddFollower(FollowingDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            var alreadyExist = _context.Followings.Any(fl => fl.FolloweeId == dto.ArtistId && fl.FollowerId == currentUserId);
            if (alreadyExist)
            {
                return BadRequest("Follower already exist!");
            }

            var following = new Following
            {
                FolloweeId = dto.ArtistId,
                FollowerId = currentUserId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IEnumerable<FollowingDto> GetAllFollowings()
        {
            var allFollows = _context.Followings.Include(f => f.Follower).ToList();

            return allFollows.Select(f => new FollowingDto()
            {
                Artist = new UserDto
                {
                    Id = f.Followee.Id,
                    Name = f.Followee.Name
                },
                Follower = new UserDto()
                {
                    Id = f.Follower.Id,
                    Name = f.Follower.Name
                }
                
            });
        }
    }
}
