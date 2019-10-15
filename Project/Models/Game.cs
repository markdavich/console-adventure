using System;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...

    public void Setup(string userName, ConsoleColor favoriteColor)
    {
      CurrentPlayer = new Player(userName, favoriteColor);
    }

    public Game(string userName, ConsoleColor favoriteColor)
    {
      Setup(userName, favoriteColor);
    }

  }
}