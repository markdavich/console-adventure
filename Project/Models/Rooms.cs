using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

// using RoomPositions = System.Collections.Generic.Dictionary<ConsoleAdventure.Types.RoomPostion, ConsoleAdventure.Project.Interfaces.BaseRoom>;
using Direction = ConsoleAdventure.Types.Direction;
using Rotation = ConsoleAdventure.Types.Rotation;
using FlipDirection = ConsoleAdventure.Types.FlipDirection;
using RoomPosition = ConsoleAdventure.Types.RoomPostion;
// using ConsoleParamsDelegate = System.Func<System.Char, System.Int32, System.Int32, ConsoleAdventure.Types.RoomPostion, ConsoleAdventure.Models.ConsoleParams>;
using RoomIdentifier = ConsoleAdventure.Types.RoomIdentifier;
using RoomTemplates = System.Collections.Generic.Dictionary<ConsoleAdventure.Types.RoomIdentifier, char[][]>;
using RoomColors = System.Collections.Generic.Dictionary<ConsoleAdventure.Types.RoomIdentifier, System.ConsoleColor>;

using PrintInstruction = System.Collections.Generic.List<ConsoleAdventure.Models.ConsoleParams>;
using PrintInstructions = System.Collections.Generic.List<System.Collections.Generic.List<ConsoleAdventure.Models.ConsoleParams>>;
using DoorState = ConsoleAdventure.Types.DoorState;

using System;
using ConsoleAdventure.Project.Models;

delegate ConsoleAdventure.Models.ConsoleParams ConsoleParamsDelegate(char c, int row, int col, RoomPosition pos);


namespace ConsoleAdventure.Models
{
  public class ColorPair
  {
    public ConsoleColor A { get; private set; }
    public ConsoleColor B { get; private set; }

    public int HashCode
    {
      get
      {
        return A.GetHashCode() ^ B.GetHashCode();
      }
    }
    public ColorPair(ConsoleColor a, ConsoleColor b)
    {
      A = a;
      B = b;
    }
  }

  public class ColorPairComparer : IEqualityComparer<ColorPair>
  {
    public bool Equals(ColorPair a, ColorPair b)
    {
      if (a == null && b == null)
        return true;
      else if (a == null || b == null)
        return false;
      else if ((a.A == b.A && a.B == b.B) || (a.A == b.B && a.B == b.A))
        return true;
      else
        return false;
    }

    public int GetHashCode(ColorPair c)
    {
      int hCode = (int)c.A ^ (int)c.B;
      int result = hCode.GetHashCode();
      return result;

      // return c.GetHashCode();
    }
  }

  public class MoveResults
  {
    public bool Success { get; set; } = false;
    public PrintInstructions PrintInstructions { get; set; } = new PrintInstructions();

    public DoorState DoorState { get; set; } = DoorState.Sealed;
  }

  public struct ConsoleParams
  {
    public string Text;
    public ConsoleColor Foreground, Background;

    public ConsoleParams(string text, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = Types.BACKGROUND_COLOR)
    {
      Text = text;
      Foreground = foreground;
      Background = background;
    }
  }

  public struct RoomInfo
  {
    public char Character;
    public string Name;

    public RoomInfo(char c, string name)
    {
      Character = c;
      Name = name;
    }
  }

  public class Rooms
  {
    // ┏━HHH━┳━HHH━┓
    // V bBb v cCc V
    // ┣━hhh━╋━hhh━┫
    // V aAa v dDd V
    // ┗━HHH━┻━HHH━┛


    // public static char[][] RoomTemplate = new char[][]
    // {
    //   "┏━═══━┳━═══━┓".ToCharArray(),
    //   "║  B  ╎  C  ║".ToCharArray(),
    //   "┣━╌╌╌━╋━╌╌╌━┫".ToCharArray(),
    //   "║  A  ╎  D  ║".ToCharArray(),
    //   "┗━═══━┻━═══━┛".ToCharArray()
    // };

