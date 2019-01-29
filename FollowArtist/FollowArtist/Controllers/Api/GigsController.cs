using System;
using FollowArtist.Dtos;
using FollowArtist.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace FollowArtist.Controllers.Api
{
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Single(g => g.Id == id && g.ArtistId == currentUserId);

            if (gig.IsCanceled)
            {
                return NotFound();
            }
            
            gig.IsCanceled = true;
            
            var notification = new Notification
            {
                GigId = gig.Id,
                Type = NotificationType.IsCanceled,
                DateTime = DateTime.Now
            };

            _context.Notifications.Add(notification);


            var users = _context.Attendances
                .Include(at => at.Gig)
                .Where(at => at.GigId == id).ToList();
            

            
            foreach (var user in users)
            {
                var usernotification = new UserNotification
                {
                    UserId = user.AttendeeId,
                    GigId = user.GigId
                };
                _context.UserNotifications.Add(usernotification);
               
            }

            _context.SaveChanges();

            return Ok();
        }
        
        [HttpGet]
        public IEnumerable<GigDto> GetAll()
        {
            var gigs = _context.Gigs
                .Include(gg => gg.Artist)
                .Include(gg => gg.Genre)
                .ToList();

            return gigs.Select(g => new GigDto()
            {
                GigId = g.Id,
                Performer = new UserDto
                {
                    Name = g.Artist.Name
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
