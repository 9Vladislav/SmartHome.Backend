using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.DTO.Incidents;

public class IncidentDto
{
    public int IncidentId { get; set; }
    public int EventId { get; set; }

    public IncidentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public string SensorName { get; set; } = null!;
    public string RoomName { get; set; } = null!;
}
