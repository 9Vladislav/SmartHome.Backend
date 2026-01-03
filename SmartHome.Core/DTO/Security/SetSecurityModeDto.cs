using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.DTO.Security;

public class SetSecurityModeDto
{
    public SecurityMode Mode { get; set; } // ARMED / DISARMED
}
