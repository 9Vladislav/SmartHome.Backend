using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.DTO.Rooms;

public class UpdateRoomDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
