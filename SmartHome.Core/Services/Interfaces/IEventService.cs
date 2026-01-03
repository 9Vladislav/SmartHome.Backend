using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.DTO.Events;

namespace SmartHome.Core.Services.Interfaces;

public interface IEventService
{
    Task HandleDeviceEventAsync(CreateEventDto dto);
}
