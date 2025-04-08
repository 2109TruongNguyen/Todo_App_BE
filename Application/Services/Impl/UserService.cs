using System.Net;
using System.Security.Claims;
using Application.Dto.Common;
using Application.Dto.Response;
using Application.Services.Common;
using Application.Services.Def;
using AutoMapper;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Impl;

public class UserService : IUserService
{
    private readonly UploadImageService _uploadImageService;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    
    public UserService(UploadImageService uploadImageService, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _uploadImageService = uploadImageService;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
    
    public async Task<ApiResponse<object?>> UpdateAvatarAsync(IFormFile file)
    {
        var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";
        var user = _userManager.FindByNameAsync(userName).Result;


        if (user == null)
            return new ApiResponse<object?>(
                HttpStatusCode.OK,
                "Avatar updated Failed"
                );
        
        user.AvatarUrl = await _uploadImageService.UploadImageToCloudinary(file);

        await _userManager.UpdateAsync(user);

        return new ApiResponse<object?>(
            HttpStatusCode.OK,
            "Avatar updated successfully"
        );
    }

    public Task<ApiResponse<UserResponse?>> GetInformation()
    {
        var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";
        var user =  _userManager.FindByNameAsync(userName).Result;
        
        return Task.FromResult(new ApiResponse<UserResponse?>(
            HttpStatusCode.OK,
            "Avatar updated successfully",
            _mapper.Map<UserResponse>(user)
        ));
    }
}