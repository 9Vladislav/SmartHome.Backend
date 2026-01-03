using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Security;

namespace SmartHome.Core.Mapping;

public static class SecurityStateMapping
{
    public static SecurityStateDto ToDto(this SecurityState state)
    {
        return new SecurityStateDto
        {
            Mode = state.Mode,
            UpdatedAt = state.UpdatedAt
        };
    }
}