    public static char[][] RoomATemplate = new char[][]
    {
      "..╌╌╌..".ToCharArray(),
      "║  A  ╎".ToCharArray(),
      "..═══..".ToCharArray()
    };

    public static char[][] RoomBTemplate = new char[][]
    {
      "..═══..".ToCharArray(),
      "║  B  ╎".ToCharArray(),
      "..╌╌╌..".ToCharArray()
    };

    public static char[][] RoomCTemplate = new char[][]
    {
      "..═══..".ToCharArray(),
      "╎  C  ║".ToCharArray(),
      "..╌╌╌..".ToCharArray()
    };

    public static char[][] RoomDTemplate = new char[][]
    {
      "..╌╌╌..".ToCharArray(),
      "╎  D  ║".ToCharArray(),
      "..═══..".ToCharArray()
    };

    public static RoomTemplates RoomTemplates = new RoomTemplates()
    {
      { RoomIdentifier.A, RoomATemplate },
      { RoomIdentifier.B, RoomBTemplate },
      { RoomIdentifier.C, RoomCTemplate },
      { RoomIdentifier.D, RoomDTemplate }
    };

    public Rotation GetRotation(RoomPosition position, Direction direction)
    {
      switch (position)
      {
        case RoomPosition.One:
          switch (direction)
          {
            case Direction.South:
              return Rotation.Clockwise;
            case Direction.East:
              return Rotation.CounterClockwise;
          }
          break;
        case RoomPosition.Two:
          switch (direction)
          {
            case Direction.South:
              return Rotation.CounterClockwise;
            case Direction.West:
              return Rotation.Clockwise;
          }
          break;
        case RoomPosition.Three:
          switch (direction)
          {
            case Direction.North:
              return Rotation.CounterClockwise;
            case Direction.East:
              return Rotation.Clockwise;
          }
          break;
        case RoomPosition.Four:
          switch (direction)
          {
            case Direction.North:
              return Rotation.Clockwise;
            case Direction.West:
              return Rotation.CounterClockwise;
          }
          break;
      }
      return Rotation.None;
    }

    public bool RotateRooms(RoomPosition position, Direction direction)
    {
      Dictionary<RoomPosition, BaseRoom> result = new Dictionary<RoomPosition, BaseRoom>();
      Rotation rotation = GetRotation(position, direction);
      switch (rotation)
      {
        case Rotation.Clockwise:
          result.Add(RoomPosition.One, RoomPositions[RoomPosition.Three]);
          result.Add(RoomPosition.Two, RoomPositions[RoomPosition.One]);
          result.Add(RoomPosition.Three, RoomPositions[RoomPosition.Four]);
          result.Add(RoomPosition.Four, RoomPositions[RoomPosition.Two]);
          break;
        case Rotation.CounterClockwise:
          result.Add(RoomPosition.One, RoomPositions[RoomPosition.Two]);
          result.Add(RoomPosition.Two, RoomPositions[RoomPosition.Four]);
          result.Add(RoomPosition.Three, RoomPositions[RoomPosition.One]);
          result.Add(RoomPosition.Four, RoomPositions[RoomPosition.Three]);
          break;
        case Rotation.None:
          return false; // Get out of here if we arent rotating.
      }
      RoomPositions = result;
      return true;
    }

    public FlipDirection GetFlipDirection(Direction direction)
    {
      switch (direction)
      {
        case Direction.East:
        case Direction.West:
          return FlipDirection.Horizontal;
        case Direction.North:
        case Direction.South:
          return FlipDirection.Vertical;
      }
      return FlipDirection.None;
    }

