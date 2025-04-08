using System.Net;

namespace Application.Dto.Common;

public class ApiResponse<T>(HttpStatusCode status, string message, T? objects = default, List<string>? errors = null)
{
    public HttpStatusCode Status { get; set; } = status;
    public string Message { get; set; } = message;
    public T? Objects { get; set; } = objects;
    public List<string>? Errors { get; set; } = errors;
}
