using ConsoleAdventure.Project;

namespace ConsoleAdventure.Services
{
  public class Commands
  {
    private GameService _gameService;

    public Commands(GameService gameService)
    {
      _gameService = gameService;
    }
  }
}