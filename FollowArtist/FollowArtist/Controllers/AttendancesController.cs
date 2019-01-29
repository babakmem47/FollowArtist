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

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _context.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
        }

        ////////////// Get //////////////////////
        [HttpGet]
        public IEnumerable<Attendance> Get()
        {
            var attendances = _context.Attendances.ToList();

            return attendances;
        }

        public IEnumerable<AttendanceDto> GetAll()
        {
            var attendances = _context.Attendances
                .Include(a => a.Attendee)
                .Include(a => a.Gig.Genre)
                .Include(a => a.Gig.Artist)
                .ToList();

            return attendances.Select(a => new AttendanceDto
            {
                Attendee = new UserDto
                {
                    Name = a.Attendee.Name
                },
                GigToAttend = new GigDto
                {
                    GigId = a.Gig.Id,
                    DateTime = a.Gig.DateTime,
                    Venue = a.Gig.Venue,
                    Performer = new UserDto
                    {
                        Name = a.Gig.Artist.Name
                    },
                    Genre = new GenreDto
                    {
                        Name = a.Gig.Genre.Name
                    }
                }
            });
        }

        public IEnumerable<AttendanceDto> GetAll(int id)
        {
            var attendances = _context.Attendances
                .Include(a => a.Attendee)
                .Include(a => a.Gig.Genre)
                .Include(a => a.Gig.Artist)
                .Where(a => a.Gig.Id == id)
                .ToList();

            return attendances.Select(a => new AttendanceDto
            {
                Attendee = new UserDto
                {
                    Name = a.Attendee.Name
                },
                GigToAttend = new GigDto
                {
                    GigId = a.Gig.Id,
                    DateTime = a.Gig.DateTime,
                    Venue = a.Gig.Venue,
                    Performer = new UserDto
                    {
                        Name = a.Gig.Artist.Name
                    },
                    Genre = new GenreDto
                    {
                        Name = a.Gig.Genre.Name
                    }
                }
            });
        }


    }
}
