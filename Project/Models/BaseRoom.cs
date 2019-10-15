using System;
using System.Collections.Generic;
using static ConsoleAdventure.Types;
using RoomTemplates = System.Collections.Generic.Dictionary<ConsoleAdventure.Types.RoomIdentifier, char[][]>;
using RoomColors = System.Collections.Generic.Dictionary<ConsoleAdventure.Types.RoomIdentifier, System.ConsoleColor>;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Models
{
  public struct DoorCoords
  {
    public int row;
    public int col;

    public DoorCoords(int r, int c)
    {
      row = r;
      col = c;
    }
  }
  public class BaseRoom : Room
  {
    private RoomTemplates _templates { get; set; }
    private RoomColors _colors { get; set; }
    public ConsoleColor Color
    {
      get
      {
        return _colors[Id];
      }
    }
    public char[][] Template
    {
      get
      {
        return _templates[Id];
      }
    }
    public Types.RoomPostion RoomPosition { get; set; }
    public Types.RoomIdentifier Id { get; set; }
    // public string Name { get; set; }
    // public string Description { get; set; }
    // public List<Item> Items { get; set; } = new List<Item>();
    public new Dictionary<string, IRoom> Exits { get; set; }

    public static Dictionary<Direction, DoorCoords> DoorCoordinates = new Dictionary<Direction, DoorCoords>()
    {
      { Direction.North, new DoorCoords(0, 3) },
      { Direction.East, new DoorCoords(1, 6) },
      { Direction.South, new DoorCoords(2, 3) },
      { Direction.West, new DoorCoords(1, 0) }
    };

    public char DoorCharacter(Types.Direction direction)
    {
      DoorCoords doorCoords = DoorCoordinates[direction];
      return Template[doorCoords.row][doorCoords.col];
    }
    public BaseRoom(
      Types.RoomIdentifier roomIdentifier,
      RoomTemplates roomTemplates,
      RoomColors roomColors
    )
    {
      Id = roomIdentifier;
      _templates = roomTemplates;
      _colors = roomColors;
    }
  }
}