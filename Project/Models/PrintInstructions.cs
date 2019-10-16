using System;
using System.Collections.Generic;

namespace ConsoleAdventure.Models
{
  public class PrintInstructionLine
  {
    public List<ConsoleParams> Line;

    public void Add(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      Line.Add(new ConsoleParams(text, foreground, background));
    }

    public PrintInstructionLine()
    {
      Line = new List<ConsoleParams>();
    }

    public PrintInstructionLine(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      Line = new List<ConsoleParams>() { new ConsoleParams(text, foreground, background) };
    }
  }
  public class PrintInstructions
  {
    public List<PrintInstructionLine> Lines { get; private set; }

    private int _index { get { return Lines.Count - 1; } }

    public PrintInstructionLine Add(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      if (Lines.Count == 0)
      {
        Lines.Add(new PrintInstructionLine(text, foreground, background));
        return Lines[0];
      }

      Lines[_index].Add(text, foreground, background);
      return Lines[_index];
    }

    public PrintInstructionLine NewLine(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      if (Lines.Count == 0)
      {
        Lines.Add(new PrintInstructionLine(text, foreground, background));
        return Lines[0];
      }

      Lines.Add(new PrintInstructionLine(text, foreground, background));
      return Lines[_index];
    }

    public PrintInstructions()
    {
      Lines = new List<PrintInstructionLine>();
    }
  }
}