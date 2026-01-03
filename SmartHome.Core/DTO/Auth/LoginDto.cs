using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.DTO.Auth;

public class LoginDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
