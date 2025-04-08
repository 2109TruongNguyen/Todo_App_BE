using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Entities;

[CollectionName("Roles")]
public class Role : MongoIdentityRole<Guid>
{
    
}