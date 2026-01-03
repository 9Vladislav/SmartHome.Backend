using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.DTO.Admin;

public class SetUserStatusDto
{
    public UserStatus Status { get; set; }
}
