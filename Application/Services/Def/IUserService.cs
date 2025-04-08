using Application.Dto.Common;
using Application.Dto.Response;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Def;

public interface IUserService
{
    Task<ApiResponse<object?>> UpdateAvatarAsync(IFormFile file);
    
    Task<ApiResponse<UserResponse?>> GetInformation();
}