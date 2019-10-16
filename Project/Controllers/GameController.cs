using System;
using System.Collections.Generic;
using ConsoleAdventure.Models;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;
using ConsoleUtilities;
using PrintInstructions = System.Collections.Generic.List<System.Collections.Generic.List<ConsoleAdventure.Models.ConsoleParams>>;
using ConsoleAdventure;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService { get; set; }

    //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
    public void Run()
    {
      while (true)
      {
        Print();
        GetUserInput();
      }
    }

    //NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
    public void GetUserInput()
    {
      Console.WriteLine("What would you like to do?");

      Console.ForegroundColor = _gameService.PlayerColor;

      string input = Console.ReadLine().ToLower() + " ";
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();
      //NOTE this will take the user input and parse it into a command and option.
      //IE: take silver key => command = "take" option = "silver key"

      switch (command)
      {
        case "go":
          _gameService.Go(option);
          break;
        case "look":
          _gameService.Look();
          break;
        case "take":
          _gameService.TakeItem(option);
          break;
        case "use":
          _gameService.UseItem(option);
          break;
        case "put":
          _gameService.Put(option);
          break;
        default:
          Console.WriteLine("Enter a valid command");
          break;
      }


    }

    //NOTE this should print your messages for the game.
    private void Print()
    {
      Console.Clear();
      Log log = new Log();

      Program.TitleController.DrawHeader();

      _gameService.PrintInstructions.ForEach(set =>
      {
        set.ForEach(instruction =>
        {
          log.Add(instruction.Text, instruction.Foreground, instruction.Background);
        });
        log.Print();
      });

      log.NewLine();
    }

    internal void Start(string userName, ConsoleColor favoriteColor)
    {
      _gameService = new GameService(userName, favoriteColor);
      Run();
    }
  }
}