    public bool FlipRooms(Direction direction)
    {
      Dictionary<RoomPosition, BaseRoom> result = new Dictionary<RoomPosition, BaseRoom>();
      FlipDirection flipDirection = GetFlipDirection(direction);
      switch (flipDirection)
      {
        case FlipDirection.Horizontal:
          result.Add(RoomPosition.One, RoomPositions[RoomPosition.Four]);
          result.Add(RoomPosition.Two, RoomPositions[RoomPosition.Three]);
          result.Add(RoomPosition.Three, RoomPositions[RoomPosition.Two]);
          result.Add(RoomPosition.Four, RoomPositions[RoomPosition.One]);
          break;
        case FlipDirection.Vertical:
          result.Add(RoomPosition.One, RoomPositions[RoomPosition.Two]);
          result.Add(RoomPosition.Two, RoomPositions[RoomPosition.One]);
          result.Add(RoomPosition.Three, RoomPositions[RoomPosition.Four]);
          result.Add(RoomPosition.Four, RoomPositions[RoomPosition.Three]);
          break;
        default:
          return false;
      }
      RoomPositions = result;
      return true;
    }

    public HashSet<char> ColoredDoorChars = new HashSet<char>() { '═', '║' };

    public char RoomChar(BaseRoom room, int row, int col)
    {
      return room.Template[row][col];
    }

    public Types.Level Level { get; set; } = Types.Level.One;

    public RoomPosition CurrentRoomPosition { get; set; } = RoomPosition.Three;

    public DoorState DoorLockState(RoomPosition position, Direction direction)
    {
      return Types.DoorState.Open;
    }

    public DoorState DoorState(Direction direction)
    {
      DoorState state = Types.DoorState.Sealed;

      switch (CurrentRoomPosition)
      {
        case RoomPosition.One:
          switch (direction)
          {
            case Direction.South:
            case Direction.East:
              state = DoorLockState(CurrentRoomPosition, direction);
              break;
          }
          break;
        case RoomPosition.Two:
          switch (direction)
          {
            case Direction.South:
            case Direction.West:
              state = DoorLockState(CurrentRoomPosition, direction);
              break;
          }
          break;
        case RoomPosition.Three:
          switch (direction)
          {
            case Direction.North:
            case Direction.East:
              state = DoorLockState(CurrentRoomPosition, direction);
              break;
          }
          break;
        case RoomPosition.Four:
          switch (direction)
          {
            case Direction.North:
            case Direction.West:
              state = DoorLockState(CurrentRoomPosition, direction);
              break;
          }
          break;
      }

      return state;
    }
    public MoveResults MoveResults(Direction direction)
    {
      MoveResults result = new MoveResults();

      result.PrintInstructions.Add(new List<ConsoleParams>());
      result.Success = false;
      result.DoorState = DoorState(direction);

      switch (result.DoorState)
      {
        case Types.DoorState.Locked:
        case Types.DoorState.Sealed:
          break;
        case Types.DoorState.Open:
          result.Success = true;
          break;
      }

      result.PrintInstructions[0].Add(new ConsoleParams("The "));
      result.PrintInstructions[0].Add(new ConsoleParams($"{direction}", GetDoorColor(CurrentRoomPosition, direction)));

      if (result.Success)
      {
        result.PrintInstructions[0].Add(new ConsoleParams($" door was {result.DoorState}"));
      }
      else
      {
        result.PrintInstructions[0].Add(new ConsoleParams($" door is {result.DoorState}"));
      }

      return result;
    }

    public RoomPosition GetAdjoiningRoomPosition(RoomPosition currentPosition, Direction direction)
    {
      RoomPosition result = currentPosition;

      switch (currentPosition)
      {
        case RoomPosition.One:
          switch (direction)
          {
            case Direction.South:
              return RoomPosition.Three;
            case Direction.East:
              return RoomPosition.Two;
          }
          break;
        case RoomPosition.Two:
          switch (direction)
          {
            case Direction.South:
              return RoomPosition.Four;
            case Direction.West:
              return RoomPosition.One;
          }
          break;
        case RoomPosition.Three:
          switch (direction)
          {
            case Direction.North:
              return RoomPosition.One;
            case Direction.East:
              return RoomPosition.Four;
          }
          break;
        case RoomPosition.Four:
          switch (direction)
          {
            case Direction.North:
              return RoomPosition.Two;
            case Direction.West:
              return RoomPosition.Three;
          }
          break;
      }

      return result;
    }

