using Application.Services.Def;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;


[Route("api/categories")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpPost("create/{categoryName}")]
    public async Task<IActionResult> CreateCategory(string categoryName)
    {
        return Ok(await _categoryService.CreateCategoryAsync(categoryName));
    }
}