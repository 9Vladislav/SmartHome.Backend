using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.DTO.Security;

public class SecurityStateDto
{
    public SecurityMode Mode { get; set; }
    public DateTime UpdatedAt { get; set; }
}
