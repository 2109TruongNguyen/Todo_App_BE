using Application.Dto.Request;
using Application.Services.Def;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Route("api/user")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    
    [HttpPut("update-avatar")]
    public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarRequest updateAvatar)
    {
        return Ok(await _userService.UpdateAvatarAsync(updateAvatar.Avatar));
    }
    
    [HttpGet("get-information")]
    public async Task<IActionResult> UpdateAvatar()
    {
        return Ok(await _userService.GetInformation());
    }
}