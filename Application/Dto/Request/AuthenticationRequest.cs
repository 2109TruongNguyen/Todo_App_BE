using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto.Request;

public class AuthenticationRequest
{
    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}