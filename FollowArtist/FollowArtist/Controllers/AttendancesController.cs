using FollowArtist.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace FollowArtist.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult AddNewAttendance([FromBody] int gigId)
        {
            var currentUserId = User.Identity.GetUserId();
            var attendance = new Attendance
            {
                AttendeeId = currentUserId,
                GigId = gigId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok(attendance);
        }

        public IHttpActionResult GetAllAttendances()
        {
            var attendances = _context.Attendances.ToList();

            return Ok(attendances);
        }
    }
}
