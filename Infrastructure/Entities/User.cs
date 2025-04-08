using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Entities;

[CollectionName("Users")]
public class User : MongoIdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public ICollection<string> TaskListIds { get; set; } = [];
    public ICollection<string> TeamIds { get; set; } = [];
    public string AvatarUrl { get; set; } = string.Empty;
}