using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Admin;

namespace SmartHome.Core.Mapping;

public static class AdminUserMapping
{
    public static AdminUserDto ToAdminDto(this User u)
    {
        return new AdminUserDto
        {
            UserId = u.UserId,
            FirstName = u.FirstName,
            LastName = u.LastName,
            PhoneNumber = u.PhoneNumber,
            Email = u.Email,
            Role = u.Role,
            Status = u.Status,
            CreatedAt = u.CreatedAt
        };
    }
}
