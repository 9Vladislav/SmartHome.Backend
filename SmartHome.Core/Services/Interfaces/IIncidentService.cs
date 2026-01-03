using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.DTO.Incidents;

namespace SmartHome.Core.Services.Interfaces;

public interface IIncidentService
{
    Task<List<IncidentDto>> GetMyIncidentsAsync(int userId);
    Task<IncidentDto?> GetMyIncidentByIdAsync(int userId, int incidentId);
    Task<bool> ResolveIncidentAsync(int userId, int incidentId);
}
