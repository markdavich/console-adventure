using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

using RoomPosition = ConsoleAdventure.Types.RoomPosition;
using RoomIdentifier = ConsoleAdventure.Types.RoomIdentifier;
using static ConsoleAdventure.Types;

namespace ConsoleAdventure.Project.Interfaces
{
    public interface IRoom
    {
        string Name { get; set; }
        string Description { get; set; }
        List<Item> Items { get; set; }
        Dictionary<string, IRoom> Exits { get; set; }
    }
}
