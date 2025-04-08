using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Configuration;

public static class ApiConfig
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var googleSettings = configuration.GetSection("Google");
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["AccessSecretKey"]!))
                };
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = googleSettings["ClientId"]!;
                googleOptions.ClientSecret = googleSettings["ClientSecret"]!;
                googleOptions.CallbackPath = "/api/auth/google-response";
                googleOptions.Scope.Add("profile"); 
                googleOptions.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
            });

        services.AddAuthorization();
    }
}