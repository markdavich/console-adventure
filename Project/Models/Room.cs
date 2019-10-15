using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public Dictionary<string, IRoom> Exits { get; set; }

    public Item TakeItem(string name)
    {
      Item result = Items.Find(item => item.Name.ToLower() == name.ToLower());

      if (result != null)
      {
        if (result.IsTakeable) {
          Items.Remove(result);
        }
      }

      return result;
    }
  }
}


