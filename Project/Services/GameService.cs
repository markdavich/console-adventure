using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;
using System.Globalization;
using System;
using ConsoleAdventure.Models;
using PrintInstruction = System.Collections.Generic.List<ConsoleAdventure.Models.ConsoleParams>;
using PrintInstructions = System.Collections.Generic.List<System.Collections.Generic.List<ConsoleAdventure.Models.ConsoleParams>>;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    public enum Noun { Calculator }

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
        SetPrintInstructions(_rooms.MovePlayer(directionEnum).PrintInstructions);
      }
    }

    internal void Put(string option)
    {
      string noun = option.Substring(0, option.IndexOf(" "));
      string verb = option.Substring(option.IndexOf(" ") + 1).Trim();

      Noun nounEnum;
      Verb verbEnum;

      PrintInstructions pi = new PrintInstructions() { new PrintInstruction() };

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
                  pi[0].Add(new ConsoleParams("You turn the calculator off and put it in your pocket", ConsoleColor.DarkGreen));
                  SetCalculator(false);
                  SetPrintInstructions(pi);
                  return;
              }
              break;
          }
        }
        pi[0].Add(new ConsoleParams("You can't put the "));
        pi[0].Add(new ConsoleParams(noun, ConsoleColor.DarkRed));
        pi[0].Add(new ConsoleParams($" '{verb}' ", ConsoleColor.DarkRed));

        SetPrintInstructions(pi);
        return;
      }
      pi[0].Add(new ConsoleParams("There is no "));
      pi[0].Add(new ConsoleParams($"'{noun}' ", ConsoleColor.DarkRed));
      pi[0].Add(new ConsoleParams("to put "));
      pi[0].Add(new ConsoleParams(verb, ConsoleColor.DarkRed));
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

      PrintInstructions.Add(new List<ConsoleParams>() { new ConsoleParams(" ") });

      Calculator calc = _game.CurrentPlayer.Inventory.Find(item =>
      {
        return item is Calculator;
      }) as Calculator;

      if (calc != null)
      {
        if (calc.On)
        {
          calc.Display.ForEach(line =>
          {
            PrintInstructions.Add(line);
          });
        }
      }

      if (instructions != null)
      {
        PrintInstructions.Add(new List<ConsoleParams>() { new ConsoleParams(" ") });
        instructions.ForEach(line =>
        {
          PrintInstructions.Add(line);
        });
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