
using FollowArtist.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace FollowArtist.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult MarkNotificationsAsRead(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var allNotificationForCurrentUser = _context.UserNotifications
                .Where(un => un.UserId == currentUserId && !un.IsRead)
                .ToList();

            allNotificationForCurrentUser.ForEach(n => n.Read());
            //foreach (var userNotification in allNotificationForCurrentUser)
            //{
            //    userNotification.IsRead = true;
            //}

            _context.SaveChanges();

            return Ok();
        }
    }
}
