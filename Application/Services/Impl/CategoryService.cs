using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using Application.Dto.Common;
using Application.Services.Def;
using Infrastructure.Entities;
using Infrastructure.Repositories.Def;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Impl;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CategoryService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    } 
    
    public async Task<ApiResponse<object?>> CreateCategoryAsync(string categoryName)
    {
        var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";
        var result = _userManager.FindByNameAsync(userName).Result;

        if (result == null)
            return new ApiResponse<object?>(
                HttpStatusCode.Created,
                "Category created failed"
            );
        var userId = result.Id;
        var category = new Category
        {
            Name = categoryName,
            UserId = userId
        };
        
        await _unitOfWork.CategoryRepository.AddAsync(category);

        return new ApiResponse<object?>(
            HttpStatusCode.Created,
            "Category created successfully"
            );
    }
}