using System;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
  public interface IGame
  {
    IRoom CurrentRoom { get; set; }
    Player CurrentPlayer { get; set; }

    void Setup(string userName, ConsoleColor favoriteColor);
  }
}