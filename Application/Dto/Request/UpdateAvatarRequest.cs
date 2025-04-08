using Microsoft.AspNetCore.Http;

namespace Application.Dto.Request;

public class UpdateAvatarRequest
{
    public IFormFile Avatar { get; set; }
}