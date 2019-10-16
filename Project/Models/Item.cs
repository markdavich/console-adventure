using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleParams = ConsoleAdventure.Models.ConsoleParams;
using System;
using System.Linq;
using PrintInstruction = System.Collections.Generic.List<ConsoleAdventure.Models.ConsoleParams>;
using PrintInstructions = System.Collections.Generic.List<System.Collections.Generic.List<ConsoleAdventure.Models.ConsoleParams>>;

namespace ConsoleAdventure.Project.Models
{
  public class Item : IItem
  {
    public bool IsTakeable { get; set; } = true;

    public List<Type> UsedOn = new List<Type>();
    public ConsoleColor Color { get; set; }

    public virtual PrintInstructions UseMessage
    {
      get
      {
        PrintInstructions result = new PrintInstructions() { new PrintInstruction() };
        result[0].Add(new ConsoleParams($"You use the {Name}", ConsoleColor.Green));
        return result;
      }
    }

    public virtual PrintInstructions TakeMessage
    {
      get
      {
        PrintInstructions result = new PrintInstructions() { new PrintInstruction() };
        result[0].Add(new ConsoleParams($"You take the {Name}", ConsoleColor.Green));
        return result;
      }
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Glyph { get; set; }

    public PrintInstruction ItemDescription
    {
      get
      {
        PrintInstruction pi = new PrintInstruction();
        pi.Add(new ConsoleParams("A "));
        pi.Add(new ConsoleParams($"{Color} ", Color));
        pi.Add(new ConsoleParams(Name));
        return pi;
      }
    }

    // "🂠"


    public PrintInstruction Symbol
    {
      get
      {
        PrintInstruction pi = new PrintInstruction();
        pi.Add(new ConsoleParams(Glyph, Color));
        return pi;
      }
    }

    public Item(string name, string glyph, ConsoleColor color)
    {
      Name = name;
      Color = color;
      Glyph = glyph;
    }
  }

  public class Card : Item
  {
    public Card(ConsoleColor color) : base("card", "🂠", color)
    {

    }
  }

  public abstract class NonTakeable : Item
  {
    public virtual string Message
    {
      get
      {
        return $"You can't take the {Name}.";
      }
    }

    public override PrintInstructions UseMessage
    {
      get
      {
        PrintInstructions result = new PrintInstructions() { new PrintInstruction() };
        result[0].Add(new ConsoleParams($"You can't use the {Name}", ConsoleColor.Green));
        return result;
      }
    }

    public NonTakeable(string name, string glyph, ConsoleColor color) : base(name, glyph, color)
    {
      IsTakeable = false;
    }
  }

  // 𓄣 𓉠 𓇝 𓅃 𓄣 𓉠 𓇝 𓅃𓅪🐦 𓆤 🗑

  public class Bird : NonTakeable
  {
    public Bird(ConsoleColor color) : base("Bird", "🐦", color)
    {

    }
  }

  public class Pot : NonTakeable
  {
    public override PrintInstructions UseMessage
    {
      get
      {
        PrintInstructions result = new PrintInstructions() { new PrintInstruction() };
        result[0].Add(new ConsoleParams("You try to take the Pot but it's too heavy.", ConsoleColor.DarkRed));
        return result;
      }
    }
    public Pot() : base("Pot", "🗑", ConsoleColor.DarkYellow)
    {

    }
  }

  public static class Lock
  {
    public enum Key { Phi, Pythagoras, Euler, Archimedes, Feigenbaum, Conway, Fibonacci, Apéry }
  }

  public class Calculator : Item
  {

    public const string ProgramText = @"
  CALCULATOR: PROGRAM ALPHA
  ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
  ┃     ϕ = (1 + √5) / 2 ≈ 1.618   ┃
  ┃     π ≈ 3.141                  ┃
  ┃     e ≈ 2.718                  ┃
  ┃    √2 ≈ 1.414                  ┃
  ┃  ζ(3) ≈ 1.202                  ┃
  ┃     λ ≈ 1.303                  ┃
  ┃     δ ≈ 4.669                  ┃
  ┃     ψ ≈ 3.3598856662           ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛";

