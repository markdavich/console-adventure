using System;

using static ConsoleAdventure.Types;

namespace ConsoleAdventure.Classes
{
  using ConsoleAdventure.Models;
  using ConsoleAdventure.Project.Models;

  public class Lock
  {
    public bool Locked { get; set; } = false;

    public RoomPosition LockedRoomPosition { get; set; } = RoomPosition.None;

    public RoomIdentifier LockedRoomId { get; set; } = RoomIdentifier.None;

    public Direction LockedDoorDirection { get; set; } = Direction.None;

    public RoomPosition KeyPadRoomPosition { get; set; } = RoomPosition.None;

    public RoomIdentifier KeyPadRoomId { get; set; } = RoomIdentifier.None;

    public Direction KeyPadDoorDirection { get; set; } = Direction.None;

    private Rooms _rooms { get; set; }

    public bool DoorIsLocked(RoomPosition roomPosition, Direction direction)
    {
      if (!Locked)
      {
        return false;
      }

      if (roomPosition == LockedRoomPosition && direction == LockedDoorDirection)
      {
        return true;
      }

      return (roomPosition == KeyPadRoomPosition) && (direction == KeyPadDoorDirection);
    }

    public void Set()
    {
      if (Locked)
      {
        return;
      }

      Random random = new Random();
      int enter = random.Next(0, 2);
      int a;
      int b;

      switch (enter)
      {
        case 0: // 1 in three chance
          a = random.Next(0, 2);
          b = random.Next(0, 2);
          Locked = a == b;
          break;

        case 1: // 1 in six chance (Roll the dice)
          a = random.Next(0, 5);
          b = random.Next(0, 5);
          Locked = a == b;
          break;

        case 2: // 1 in 10 chance
          a = random.Next(0, 9);
          b = random.Next(0, 9);
          Locked = a == b;
          break;
      }

      if (!Locked)
      {
        return;
      }

      int roomNumber = random.Next(0, 3);

      LockedRoomPosition = (RoomPosition)roomNumber;

      int coin = random.Next(0, 1);

      Direction doorDirection;

      switch (LockedRoomPosition)
      {
        case RoomPosition.One:
          doorDirection = coin == 1 ? Direction.South : Direction.East;
          break;
        case RoomPosition.Two:
          doorDirection = coin == 1 ? Direction.South : Direction.West;
          break;
        case RoomPosition.Three:
          doorDirection = coin == 1 ? Direction.North : Direction.East;
          break;
        case RoomPosition.Four:
          doorDirection = coin == 1 ? Direction.North : Direction.West;
          break;
        default:
          doorDirection = Direction.East;
          break;
      }

      LockedRoomId = _rooms.RoomPositions[LockedRoomPosition].Id;
      LockedDoorDirection = doorDirection;

      KeyPadRoomPosition = _rooms.GetAdjoiningRoomPosition(LockedRoomPosition, doorDirection);
      KeyPadRoomId = _rooms.GetAdjoiningRoom(LockedRoomPosition, doorDirection).Id;
      KeyPadDoorDirection = _rooms.GetOppisiteDirection(doorDirection);

      ConsoleColor roomColor = _rooms.RoomColors[KeyPadRoomId];
      ConsoleColor doorColor = _rooms.GetDoorColor(KeyPadRoomPosition, KeyPadDoorDirection);
      BaseRoom keyPadRoom = _rooms.RoomPositions[KeyPadRoomPosition];

      keyPadRoom.Items.Add(new KeyPadLock(roomColor, KeyPadDoorDirection, doorColor));
    }

    private void _reset()
    {
      LockedDoorDirection = Direction.None;
      LockedRoomId = RoomIdentifier.None;
      LockedRoomPosition = RoomPosition.None;
      KeyPadDoorDirection = Direction.None;
      KeyPadRoomId = RoomIdentifier.None;
      KeyPadRoomPosition = RoomPosition.None;
      Locked = false;
    }

    public void Unlock()
    {
      BaseRoom room = _rooms.RoomPositions[KeyPadRoomPosition];
      room.RemoveItem(KeyPadLock.KeyPadName);
      _reset();
    }

    public Lock(Rooms rooms)
    {
      _rooms = rooms;
    }
  }
}