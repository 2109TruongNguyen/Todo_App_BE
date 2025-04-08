using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto.Request;

public class RegisterRequest
{
    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
    [JsonPropertyName("password")]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [JsonPropertyName("nickName")]
    public string NickName { get; set; } = string.Empty;
}