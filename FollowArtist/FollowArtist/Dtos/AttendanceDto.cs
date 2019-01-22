
using Newtonsoft.Json;

namespace FollowArtist.Dtos
{
    public class AttendanceDto
    {
        [JsonIgnore]
        public int GigId { get; set; }

        public UserDto Attendee { get; set; }

        public GigDto GigToAttend { get; set; }

    }
}