using System;
using ConsoleAdventure.Controllers;
using ConsoleAdventure.Project;
using ConsoleAdventure.Project.Controllers;

namespace ConsoleAdventure
{
  public class ProgramUtils
  {
    public TitleController TitleController { get; private set; } = new TitleController();
  }
  public class Program
  {
    public static TitleController TitleController { get; set; }
    public static void Main(string[] args)
    {
      TitleController = new ProgramUtils().TitleController;
      // new ProgramUtils();
      new GameController().Start(TitleController.Name, TitleController.UserColor);
    }
  }
}
