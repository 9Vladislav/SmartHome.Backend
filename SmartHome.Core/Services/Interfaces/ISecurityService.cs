using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;
using SmartHome.Core.DTO.Security;

namespace SmartHome.Core.Services.Interfaces;

public interface ISecurityService
{
    Task<SecurityStateDto> GetMyStateAsync(int userId);
    Task<SecurityStateDto> SetMyModeAsync(int userId, SecurityMode mode);
}