    public BaseRoom CurrentRoom
    {
      get
      {
        return RoomPositions[CurrentRoomPosition];
      }
    }

    public void AddCards()
    {
      foreach (BaseRoom room in RoomPositions.Values)
      {
        if (room.Id != RoomIdentifier.B)
        {
          room.Items.Add(new Card(room.Color));
        }
      }
    }

    public PrintInstructions GetRoomDescription()
    {
      PrintInstructions result = new PrintInstructions();

      PrintInstruction pi = new PrintInstruction();

      pi.Add(new ConsoleParams("You are in the "));
      pi.Add(new ConsoleParams(RoomInfo[CurrentRoom.Id].Name, CurrentRoom.Color));
      pi.Add(new ConsoleParams($" ({RoomInfo[CurrentRoom.Id].Character}) Room"));

      result.Add(pi);

      result.Add(new PrintInstruction() { new ConsoleParams(" ") });

      result.Add(new PrintInstruction() { new ConsoleParams("Exits:") });

      ConsoleColor northDoor = GetDoorColor(CurrentRoomPosition, Direction.North);
      ConsoleColor eastDoor = GetDoorColor(CurrentRoomPosition, Direction.East);
      ConsoleColor southDoor = GetDoorColor(CurrentRoomPosition, Direction.South);
      ConsoleColor westDoor = GetDoorColor(CurrentRoomPosition, Direction.West);

      pi = new PrintInstruction();
      pi.Add(new ConsoleParams($"    {northDoor}", northDoor));
      pi.Add(new ConsoleParams(" door to the North"));
      result.Add(pi);

      pi = new PrintInstruction();
      pi.Add(new ConsoleParams($"    {eastDoor}", eastDoor));
      pi.Add(new ConsoleParams(" door to the East"));
      result.Add(pi);

      pi = new PrintInstruction();
      pi.Add(new ConsoleParams($"    {southDoor}", southDoor));
      pi.Add(new ConsoleParams(" door to the South"));
      result.Add(pi);

      pi = new PrintInstruction();
      pi.Add(new ConsoleParams($"    {westDoor}", westDoor));
      pi.Add(new ConsoleParams(" door to the West"));
      result.Add(pi);

      result.Add(new PrintInstruction() { new ConsoleParams(" ") });

      if (CurrentRoom.Items.Count == 0)
      {
        result.Add(new PrintInstruction() { new ConsoleParams("The Room is empty") });
      }
      else
      {
        result.Add(new PrintInstruction() { new ConsoleParams("Items in the room") });
        CurrentRoom.Items.ForEach(item =>
        {
          pi = item.ItemDescription;
          pi.Insert(0, new ConsoleParams("    "));
          result.Add(pi);
        });
      }

      return result;
    }

    public MoveResults MovePlayer(Direction direction)
    {
      MoveResults result = MoveResults(direction);

      if (result.Success)
      {
        if (FullyMerged())
        {
          CurrentRoomPosition = GetAdjoiningRoomPosition(CurrentRoomPosition, direction);
        }
        else
        {
          switch (Level)
          {
            case Types.Level.One:
              switch (direction)
              {
                case Direction.East:
                case Direction.West:
                  RotateRooms(CurrentRoomPosition, direction);
                  break;
                case Direction.North:
                case Direction.South:
                  FlipRooms(direction);
                  break;
              }
              break;
            case Types.Level.Two:
              switch (direction)
              {
                case Direction.North:
                case Direction.South:
                  RotateRooms(CurrentRoomPosition, direction);
                  break;
                case Direction.East:
                case Direction.West:
                  FlipRooms(direction);
                  break;
              }
              break;
          }

          if (FullyMerged())
          {
            AddCards();
          }
        }
      }

      if (result.Success)
      {
        int newIndex = result.PrintInstructions.Count;
        BaseRoom room = RoomPositions[CurrentRoomPosition];
        result.PrintInstructions.Add(new List<ConsoleParams>());
        result.PrintInstructions[newIndex].Add(new ConsoleParams("You enter the ", ConsoleColor.DarkGreen));
        result.PrintInstructions[newIndex].Add(new ConsoleParams(RoomInfo[room.Id].Name, room.Color));
        result.PrintInstructions[newIndex].Add(new ConsoleParams(" room.", ConsoleColor.DarkGreen));
      }

      return result;
    }

