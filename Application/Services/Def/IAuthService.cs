using System.Runtime.InteropServices.JavaScript;
using Application.Dto.Common;
using Application.Dto.Request;
using Application.Dto.Response;

namespace Application.Services.Def;

public interface IAuthService
{
    public Task<ApiResponse<AuthenticationResponse?>> LoginAsync(AuthenticationRequest authenticationRequest);
    public Task<ApiResponse<Object?>> RegisterAsync(RegisterRequest registerRequest);
    
    public Task<ApiResponse<Dictionary<string, string>>?> RefreshTokenAsync(string refreshToken);
    public Task<ApiResponse<object?>> LogoutAsync();
}