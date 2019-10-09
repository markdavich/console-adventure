using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
    public interface IGameService
    {
        List<string> Messages { get; }
        void Setup(string playerName);

        void Reset();

        #region Console Commands

        //Stops the application
        void Quit();

        //Should display a list of commands to the console
        void Help();

        //Validate CurrentRoom.Exits contains the desired direction
        //if it does change the CurrentRoom
        void Go(string direction);
        void TakeItem(string itemName);
        void UseItem(string itemName);
        //Print the list of items in the players inventory to the console
        void Inventory();
        //Display the CurrentRoom Description, Exits, and Items
        void Look();

        #endregion
    }
}
