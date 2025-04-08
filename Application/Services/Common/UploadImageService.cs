using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Common;

public class UploadImageService
{
    private const string CLOUD_NAME = "dkzn3xjwt";
    private const string API_KEY = "425313844243574";
    private const string API_SECRET = "qJ2vFs9XOv8gAv-QI8JWVh2SrQw";

    public UploadImageService()
    {
        
    }
    public async Task<string> UploadImageToCloudinary(IFormFile file)
    {
        var account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
        var cloudinary = new Cloudinary(account);

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = Guid.NewGuid().ToString() 
        };

        var uploadResult = await cloudinary.UploadAsync(uploadParams);
        return uploadResult.SecureUrl.AbsoluteUri;
    }
}