    public static Dictionary<RoomPosition, BaseRoom> RoomPositions { get; set; } = new Dictionary<RoomPosition, BaseRoom>();

    // NOTE: I tried this for the DeligateMap but everything went static...
    // private static ConsoleParamsDelegate Copy = delegate (char c, int row, int col, RoomPosition pos)
    // {
    //   return new ConsoleParams(c, Types.WALL_COLOR, Types.ROOM_COLOR);
    // };

    public char[][] RoomsTemplate = new char[][]
    {
      // This is the fixed position room template
      // Rooms can change positions. Positions don't change
     //0123456....12 
      "┏━---━┳━---━┓".ToCharArray(), // 0 Don't check for overlap
      "| tTt v tTt |".ToCharArray(), // 1 Check for vertical overlap
      "┣━hhh━╋━hhh━┫".ToCharArray(), // 2 Check for horizontal overlap
      "| tTt v tTt |".ToCharArray(), // 3 Check for vertical overlap at [3][0, 12]
      "┗━---━┻━---━┛".ToCharArray()  // 4 Don't check for overlap
    };

    public ColorPairComparer ColorPairComparer = new ColorPairComparer();

    /// <summary> Gets created and initialized in ColorState setter</summary>
    public Dictionary<ColorPair, ConsoleColor> MergedColorDictionary { get; private set; }

    /// <summary> Gets created and initialized in ColorState setter</summary>
    public RoomColors RoomColors { get; private set; }

    public const ConsoleColor CCC = ConsoleColor.Cyan;
    public const ConsoleColor CCY = ConsoleColor.Yellow;
    public const ConsoleColor CCM = ConsoleColor.Magenta;
    public const ConsoleColor CCR = ConsoleColor.Red;
    public const ConsoleColor CCG = ConsoleColor.Green;
    public const ConsoleColor CCB = ConsoleColor.Blue;
    public const ConsoleColor CCK = Types.WALL_AND_DOOR_COLOR;
    public const ConsoleColor CCW = ConsoleColor.White;
    public const ConsoleColor CDM = ConsoleColor.DarkMagenta;
    public const ConsoleColor CDY = ConsoleColor.DarkYellow;

    public bool FullyMerged()
    {
      return RoomPositions[RoomPosition.One].Id == RoomIdentifier.D
        && RoomPositions[RoomPosition.Two].Id == RoomIdentifier.A
        && RoomPositions[RoomPosition.Three].Id == RoomIdentifier.C
        && RoomPositions[RoomPosition.Four].Id == RoomIdentifier.B;
    }

    public Dictionary<RoomIdentifier, RoomInfo> RoomInfo;

