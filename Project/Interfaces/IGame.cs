using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
    public interface IGame
    {
        IRoom CurrentRoom { get; set; }
        IPlayer CurrentPlayer { get; set; }

        void Setup();
    }
}