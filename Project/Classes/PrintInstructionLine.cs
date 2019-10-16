using System;
using System.Collections.Generic;

namespace ConsoleAdventure.Classes
{
  public class PrintInstructionLine
  {
    public List<ConsoleParams> Instructions;

    public PrintInstructionLine Add(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      Instructions.Add(new ConsoleParams(text, foreground, background));
      return this;
    }

    public PrintInstructionLine Add(ConsoleParams instruction)
    {
      Instructions.Add(instruction);
      return this;
    }

    public PrintInstructionLine Add(PrintInstructionLine printInstructionLine)
    {
      printInstructionLine.Instructions.ForEach(instruction =>
      {
        Instructions.Add(instruction);
      });
      return this;
    }

    public PrintInstructionLine()
    {
      Instructions = new List<ConsoleParams>();
    }

    public PrintInstructionLine(ConsoleParams instruction)
    {
      Instructions = new List<ConsoleParams>() { instruction };
    }

    public PrintInstructionLine(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      Instructions = new List<ConsoleParams>() { new ConsoleParams(text, foreground, background) };
    }
  }
}