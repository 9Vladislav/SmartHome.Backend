using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.DTO.Events;

public class EventDto
{
    public int EventId { get; set; }
    public int SensorId { get; set; }

    public EventType Type { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public string SensorName { get; set; } = null!;
    public string RoomName { get; set; } = null!;

    public int? IncidentId { get; set; }
}
