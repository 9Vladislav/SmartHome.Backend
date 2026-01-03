using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.DTO.Sensors;

public class SensorDto
{
    public int SensorId { get; set; }
    public int RoomId { get; set; }
    public string RoomName { get; set; } = null!;

    public SensorType Type { get; set; } // PIR / CONTACT
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public bool IsEnabled { get; set; }
    public DateTime CreatedAt { get; set; }
}
