using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
    public interface IPlayer
    {
        string Name { get; set; }
        List<Item> Inventory { get; set; }
    }
}
