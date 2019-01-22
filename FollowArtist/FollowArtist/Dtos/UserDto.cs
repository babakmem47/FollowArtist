using Newtonsoft.Json;

namespace FollowArtist.Dtos
{
    public class UserDto
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}