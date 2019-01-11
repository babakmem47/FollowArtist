using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowArtist.Models
{
    public class Attendance
    {
        public ApplicationUser Attendee { get; set; }

        [Key]
        [Column(Order = 1)]
        public string AttendeeId { get; set; }
        
        public Gig Gig { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GigId { get; set; }

    }
}