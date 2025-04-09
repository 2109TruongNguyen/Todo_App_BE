using System.Net;
using System.Security.Claims;
using Application.Dto.Common;
using Application.Dto.Request;
using Application.Services.Def;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controller;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    // Authenticate user
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        if (!ModelState.IsValid)
        {
            return Ok(new ApiResponse<ModelStateDictionary>(
                HttpStatusCode.BadRequest,
                "Invalid request",
                ModelState
            ));
        }
        
        return Ok( await _authService.LoginAsync(authenticationRequest));
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
        {
            return Ok(new ApiResponse<ModelStateDictionary>(
                HttpStatusCode.BadRequest,
                "Invalid request",
                ModelState
            ));
        }

        return Ok( await _authService.RegisterAsync(registerRequest));
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        return Ok(await _authService.RefreshTokenAsync(refreshToken));
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok(await _authService.LogoutAsync());
    }
    
    // Authenticate Google user
    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(GoogleCallback), "Auth", null, Request.Scheme)
        };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
    
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
        {
            return Ok(new ApiResponse<object?>(
                HttpStatusCode.Unauthorized,
                "Google authentication failed",
                null
            ));
        }

        var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            
        foreach (var claim in claims)
        {
            Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
        }

        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var givenName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
        var familyName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
        var profilePicture = claims?.FirstOrDefault(c => c.Type == "urn:google:picture")?.Value;
        var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return Ok(new ApiResponse<object?>(
                HttpStatusCode.BadRequest,
                "Cannot get email from Google",
                null
            ));
        }

        var googleUser = new
        {
            GoogleId = googleId,
            Email = email,
            FullName = name,
            FirstName = givenName,
            LastName = familyName,
            ProfilePicture = profilePicture
        };
        return Ok(new ApiResponse<object?>(
            HttpStatusCode.OK,
            "Login Google successful",
            googleUser
            ));
    }
}