    public override PrintInstructions UseMessage
    {
      get
      {
        PrintInstructions result = new PrintInstructions() { new PrintInstruction() };
        result[0].Add(new ConsoleParams("You turn on the calculator and run program Alpha", ConsoleColor.DarkGreen));
        return result;
      }
    }

    public PrintInstructions Display
    {
      get
      {
        PrintInstructions pi = new PrintInstructions();
        string[] lines = ProgramText.Split('\n');
        pi.Add(new PrintInstruction() { new ConsoleParams(lines[1], ConsoleColor.DarkGray) });
        for (int i = 2; i < lines.Length; i++)
        {
          pi.Add(new PrintInstruction() { new ConsoleParams(lines[i], ConsoleColor.Green) });
        }
        return pi;
      }
    }

    public bool On { get; set; } = false;

    public Calculator() : base("Calculator", "🖩", ConsoleColor.DarkGray)
    {
      //                  Phi:    ϕ = (1 + √5) / 2 = 1.618
      //           Archimedes:    π = 3.141
      //     Euler's Constant:    e = Eul 2.718
      //           Pythagoras:   √2 = 1.414
      //     Apéry's Constant: ζ(3) = 1.202
      //               Conway:    λ = 1.303
      //           Feigenbaum:    δ = 4.669
      // Reciprocal Fibonacci:    ψ = 3.3598856662
    }
  }

  public class KeyPadLock : NonTakeable
  {
    public const string KeyPadText = @"
  ╔═════════════════════════╗
  ║                         ║
  ║           __       __   ║
  ║  /|        _)      __)  ║
  ║   |       /__      __)  ║
  ║            __       _   ║
  ║  |_|      |_       |_   ║
  ║    |      __)      |_)  ║
  ║   __       _        _   ║
  ║    /      (_)      (_|  ║
  ║   /       (_)       _|  ║
  ║            _            ║
  ║           / \           ║
  ║           \_/           ║
  ╚═════════════════════════╝ ";
    private Dictionary<Lock.Key, int> Values = new Dictionary<Lock.Key, int>()
    {
      { Lock.Key.Phi, 1618 },
      { Lock.Key.Pythagoras, 1414 },
      { Lock.Key.Euler, 2718 },
      { Lock.Key.Archimedes, 3141 },
      { Lock.Key.Feigenbaum, 4669 },
      { Lock.Key.Conway, 1303 },
      { Lock.Key.Fibonacci, 3360 },
      { Lock.Key.Apéry, 1202 }
    };

    public PrintInstructions Display
    {
      get
      {
        PrintInstructions pi = new PrintInstructions() { };
        string hint = Enum.GetName(typeof(Lock.Key), Key);

        int leadingSpaces = (25 - hint.Length) / 2;
        int trailingSpaces = 25 - leadingSpaces - hint.Length;


        string[] lines = KeyPadText.Split('\n');
        pi.Add(new PrintInstruction() { new ConsoleParams(lines[0], Color) });
        pi.Add(new PrintInstruction() { });
        pi[1].Add(new ConsoleParams("  ║", Color));
        pi[1].Add(new ConsoleParams(new String(' ', leadingSpaces)));
        pi[1].Add(new ConsoleParams(hint, ConsoleColor.DarkRed));
        pi[1].Add(new ConsoleParams(new String(' ', trailingSpaces)));
        pi[1].Add(new ConsoleParams("║", Color));

        for (int i = 2; i < lines.Length; i++)
        {
          pi.Add(new PrintInstruction() { new ConsoleParams(lines[i], Color) });
        }


        return pi;
      }
    }

    public Lock.Key Key { get; private set; }

    public string Hint
    {
      get
      {
        return Enum.GetName(typeof(Lock.Key), Key);
      }
    }

    public bool Unlock(int code)
    {
      return code == Values[Key];
    }

    public KeyPadLock(ConsoleColor color) : base("Numeric Key Pad Lock", "🔒", color)
    {
      Random random = new Random();
      Key = (Lock.Key)(
          random.Next(
            Enum.GetValues(typeof(Lock.Key)).Cast<int>().Min(),
            Enum.GetValues(typeof(Lock.Key)).Cast<int>().Max()
          )
      );
    }

  }
}