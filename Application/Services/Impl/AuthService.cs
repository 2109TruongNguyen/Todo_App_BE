using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Application.Constants;
using Application.Dto.Common;
using Application.Dto.Request;
using Application.Dto.Response;
using Application.Services.Def;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Impl;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    
    public AuthService(UserManager<User> userManager, IJwtService jwtService, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _signInManager = signInManager;
    }
    
    public async Task<ApiResponse<AuthenticationResponse?>> LoginAsync(AuthenticationRequest authenticationRequest)
    {
        var user = await _userManager.FindByNameAsync(authenticationRequest.UserName);
        if (user == null || !await _userManager.CheckPasswordAsync( user, authenticationRequest.Password))
        {
            return new ApiResponse<AuthenticationResponse?>(
                HttpStatusCode.BadRequest,
                MessageConstant.AuthMessage.LOGIN_INVALID_REQUEST,
                null
            );
        }
        
        var authenticationResponse = new AuthenticationResponse()
        {
            accessToken = _jwtService.GenerateAccessToken(user),
            refreshToken = _jwtService.GenerateRefreshToken(user),
        };
        
        return new ApiResponse<AuthenticationResponse?>(
            HttpStatusCode.OK,
            MessageConstant.AuthMessage.LOGIN_SUCCESS,
            authenticationResponse
        );
    }

    public async Task<ApiResponse<Object?>> RegisterAsync(RegisterRequest registerRequest)
    {
        var user = new User()
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            FirstName = registerRequest.FirstName.Trim(),
            LastName = registerRequest.LastName.Trim(),
            FullName = registerRequest.LastName + " " + registerRequest.FirstName,
            NickName = registerRequest.NickName,
            AvatarUrl = MessageConstant.AuthMessage.DEFAULT_AVATAR_URL
        };
        
        var existingUser = await _userManager.FindByNameAsync(registerRequest.UserName);
        if (existingUser != null)
        {
            return new ApiResponse<object?>(
                HttpStatusCode.BadRequest,
                MessageConstant.AuthMessage.REGISTER_USERNAME_EXISTS,
                null
            );
        }
        
        var result = await _userManager.CreateAsync(user, registerRequest.PasswordHash);
        if (result.Succeeded)
        {
            return new ApiResponse<object?>(
                HttpStatusCode.Created,
                MessageConstant.AuthMessage.REGISTER_SUCCESS,
                null
                );
        }

        return new ApiResponse<object?>(
            HttpStatusCode.BadRequest,
            MessageConstant.AuthMessage.REGISTER_FAIL,
            null
        );;
    }

    public async Task<ApiResponse<Dictionary<string, string>>?> RefreshTokenAsync(string refreshToken)
    {
        var userName = _jwtService.ValidateRefreshToken(refreshToken);
        if (string.IsNullOrEmpty(userName))
        {
            return new ApiResponse<Dictionary<string, string>>(
                HttpStatusCode.BadRequest,
                MessageConstant.AuthMessage.REFRESH_INVALID_TOKEN,
                null
            );
        }
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return new ApiResponse<Dictionary<string, string>>(
                HttpStatusCode.NotFound,
                MessageConstant.AuthMessage.REFRESH_USER_NOT_FOUND,
                null
            );
        }
        return new ApiResponse<Dictionary<string, string>>(
            HttpStatusCode.OK,
            MessageConstant.AuthMessage.REFRESH_SUCCESS,
            new Dictionary<string, string> { { "accessToken", _jwtService.GenerateAccessToken(user) } }
        );;
    }

    public async Task<ApiResponse<object?>> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return new ApiResponse<object?>(
            HttpStatusCode.OK,
            "Logout successful",
            null
        );
    }
}