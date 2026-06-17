using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.DTO.Profile;

namespace SmartHome.Core.Services.Interfaces;

public interface IProfileService
{
    Task<ProfileDto> GetMyProfileAsync(int userId);
    Task<ProfileDto> UpdateMyProfileAsync(int userId, UpdateProfileDto dto);
}