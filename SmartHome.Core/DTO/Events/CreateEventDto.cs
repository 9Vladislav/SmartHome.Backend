using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.DTO.Events;

public class CreateEventDto
{
    public int SensorId { get; set; }
    public EventType Type { get; set; }
    public DateTime? OccurredAt { get; set; }
}