    public Types.ColorState ColorState
    {
      get { return ColorState; }
      set
      {
        // ColorState = value;
        MergedColorDictionary = new Dictionary<ColorPair, ConsoleColor>(ColorPairComparer);
        RoomColors = new Dictionary<RoomIdentifier, ConsoleColor>();
        RoomInfo = new Dictionary<RoomIdentifier, RoomInfo>();
        switch (value)
        {
          case Types.ColorState.CMYK:
            RoomInfo.Add(RoomIdentifier.A, new RoomInfo('χ', "Chi"));
            RoomInfo.Add(RoomIdentifier.B, new RoomInfo('κ', "Kappa"));
            RoomInfo.Add(RoomIdentifier.C, new RoomInfo('γ', "Gamma"));
            RoomInfo.Add(RoomIdentifier.D, new RoomInfo('μ', "Mu"));
            RoomColors.Add(RoomIdentifier.A, CCC);
            RoomColors.Add(RoomIdentifier.B, CCK);
            RoomColors.Add(RoomIdentifier.C, CCY);
            RoomColors.Add(RoomIdentifier.D, CCM);
            MergedColorDictionary.Add(new ColorPair(CCM, CCC), CCB);
            MergedColorDictionary.Add(new ColorPair(CCM, CCY), CCR);
            MergedColorDictionary.Add(new ColorPair(CCC, CCK), CCG);
            MergedColorDictionary.Add(new ColorPair(CCY, CCK), CCG);
            break;
          case Types.ColorState.RGBK:
            RoomColors.Add(RoomIdentifier.A, CCR);
            RoomColors.Add(RoomIdentifier.B, CCK);
            RoomColors.Add(RoomIdentifier.C, CCB);
            RoomColors.Add(RoomIdentifier.D, CCG);
            MergedColorDictionary.Add(new ColorPair(CCG, CCR), CCY);
            MergedColorDictionary.Add(new ColorPair(CCG, CCB), CCC);
            MergedColorDictionary.Add(new ColorPair(CCB, CCK), CDM);
            MergedColorDictionary.Add(new ColorPair(CCR, CCK), CDM);
            break;
          case Types.ColorState.RYBW:
            RoomInfo.Add(RoomIdentifier.A, new RoomInfo('R', "Red"));
            RoomInfo.Add(RoomIdentifier.B, new RoomInfo('W', "White"));
            RoomInfo.Add(RoomIdentifier.C, new RoomInfo('B', "Blue"));
            RoomInfo.Add(RoomIdentifier.D, new RoomInfo('Y', "Yellow"));
            RoomColors.Add(RoomIdentifier.A, CCR);
            RoomColors.Add(RoomIdentifier.B, CCW);
            RoomColors.Add(RoomIdentifier.C, CCB);
            RoomColors.Add(RoomIdentifier.D, CCY);
            MergedColorDictionary.Add(new ColorPair(CCY, CCR), CDY);
            MergedColorDictionary.Add(new ColorPair(CCY, CCB), CCG);
            MergedColorDictionary.Add(new ColorPair(CCB, CCW), CDM);
            MergedColorDictionary.Add(new ColorPair(CCR, CCW), CDM);
            break;
        }
      }
    }

    public ConsoleColor GetMergedColor(BaseRoom room1, BaseRoom room2)
    {
      if (room1.Color == room2.Color)
      {
        return room1.Color;
      }

      ColorPair colorPair = new ColorPair(room1.Color, room2.Color);

      return MergedColorDictionary[colorPair];
    }

    public BaseRoom GetAdjoiningRoom(RoomPosition currentPosition, Direction direction)
    {
      switch (currentPosition)
      {
        case RoomPosition.One:
          switch (direction)
          {
            case Direction.South:
              return RoomPositions[RoomPosition.Three];
            case Direction.East:
              return RoomPositions[RoomPosition.Two];
            default:
              break;
          }
          break;
        case RoomPosition.Two:
          switch (direction)
          {
            case Direction.West:
              return RoomPositions[RoomPosition.One];
            case Direction.South:
              return RoomPositions[RoomPosition.Four];
            default:
              break;
          }
          break;
        case RoomPosition.Three:
          switch (direction)
          {
            case Direction.North:
              return RoomPositions[RoomPosition.One];
            case Direction.East:
              return RoomPositions[RoomPosition.Four];
            default:
              break;
          }
          break;
        case RoomPosition.Four:
          switch (direction)
          {
            case Direction.North:
              return RoomPositions[RoomPosition.Two];
            case Direction.West:
              return RoomPositions[RoomPosition.Three];
            default:
              break;
          }
          break;
        default:
          break;
      }
      return null;
    }

    public Direction GetOppisiteDirection(Direction d)
    {
      switch (d)
      {
        case Direction.North:
          return Direction.South;
        case Direction.East:
          return Direction.West;
        case Direction.South:
          return Direction.North;
        case Direction.West:
          return Direction.East;
      }
      return d;
    }

