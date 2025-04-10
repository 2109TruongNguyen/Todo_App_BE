using System.Net;
using System.Security.Claims;
using Application.Dto.Common;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Services.Def;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
        // var scheme = Request.Host.Host.Contains("localhost") ? "http" : "https";
        //
        // var properties = new AuthenticationProperties
        // {
        //     RedirectUri = Url.Action(nameof(GoogleCallback), "Auth", null, scheme)
        // };
        
        var scheme = Request.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? Request.Scheme;

        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(GoogleCallback), "Auth", null, scheme)
        };

        
        Console.WriteLine(properties.RedirectUri);
        
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
        
        if (claims != null)
        {
            var enumerable = claims as Claim[] ?? claims.ToArray();

            var email = enumerable?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = enumerable?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var givenName = enumerable?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var familyName = enumerable?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            var profilePicture = enumerable?.FirstOrDefault(c => c.Type == "urn:google:picture")?.Value;
            
            var userLogin = new GoogleAuthenticationRequest()
            {
                Email = email,
                AvatarUrl = profilePicture,
                FirstName = givenName,
                LastName = familyName
            };
            var authenticationResponse = await _authService.GoogleLoginAsync(userLogin);
            return Redirect(GetUrlGoogleCallBack(authenticationResponse));
        }
        
        return Redirect(GetUrlGoogleCallBack(null));
    }

    private string GetUrlGoogleCallBack(AuthenticationResponse? response)
    {
        var frontendUrl = Request.Host.Host.Contains("localhost") ? "http://localhost:3000" : "https://todo-app-fe-y2al.onrender.com";
        if (response == null)
        {
            return $"{frontendUrl}/google-handler?error=invalid_request";
        }
        var url = $"{frontendUrl}/google-handler?access={response.accessToken}&refresh={response.refreshToken}";
        return url;
    }

}
