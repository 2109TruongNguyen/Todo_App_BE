using Infrastructure.Enums;

namespace Infrastructure.Entities;

public class Member
{
    public Guid UserId { get; set; }
    public RoleInTeam Role { get; set; }
}