    public bool DoorsAreMerged(RoomPosition currentPosition, Direction direction)
    {
      BaseRoom adjoiningRoom = GetAdjoiningRoom(currentPosition, direction);

      if (adjoiningRoom == null)
      {
        return false;
      }

      bool currentIsMergable = ColoredDoorChars.Contains(RoomPositions[currentPosition].DoorCharacter(direction));
      bool adjoiningIsMergable = ColoredDoorChars.Contains(adjoiningRoom.DoorCharacter(GetOppisiteDirection(direction)));

      return currentIsMergable && adjoiningIsMergable;
    }

    public ConsoleColor GetDoorColor(RoomPosition roomPosition, Direction direction)
    {
      BaseRoom room = RoomPositions[roomPosition];
      BaseRoom adjoiningRoom = GetAdjoiningRoom(roomPosition, direction);
      char doorChar = room.DoorCharacter(direction);

      if (DoorsAreMerged(roomPosition, direction))
      {
        // We need to get the merged color
        return GetMergedColor(room, adjoiningRoom);
      }

      if (ColoredDoorChars.Contains(doorChar))
      {
        return room.Color;
      }

      // If the adjoining door is a color door we get that
      if (adjoiningRoom != null)
      {
        char adjoiningDoorChar = adjoiningRoom.DoorCharacter(GetOppisiteDirection(direction));
        if (ColoredDoorChars.Contains(adjoiningDoorChar))
        {
          return adjoiningRoom.Color;
        }
      }

      return Types.WALL_AND_DOOR_COLOR;
    }

    // public char GetDoorCharacter(BaseRoom room, Direction direction)
    // {
    //   return room.DoorCharacter(direction);
    // }

    public char GetDoorChar(RoomPosition roomPosition, Direction direction)
    {
      BaseRoom currentRoom = RoomPositions[roomPosition];
      BaseRoom adjoiningRoom = GetAdjoiningRoom(roomPosition, direction);

      char curRoomChar = currentRoom.DoorCharacter(direction);

      if (adjoiningRoom == null)
      {
        return curRoomChar;
      }

      char adjRoomChar = adjoiningRoom.DoorCharacter(GetOppisiteDirection(direction));

      if (ColoredDoorChars.Contains(adjRoomChar))
      {
        return adjRoomChar;
      }

      return curRoomChar;
    }

    public ConsoleColor GetBackgroundColor(RoomPosition pos)
    {
      BaseRoom room = RoomPositions[pos];
      return FullyMerged() ? room.Color : Types.BACKGROUND_COLOR;
    }

    public ConsoleColor GetForegroundColor()
    {
      return FullyMerged() ? Types.BACKGROUND_COLOR : ConsoleColor.Red;
    }

    // ConsoleParamsDelegates...
    private ConsoleParams Copy(char c, int row, int col, RoomPosition pos)
    {
      return new ConsoleParams(c.ToString(), Types.WALL_AND_DOOR_COLOR, Types.ROOM_COLOR);
    }

    private ConsoleParams Lookup(char c, int row, int col, RoomPosition pos)
    {
      BaseRoom room = RoomPositions[pos];
      char doorChar = RoomChar(room, row, col);
      ConsoleColor doorColor = ColoredDoorChars.Contains(doorChar) ? room.Color : Types.WALL_AND_DOOR_COLOR;
      return new ConsoleParams(doorChar.ToString(), doorColor, Types.BACKGROUND_COLOR);
    }

    private ConsoleParams HorizontalOverlap(char c, int row, int col, RoomPosition pos)
    {
      // If we are here we know that we are looking south
      // to check the overlap so we use Direction.South
      BaseRoom room = RoomPositions[pos];
      ConsoleColor color = GetDoorColor(pos, Direction.South);
      char doorChar = GetDoorChar(pos, Direction.South);
      return new ConsoleParams(doorChar.ToString(), color, Types.ROOM_COLOR);
    }

