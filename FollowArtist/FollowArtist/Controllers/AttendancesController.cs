using FollowArtist.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using FollowArtist.Dtos;

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
        public IHttpActionResult AddNewAttendance(AttendanceDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            var alreadyExist = _context.Attendances.Any(a => a.AttendeeId == currentUserId && a.GigId == dto.GigId);
            if (alreadyExist)
            {
                return BadRequest("The attendance already exists.");
            }
            var attendance = new Attendance
            {
                AttendeeId = currentUserId,
                GigId = dto.GigId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

        public IHttpActionResult GetAllAttendances()
        {
            var attendances = _context.Attendances.ToList();

            return Ok(attendances);
        }
    }
}
