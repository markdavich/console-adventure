using System;
using System.Collections.Generic;
using ConsoleAdventure.Classes;

namespace ConsoleAdventure.Classes
{

  public class PrintInstructions
  {
    public List<PrintInstructionLine> Lines { get; private set; }

    private int _index { get { return Lines.Count - 1; } }

    public PrintInstructions Add(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      if (Lines.Count == 0)
      {
        Lines.Add(new PrintInstructionLine(text, foreground, background));
        return this;
        // return Lines[0];
      }

      Lines[_index].Add(text, foreground, background);
      return this;
      // return Lines[_index];
    }

    public PrintInstructions Add(PrintInstructionLine printInstructionLine)
    {
      if (Lines.Count == 0)
      {
        Lines.Add(printInstructionLine);
        return this;
      }

      printInstructionLine.Instructions.ForEach(instruction =>
      {
        Lines[_index].Add(instruction);
      });

      return this;
    }

    public PrintInstructions Add(ConsoleParams instruction)
    {
      if (Lines.Count == 0)
      {
        Lines.Add(new PrintInstructionLine(instruction));
        return this;
      }

      Lines[_index].Add(instruction);

      return this;
    }

    public PrintInstructions AddInstructions(PrintInstructions printInstructions)
    {
      printInstructions.Lines.ForEach(line =>
      {
        Lines.Add(line);
      });

      return this;
    }

    public PrintInstructions NewLine(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      if (Lines.Count == 0)
      {
        Lines.Add(new PrintInstructionLine(text, foreground, background));
        return this;
      }

      Lines.Add(new PrintInstructionLine(text, foreground, background));
      return this;
    }

    public PrintInstructions NewLine(PrintInstructionLine printInstructionLine)
    {
      if (Lines.Count == 0)
      {
        Lines.Add(printInstructionLine);
        return this;
      }

      NewLine();
      printInstructionLine.Instructions.ForEach(instruction =>
      {
        Lines[_index].Add(instruction);
      });

      return this;
    }

    private int _maxLength()
    {
      int max = 0;
      int start = 0;
      Lines.ForEach(line =>
      {
        start = 0;
        line.Instructions.ForEach(instruction =>
        {
          start += instruction.Text.Length;
        });
        if (start > max)
        {
          max = start;
        }
      });
      return max;
    }

    private void _addTrailingSpaces()
    {
      int max = _maxLength();
      int length;
      Lines.ForEach(line =>
      {
        length = 0;
        line.Instructions.ForEach(instruction =>
        {
          length += instruction.Text.Length;
        });
        if (length < max)
        {
          line.Add(new String(' ', max - length));
        }
      });
    }

    public void RightJoin(PrintInstructions instructions)
    {
      int spaces = _maxLength();
      int myMax = Lines.Count - 1;
      int insMax = instructions.Lines.Count - 1;
      int i;

      if (myMax < insMax)
      {
        for (i = 0; i < insMax - myMax; i++)
        {
          NewLine(new String(' ', spaces));
        }
      }

      _addTrailingSpaces();

      i = 0;

      Lines.ForEach(line =>
      {
        if (i <= insMax)
        {
          line.Add(instructions.Lines[i]);
          i++;
        }
      });
    }

    public PrintInstructions()
    {
      Lines = new List<PrintInstructionLine>();
    }

    public PrintInstructions(
      string text = "",
      ConsoleColor foreground = ConsoleColor.White,
      ConsoleColor background = ConsoleColor.Black
    )
    {
      Lines = new List<PrintInstructionLine>() { new PrintInstructionLine(text, foreground, background) };
    }


  }
}