    private ConsoleParams VerticalOverlap(char c, int row, int col, RoomPosition pos)
    {
      // If we are here we know we are looking east
      // We will use Direction.East

      ConsoleColor color = GetDoorColor(pos, Direction.East);
      char doorChar = GetDoorChar(pos, Direction.East);
      return new ConsoleParams(doorChar.ToString(), color, Types.ROOM_COLOR);
    }

    private ConsoleParams Space(char c, int row, int col, RoomPosition pos)
    {
      return new ConsoleParams(' '.ToString(), GetForegroundColor(), GetBackgroundColor(pos));
    }

    private ConsoleParams RoomDesignator(char c, int row, int col, RoomPosition pos)
    {
      char roomChar = RoomInfo[RoomPositions[pos].Id].Character;
      return new ConsoleParams(roomChar.ToString(), GetForegroundColor(), GetBackgroundColor(pos));
    }

    private Dictionary<char, ConsoleParamsDelegate> DeligateMap;

    public PrintInstructions PrintInstructions(string banner, string spaces = "  ")
    {
      PrintInstructions result = new PrintInstructions();
      int row, col, rowOffset, colOffset;
      RoomPosition position;
      char c;


      for (row = 0; row < 3; row++)
      {
        result.Add(new List<ConsoleParams>());

        for (col = 0; col < 7; col++)
        {
          c = RoomsTemplate[row][col];
          position = RoomPosition.One;
          // rowOffset = 0;
          // colOffset = 0;
          result[row].Add(DeligateMap[c](c, row, col, position));
        }

        for (col = 7; col < 13; col++)
        {
          c = RoomsTemplate[row][col];
          position = RoomPosition.Two;
          // rowOffset = 0;
          colOffset = 6;
          result[row].Add(DeligateMap[c](c, row, col - colOffset, position));
        }
      }

      for (row = 3; row < 5; row++)
      {
        result.Add(new List<ConsoleParams>());

        for (col = 0; col < 7; col++)
        {
          c = RoomsTemplate[row][col];
          position = RoomPosition.Three;
          rowOffset = 2;
          // colOffset = 0;
          result[row].Add(DeligateMap[c](c, row - rowOffset, col, position));
        }

        for (col = 7; col < 13; col++)
        {
          c = RoomsTemplate[row][col];
          position = RoomPosition.Four;
          rowOffset = 2;
          colOffset = 6;
          result[row].Add(DeligateMap[c](c, row - rowOffset, col - colOffset, position));
        }
      }

      return result;
    }

    private void _setup()
    {
      DeligateMap = new Dictionary<char, ConsoleParamsDelegate>()
      {
          { ' ', Copy },
          { '┏', Copy },
          { '━', Copy },
          { '┳', Copy },
          { '┓', Copy },
          { '┣', Copy },
          { '┫', Copy },
          { '╋', Copy },
          { '┗', Copy },
          { '┻', Copy },
          { '┛', Copy },
          { '-', Lookup },
          { '|', Lookup },
          { 'h', HorizontalOverlap},
          { 'v', VerticalOverlap },
          { 't', Space },
          { 'T', RoomDesignator }
      };

      ColorState = Types.ColorState.CMYK;

      // Add rooms to the

      BaseRoom A = new BaseRoom(RoomIdentifier.A, RoomTemplates, RoomColors);
      BaseRoom B = new BaseRoom(RoomIdentifier.B, RoomTemplates, RoomColors);
      BaseRoom C = new BaseRoom(RoomIdentifier.C, RoomTemplates, RoomColors);
      BaseRoom D = new BaseRoom(RoomIdentifier.D, RoomTemplates, RoomColors);

      Calculator Calculator = new Calculator();
      C.Items.Add(Calculator);

      RoomPositions.Add(RoomPosition.One, B);
      RoomPositions.Add(RoomPosition.Two, C);
      RoomPositions.Add(RoomPosition.Three, A);
      RoomPositions.Add(RoomPosition.Four, D);
    }

    public Rooms()
    {
      _setup();
    }

  }
}