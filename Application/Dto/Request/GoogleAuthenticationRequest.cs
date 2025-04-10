using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto.Request;

public class GoogleAuthenticationRequest
{
    public string? Email { get; set; } = string.Empty;
    
    public string? FirstName { get; set; } = string.Empty;
    
    public string? LastName { get; set; } = string.Empty;
    
    public string? AvatarUrl {get; set; } = string.Empty;
}