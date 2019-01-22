using System.Data.Entity;
using System.Collections.Generic;
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
                .Include(a => a.Gig.Atrist)
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
                        Name = a.Gig.Atrist.Name
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
                .Include(a => a.Gig.Atrist)
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
                        Name = a.Gig.Atrist.Name
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
