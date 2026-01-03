using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.DTO.Admin;

public class AdminOverviewStatsDto
{
    public int UsersTotal { get; set; }
    public int UsersActive { get; set; }
    public int UsersBlocked { get; set; }

    public int SensorsTotal { get; set; }
    public int SensorsEnabled { get; set; }
    public int SensorsDisabled { get; set; }

    public int IncidentsOpen { get; set; }
    public int IncidentsLast24h { get; set; }
    public int EventsLast24h { get; set; }
}
