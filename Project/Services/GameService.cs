using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;
using System.Globalization;
using System;
using ConsoleAdventure.Models;
using ConsoleAdventure.Classes;
using static ConsoleAdventure.Types;
using ConsoleUtilities;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    public enum Noun { Calculator, Door }

    public enum Verb { Away }

    public bool EasyMode { get; private set; }

    private void _setEasyMode(string userName)
    {
      foreach (string s in GameService.CHEATS)
      {
        if (userName.ToLower().Contains(s.ToLower()))
        {
          EasyMode = true;
        }
      }
    }

    public static string[] CHEATS { get; } = new string[] { "cheat", "easy", "d$" };
    private Game _game { get; set; }

    private Rooms _rooms { get; set; }

    private TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;

    public List<string> Messages { get; set; }

    public PrintInstructions PrintInstructions { get; set; } = new PrintInstructions();

    public PrintInstructions MoveInstructions { get; set; } = new PrintInstructions();

    public GameService(string userName, ConsoleColor favoriteColor)
    {
      _rooms = new Rooms();
      _game = new Game(userName, favoriteColor);
      _setEasyMode(userName);
      Messages = new List<string>();
      SetPrintInstructions();
    }

    public void Go(string direction)
    {
      Types.Direction directionEnum;
      if (Enum.TryParse(_textInfo.ToTitleCase(direction), out directionEnum))
      {
        if (!EasyMode)
        {
          _rooms.Lock.Set();
        }
        SetPrintInstructions(_rooms.MovePlayer(directionEnum).PrintInstructions);
      }
    }

    internal void Unlock(string option)
    {
      PrintInstructions pi = new PrintInstructions();
      string[] options = option.Trim().Split(" ");
      string direction;
      string noun;

      if (options[0] == "")
      {
        pi.Add("What did you want to unlock?", ConsoleColor.Blue);
        SetPrintInstructions(pi);
        return;
      }

      if (options.Length < 2)
      {
        direction = options[0];
        noun = "";
      }
      else
      {
        direction = options[0];
        noun = options[1];
      }

      Noun nounEnum;
      Direction directionEnum;

      // Check for direction
      if (Enum.TryParse(_textInfo.ToTitleCase(direction), out directionEnum))
      {
        // Check the noun
        if (Enum.TryParse(_textInfo.ToTitleCase(noun), out nounEnum))
        {
          switch (nounEnum)
          {
            case Noun.Door:
              // They have given us a valid direction and "Door"
              if (_rooms.Lock.DoorIsLocked(_rooms.CurrentRoomPosition, directionEnum))
              {
                UnlockDoor();
                return;
                // Ok This door is locked
              }
              else
              {
                pi.Add($"The {directionEnum} is not locked", ConsoleColor.Blue);
                SetPrintInstructions(pi);
                return;
              }
            default:
              // We dont have anything else to unlock at this direction
              pi.Add($"There is no {directionEnum} {nounEnum} to Unlock");
              SetPrintInstructions(pi);
              return;
          }
        }
        // We were given a valid direction but an invalid noun
        pi.Add($"There is no {directionEnum} {noun} to Unlock");
        SetPrintInstructions(pi);
        return;
      }
      else if (Enum.TryParse(_textInfo.ToTitleCase(direction), out nounEnum)) // Check for a noun in the direction place
      {
        switch (nounEnum)
        {
          case Noun.Door:
            // Which door???
            pi.Add($"Which {nounEnum} did you want to unlock?", ConsoleColor.Blue);
            pi.NewLine($"Did you mean 'unlock [north|east|south|west] door'?", ConsoleColor.Blue);
            SetPrintInstructions(pi);
            return;
          case Noun.Calculator:
            // The calculator is unlocked...
            if (PlayerHasItem(noun.ToLower()))
            {
              pi.Add("The calculator is unlocked", ConsoleColor.DarkGreen);
              SetPrintInstructions(pi);
              return;
            }
            else
            {
              pi.Add("You don't have a calculator to unlock", ConsoleColor.DarkRed);
              SetPrintInstructions(pi);
              return;
            }
          default:
            pi.Add($"There is no '{noun}' to unlock", ConsoleColor.DarkRed);
            SetPrintInstructions(pi);
            return;
        }
      }
      else
      {
        // We were given an unknown noun and direction
        pi.Add($"There is no '{option}' to unlock", ConsoleColor.DarkRed);
        SetPrintInstructions(pi);
        return;
      }
    }

    private void UnlockDoor()
    {
      bool trying = true;

      KeyPadLock kpl = _rooms.CurrentRoom.FindItem(KeyPadLock.KeyPadName) as KeyPadLock;

      string msg = "";

      while (trying)
      {
        // Print Title
        // Print Rooms
        // Print KeyPad

        Console.Clear();
        Log log = new Log();

        PrintInstructions calcPrintInstructions = null;

        // Print Title
        Program.TitleController.DrawHeader();

        // Print Rooms
        _rooms.PrintInstructions("").Lines.ForEach(line =>
        {
          line.Instructions.ForEach(instruction =>
          {
            log.Add(instruction.Text, instruction.Foreground, instruction.Background);
          });
          log.Print();
        });

        Calculator calc = _game.CurrentPlayer.Inventory.Find(item =>
        {
          return item is Calculator;
        }) as Calculator;

        if (calc != null)
        {
          if (calc.On)
          {

            calcPrintInstructions = calc.Display;
          }
        }

        PrintInstructions unlockInstructions = new PrintInstructions().AddInstructions(kpl.Display);

        if (calcPrintInstructions != null)
        {
          unlockInstructions.RightJoin(calcPrintInstructions);
        }

        unlockInstructions.NewLine(msg, ConsoleColor.DarkRed);

        unlockInstructions.Lines.ForEach(line =>
        {
          line.Instructions.ForEach(instruction =>
          {
            log.Add(instruction.Text, instruction.Foreground, instruction.Background);
          });
          log.Print();
        });

        log.NewLine();

        log.Add("Enter the ");
        log.Add("code", ConsoleColor.DarkRed);
        log.Add(" to unlock the door (type 'quit' to stop trying)");
        log.Print();

        string userInput = Console.ReadLine();

        if (userInput.ToLower().Trim() == "quit")
        {
          SetPrintInstructions(new PrintInstructions("You couldn't unlock the door", ConsoleColor.DarkRed));
          return;
        }

        if (userInput == "use calculator")
        {
          SetCalculator(true);
          continue;
        }

        try
        {
          int numVal = Int32.Parse(userInput.Substring(0, 5));

          bool unlocked = kpl.Unlock(numVal);

          if (unlocked)
          {
            Classes.Lock lk = _rooms.Lock;
            calcPrintInstructions = new PrintInstructions();
            calcPrintInstructions.NewLine("You Unlocked the ")
              .Add($"{lk.KeyPadDoorDirection}", _rooms.GetDoorColor(lk.KeyPadRoomPosition, lk.KeyPadDoorDirection))
              .Add(" DOOR!!!");
            calcPrintInstructions.NewLine("NICE JOB!!!", ConsoleColor.Green);
            lk.Unlock();
            return;
          }
          else
          {
            msg = $"  '{userInput}' does not unlock the door. Good try though.";
          }
        }
        catch (FormatException e)
        {
          msg = $"  '{userInput}' does not unlock the door";
        }
      }
    }

    internal void Put(string option)
    {
      string noun = option.Substring(0, option.IndexOf(" "));
      string verb = option.Substring(option.IndexOf(" ") + 1).Trim();

      Noun nounEnum;
      Verb verbEnum;

      PrintInstructions pi = new PrintInstructions();

      if (Enum.TryParse(_textInfo.ToTitleCase(noun), out nounEnum))
      {
        if (Enum.TryParse(_textInfo.ToTitleCase(verb), out verbEnum))
        {
          switch (nounEnum)
          {
            case Noun.Calculator:
              switch (verbEnum)
              {
                case Verb.Away:
                  pi.Add("You turn the calculator off and put it in your pocket", ConsoleColor.DarkGreen);
                  SetCalculator(false);
                  SetPrintInstructions(pi);
                  return;
              }
              break;
          }
        }
        pi.Add("You can't put the ");
        pi.Add(noun, ConsoleColor.DarkRed);
        pi.Add($" '{verb}' ", ConsoleColor.DarkRed);

        SetPrintInstructions(pi);
        return;
      }
      pi.Add("There is no ");
      pi.Add($"'{noun}' ", ConsoleColor.DarkRed);
      pi.Add("to put ");
      pi.Add(verb, ConsoleColor.DarkRed);
      SetPrintInstructions(pi);
    }

    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      SetPrintInstructions(_rooms.GetRoomDescription());
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }

    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public ConsoleColor PlayerColor
    {
      get
      {
        return _game.CurrentPlayer.FavoriteColor;
      }
    }

    public void SetPrintInstructions(PrintInstructions instructions = null)
    {
      PrintInstructions = _rooms.PrintInstructions("");

      Calculator calc = _game.CurrentPlayer.Inventory.Find(item =>
      {
        return item is Calculator;
      }) as Calculator;

      if (calc != null)
      {
        if (calc.On)
        {
          PrintInstructions.NewLine();
          PrintInstructions.AddInstructions(calc.Display);
        }
      }

      if (instructions != null)
      {
        PrintInstructions.NewLine();
        PrintInstructions.AddInstructions(instructions);
      }
    }

    public void Setup(string playerName)
    {
      _rooms = new Rooms();
    }

    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      Item item = _rooms.CurrentRoom.TakeItem(itemName);
      if (item != null)
      {
        _game.CurrentPlayer.Inventory.Add(item);
        SetPrintInstructions(item.TakeMessage);
      }
    }

    public void SetCalculator(bool onOff)
    {
      if (PlayerHasItem("calculator"))
      {
        Calculator calculator = PlayerItem("calculator") as Calculator;
        calculator.On = onOff;
        SetPrintInstructions(calculator.UseMessage);
      }
    }

    private bool PlayerHasItem(string itemName)
    {
      return _game.CurrentPlayer.Inventory.Find(item =>
      {
        return item.Name.ToLower() == itemName;
      }) != null;
    }

    private bool RoomHasItem(string itemName)
    {
      return _rooms.CurrentRoom.Items.Find(item =>
      {
        return item.Name.ToLower() == itemName;
      }) != null;
    }

    private Item PlayerItem(string itemName)
    {
      return _game.CurrentPlayer.Inventory.Find(item =>
      {
        return item.Name.ToLower() == itemName;
      });
    }

    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public void UseItem(string itemName)
    {
      if (PlayerHasItem(itemName))
      {
        Item i = PlayerItem(itemName);
        if (i is Calculator)
        {

          (i as Calculator).On = true;
          // SetPrintInstructions((i as Calculator).UseMessage);

        }
        SetPrintInstructions();
        return;
      }
    }
  }
}