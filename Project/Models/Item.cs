using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
    public class Item : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}