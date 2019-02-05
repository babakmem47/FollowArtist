using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FollowArtist.Models;
using Microsoft.AspNet.Identity;

namespace FollowArtist.Controllers.Api
{
    [Authorize]
    public class UserNotificationsController : ApiController
    {
        private ApplicationDbContext _context;
        
        public UserNotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public int GetUserNotifications()
        {
            var currentLoggedInUserId = User.Identity.GetUserId();
            var count = _context.UserNotifications
                .Count(un => un.UserId == currentLoggedInUserId);

            return count;
;        }
    }
}
