using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
    public class Player : IPlayer
    {
        public string Name { get; set; }

        public ConsoleColor FavoriteColor { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player(string name, ConsoleColor favoriteColor)
        {
            Name = name;
            FavoriteColor = favoriteColor;
        }
    }
}