using System;

namespace ConsoleAdventure.Classes
{
  public struct ConsoleParams
  {
    public string Text;
    public ConsoleColor Foreground, Background;

    public ConsoleParams(string text, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = Types.BACKGROUND_COLOR)
    {
      Text = text;
      Foreground = foreground;
      Background = background;
    }
  }
}