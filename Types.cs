using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;



namespace ConsoleAdventure
{
  public static class Types
  {
    public enum Direction { North, East, South, West, None }
    public enum RoomPostion { One, Two, Three, Four }
    public enum Rotation { Clockwise, CounterClockwise, None }
    public enum FlipDirection { Vertical, Horizontal, None }
    public enum RoomIdentifier { A, B, C, D, None };

    public enum ColorState { CMYK, RGBK, RYBW };

    public enum Level { One, Two, Three, Four, Five }

    public enum DoorState { Open, Locked, Sealed , None}

    public const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Black;
    public const ConsoleColor WALL_AND_DOOR_COLOR = ConsoleColor.DarkGray;
    public const ConsoleColor ROOM_COLOR = ConsoleColor.Black;
  }
}