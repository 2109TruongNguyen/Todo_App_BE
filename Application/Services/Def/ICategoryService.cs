using Application.Dto.Common;

namespace Application.Services.Def;

public interface ICategoryService
{
    public Task<ApiResponse<object?>> CreateCategoryAsync(string categoryName);
}