using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.DTO.Sensors;

public class UpdateSensorDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
