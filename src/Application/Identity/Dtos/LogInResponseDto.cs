using System.Text.Json.Serialization;

namespace DevToDev.Application.Identity.Dtos
{
    public class LogInResponseDto
    {
        public string AccessToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}