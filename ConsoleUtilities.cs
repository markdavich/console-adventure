using System;
using System.Collections.Generic;
using System.Threading;



namespace ConsoleUtilities
{
  public static class Color
  {
    public const ConsoleColor DarkBlue = ConsoleColor.DarkBlue;
    public const ConsoleColor DarkGreen = ConsoleColor.DarkGreen;
    public const ConsoleColor DarkCyan = ConsoleColor.DarkCyan;
    public const ConsoleColor DarkRed = ConsoleColor.DarkRed;
    public const ConsoleColor DarkMagenta = ConsoleColor.DarkMagenta;
    public const ConsoleColor DarkYellow = ConsoleColor.DarkYellow;
    public const ConsoleColor Gray = ConsoleColor.Gray;
    public const ConsoleColor DarkGray = ConsoleColor.DarkGray;
    public const ConsoleColor Blue = ConsoleColor.Blue;
    public const ConsoleColor Green = ConsoleColor.Green;
    public const ConsoleColor Cyan = ConsoleColor.Cyan;
    public const ConsoleColor Red = ConsoleColor.Red;
    public const ConsoleColor Magenta = ConsoleColor.Magenta;
    public const ConsoleColor Yellow = ConsoleColor.Yellow;
    public const ConsoleColor White = ConsoleColor.White;
    public const ConsoleColor Black = ConsoleColor.Black;
  }

  public static class Glyph
  {
    // Paste these in the console to see how they look
    // _ ﹏ ~ 〜 † ‡ ¶ § ⁂ ※ ☞ ⁑ ⁕ ⁖ ⁘ ⁙ ⁛ ⁜ * ⁎ ∗ ★ ❗ ❕ ❢ ❣ ! ¡ ‼ ！ ՜ ǃ ᥄ ꜟ ꜝ ꜞ ߹ ︕ ﹗ ❓ ❔ ? ¿ ⁇ ؟
    // ✓ ✔ ⍻ 🗸 ✗ ✘ 𐄂 ❌ ❎ ☐ ☑ 🗹 ⮽ 🗳 ☒ 🗴 🗵 🗶 🗷 • ◦ ‣ ⦿ ⦾ ⁃ ◘ ❥ ⁌ ⁍ ☙ ❧ ◉ ◎ ⮾ ⮿
    // ⯀ ■ ⬛ ◼ ◾ 🞍 ▪ 🞌 □ ⬜ ◻ ◽ ▫ 🞎 🞏 🞐 🞑 🞒 🞓 🞔 🞕 🞖 ▢ ⧆ ⧇ ❏ ❐ ❑ ❒ ⧈ ▣
    // ▲ ▼ ◀ ▶ △ ▽ ◁ ▷ ▴ ▾ ◂ ▸ ▵ ▿ ◃ ▹ ⛛ ◄ ► ◅ ▻ 🞀 🞂 🞁 🞃
    // 🞄 ● ⚫ ⬤ ⚬ ○ ⚪ ◯ ◌ ❍ 🔾 🔿 🞅 🞆 🞇 🞈 🞉 🞊 🞋 ◴ ◵ ◶ ◷ ◔ ◕ ◖ ◗ ⯊ ⯋ ◐ ◑ ◓ ◒
    // 👍 👎 🖒 🖓 👌 🖏 ✌ 🖔 ☜ ☞ ☝ ☟ 🖗 👈 👉 👆 👇 🖘 🖙 🖞 🖟
    // ✻ ✼ ✽ ❃ ❉ ✢ ✣ ✤ ✥ 🞯 🞰 🞱 🞲 🞳 🞴 🞵 🞶 🞷 🞸 🞹 🞺 🞻 🞼 🞽 🞾 🞿 ✳ ❊ ❋ ✺ ❇ ❈ ✨ ❄ ❅ ❆ ✿ ❀ ❁ ✾
    // ✦ ✧ 🟄 🟅 🟆 🟇 🟈 🟋 🟌 🟍 ✶ ✡ ❂ ✴ ✵ ✷ ✸ 🟎 🟏 🟐 🟑 ✹ 🟒 🟓 🟔
    // ↵ ↩ ⏎ ⮐ ⮑ ⮒ ⮓ @ © 🄯 ® ℗ ™ ℠ 🙨 🙩 🙪 🙫 💰 💳 😃

    // 🐭
    // ┃┣━━━┳━━━━┳━━┓
    // ┃┗┓┏┛┃╻╺━━┛╺┓┃
    // ┣┓┃┗┓┗┻━━━┳╸┃┃
    // ┃┃┣╸┣━━┳━┓┗━┛┃
    // ┃┃┃┏┛┏╸┃╻┣━┳╸┃
    // ┃┗━┫╻┣━━┫┗╸┃┏┫
    // ┃┏━┫┃┃╺┓┗━┓┃┃┃
    // ┃┃┃┃┃┗┓┗━┓┗┻╸┃
    // ┗━┫┏┻━━━━┻━━━┛

    public const string CheckTrue = "🗹";
    public const string CheckFalse = "▢ ";
    public const string RadioTrue = "🞊";
    public const string RadioFalse = "🞅";
    public const string ArrowUp = "🠉";
    public const string ArrowDown = "🠋";
    public const string ArrowLeft = "🠈";
    public const string ArrowRight = "🠊";
    public const string LineIndicator = "🞂";
    public const ConsoleColor TrueColor = Color.Green;
    public const ConsoleColor FalseColor = Color.DarkGray;

    public static string GetInicator(bool isCurrentLine)
    {
      if (isCurrentLine)
      {
        return LineIndicator;
      }
      else
      {
        return new String(' ', LineIndicator.Length);
      }
    }

  }

  public class LogText
  {
    public string Text { get; private set; }
    public ConsoleColor Foreground { get; private set; }

    public ConsoleColor Background { get; private set; }

    public LogText(string text, ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black)
    {
      this.Text = text;
      this.Foreground = foreground;
      this.Background = background;
    }
  }
  public class Log
  {
    private const ConsoleColor CONSOLE_FOREGROUND = Color.White;
    private const ConsoleColor CONSOLE_BACKGROUND = Color.Black;
    List<LogText> log = new List<LogText>();
    private ConsoleColor fg { get; set; }

    public static void PrintColors()
    {
      ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
      foreach (ConsoleColor color in colors)
      {
        Console.ForegroundColor = color;
        Console.WriteLine($"{color}");
      }

      Console.ResetColor();
    }

    public static void ClearLine()
    {
      int top = Console.CursorTop;
      Console.SetCursorPosition(0, top);
      Console.Write(new string(' ', Console.WindowWidth));
      // Console.Write($"\r{new String(' ', Console.WindowWidth)}");
      Console.SetCursorPosition(0, top);

    }

    public static void ClearLineFrom(int left)
    {
      int top = Console.CursorTop;
      Console.SetCursorPosition(left, top);
      Console.Write(new string(' ', Console.WindowWidth - left));
      // Console.Write($"\r{new String(' ', Console.WindowWidth - left)}");
      Console.SetCursorPosition(left, top);
    }

    public static void ClearRemainingLine()
    {
      ClearLineFrom(Console.CursorLeft);
    }

    public Log(
      ConsoleColor foreground = Color.White,
      ConsoleColor background = Color.Black
    )
    {
      fg = foreground;
      Console.ForegroundColor = foreground;
      Console.BackgroundColor = background;
    }

    public void Add(string text)
    {
      log.Add(new LogText(text, CONSOLE_FOREGROUND, CONSOLE_BACKGROUND));
    }

    public void Add(string text, ConsoleColor foreground, ConsoleColor background = CONSOLE_BACKGROUND)
    {
      log.Add(new LogText(text, foreground, background));
    }

    public void Add(char c, ConsoleColor foreground, ConsoleColor background = CONSOLE_BACKGROUND)
    {
      Add(c.ToString(), foreground, background);
    }

    public void Print(bool addNewLine = true)
    {

      foreach (var item in log)
      {
        Console.BackgroundColor = item.Background;
        Console.ForegroundColor = item.Foreground;
        Console.Write($"{item.Text}");
      }

      // Console.Write($"--TEST--CursorTop = {Console.CursorTop}");

      Console.ResetColor();
      Console.ForegroundColor = fg;

      if (addNewLine)
      {
        // Console.Write("\r\n");
        Console.WriteLine("");
      }

      log.Clear();
      log.TrimExcess();
    }

    public void Print(string text, ConsoleColor color = CONSOLE_FOREGROUND)
    {
      Console.ForegroundColor = color;
      Console.WriteLine(text);
      Console.ForegroundColor = fg;
    }

    public void NewLine(int numberOfBlankLines = 1)
    {
      for (int i = 0; i < numberOfBlankLines; i++)
      {
        Console.WriteLine(new String(' ', Console.BufferWidth - Console.CursorLeft));
      }
    }
  }

  public class LogService
  {
    private List<LogText> _list { get; set; }

    private ConsoleColor fg { get; set; }

    public List<LogText> Data
    {
      get
      {
        List<LogText> result = new List<LogText>();
        _list.ForEach(logEntry =>
        {
          result.Add(logEntry);
        });
        return result;
      }
    }

    public void Add(string text, ConsoleColor color = Color.Gray)
    {
      _list.Add(new LogText(text, color));
    }

    public LogService()
    {
    }
  }

  public class LogController
  {
    private Log log { get; set; }
    public void Print(List<LogText> data)
    {
      data.ForEach(element =>
      {
        log.Add(element.Text, element.Foreground);
      });
      log.Print(true);
    }

    public void NewLine()
    {
      log.NewLine();
    }

    public LogController(
      ConsoleColor foreground = Color.Gray,
      ConsoleColor background = Color.Black
    )
    {
      log = new Log(foreground, background);
    }
  }

  public class LogMarquee
  {
    private ConsoleColor fg;
    private ConsoleColor bg;
    private string _text;
    public volatile bool running;
    private int cursorTop;

    public void Stop()
    {
      running = false;
      marquee.Join();
    }

    private Thread Marquee()
    {
      return new Thread(() =>
        {
          int width = _text.Length * 2;
          int length = _text.Length;
          int pos = 0;
          string firstPart;
          string gap = new String(' ', length);
          string secondPart = "";
          string line = "";

          Console.CursorVisible = false;

          while (running)
          {
            int currentTop = Console.CursorTop;
            int currentLeft = Console.CursorLeft;

            if (pos == width)
            {
              pos = 0;
            }

            if (pos > length)
            {
              int offset = pos - length;
              firstPart = _text.Substring(length - offset, offset);
              secondPart = _text.Substring(0, length - offset);
              line = firstPart + gap + secondPart;
            }
            else
            {
              firstPart = new String(' ', pos);
              line = firstPart + _text + new String(' ', width - (firstPart.Length + length));
            }

            Console.CursorTop = cursorTop;
            Console.CursorLeft = 0;
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write($"\r{new String(' ', width)}");
            Console.Write($"\r{line}");

            Console.CursorLeft = currentLeft;
            Console.CursorTop = currentTop;
            Console.ResetColor();

            pos++;

            Thread.Sleep(200);
          }

          Console.CursorVisible = true;
        }
      );
    }

    public Thread marquee { get; set; }
    public LogMarquee(
      string text,
      ConsoleColor foreground = Color.Yellow,
      ConsoleColor background = Color.DarkMagenta
    )
    {

      fg = foreground;
      bg = background;
      _text = text;
      running = true;
      cursorTop = Console.CursorTop;
      Console.WriteLine(""); // The Marquee will go here.
      marquee = Marquee();
      marquee.Start();
    }

    public void NewMessage(
      string text,
      ConsoleColor foreground = Color.Yellow,
      ConsoleColor background = Color.DarkMagenta
    )
    {
      fg = foreground;
      bg = background;
      _text = text;
      running = true;
      cursorTop = Console.CursorTop;
      Console.WriteLine(""); // The Marquee will go here.
      marquee = Marquee();
      marquee.Start();
    }
  }

  public class ConsoleConfig
  {
    public bool IsHeader { get; set; }
    public int LineWidth { get; set; }
    public int ValueWidth { get; set; }
    public int MaxDisplayWidth { get; set; }
    public int CountWidth { get; set; }
    public ConsoleColor BackgroundColor { get; set; }
    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor HeaderBackgroundColor { get; set; }
    public ConsoleColor HeaderForegroundColor { get; set; }
    public ConsoleColor TrueColor { get; set; }
    public ConsoleColor FalseColor { get; set; }
    public ConsoleColor CurLineIndicatorColor { get; set; }
    public ConsoleColor CountColor { get; set; }
    public ConsoleColor ValueColor { get; set; }
    public ConsoleColor ListTitleColor { get; set; }
    public ConsoleColor TotalColor { get; set; }
    public ConsoleColor TextInputTagColor { get; set; }
    public ConsoleColor TextInputColor { get; set; }
    public ConsoleColor SelectableLineColor { get; set; }
    public ConsoleColor SelectableLineTextColor { get; set; }
    public bool Drawn { get; set; }

    public ConsoleColor CheckColor(bool isChecked)
    {
      return isChecked ? TrueColor : FalseColor;
    }

    public const string Format = "LineIndicatorCheckTrue MaxDisplayWidth ValueWidth CountWidth ";

    public int Width
    {
      get
      {
        int result
          = Glyph.LineIndicator.Length
          + Glyph.CheckTrue.Length
          + " ".Length
          + MaxDisplayWidth
          + " ".Length
          + ValueWidth
          + " ".Length
          + CountWidth
          + " ".Length;

        return result;
      }
    }

    public ConsoleConfig()
    {
      Drawn = false;
      Console.ResetColor();
      LineWidth = Console.WindowWidth;
      ValueWidth = 6;
      MaxDisplayWidth = 25;
      CountWidth = 3;
      BackgroundColor = Console.BackgroundColor;
      ForegroundColor = Console.ForegroundColor;
      HeaderBackgroundColor = Color.DarkGray;
      HeaderForegroundColor = Color.Black;
      TrueColor = Color.Green;
      FalseColor = Color.DarkGray;
      CurLineIndicatorColor = Color.Yellow;
      CountColor = Color.DarkCyan;
      ValueColor = Color.Magenta;
      ListTitleColor = Color.Cyan;
      TextInputColor = Color.Yellow;
      TextInputTagColor = Color.DarkGray;
      SelectableLineColor = CurLineIndicatorColor;
      SelectableLineTextColor = Color.DarkGray;
      TotalColor = Color.Red;
    }
  }

  public abstract class ConsoleDrawable
  {
    public enum BoolCheckType { CheckBox, Radio, None };

    public BoolCheckType CheckType { get; }

    public virtual string Display { get; set; }

    public abstract void Draw(
      bool isCurrentLine,
      Log log,
      ConsoleConfig config
    );

    public ConsoleDrawable(string text, BoolCheckType boolCheckType = BoolCheckType.None)
    {
      Display = text;
      CheckType = boolCheckType;
    }
  }

  public class SelectableLine : ConsoleDrawable
  {
    public SelectableLine(string text) : base(text, BoolCheckType.None)
    {
    }

    public override void Draw(bool isCurrentLine, Log log, ConsoleConfig config)
    {
      ConsoleColor fg = config.SelectableLineColor;
      ConsoleColor bg = config.BackgroundColor;

      if (isCurrentLine)
      {
        fg = config.SelectableLineTextColor;
        bg = config.SelectableLineColor;
      }

      Console.ForegroundColor = config.CurLineIndicatorColor;
      Console.Write(Glyph.GetInicator(isCurrentLine));

      Console.BackgroundColor = bg;
      Console.ForegroundColor = fg;

      string text = Display + new String(' ', config.Width - Glyph.GetInicator(isCurrentLine).Length - Display.Length);
      Console.Write(text);
      Console.ResetColor();
    }

  }

  public class TextInput : ConsoleDrawable
  {
    public int DisplayLength { get; set; }
    public string Value { get; set; }
    public void ProcessText(string text)
    {
      Value = text;
    }
    public override void Draw(bool isCurrentLine, Log log, ConsoleConfig config)
    {
      log.Add(Glyph.GetInicator(isCurrentLine), config.CurLineIndicatorColor);
      log.Add(new String(' ', DisplayLength - Display.Length));
      log.Add($"{Display} ", config.TextInputTagColor);
      log.Add(Value, config.TextInputColor);
      log.Add(new String(' ', Console.WindowWidth - Console.CursorLeft));
      log.Print(false);
      Console.ResetColor();
    }

    public TextInput(int displayLength, string text) : base(text, BoolCheckType.None)
    {
      DisplayLength = displayLength;
    }
  }

  public class Total : ConsoleDrawable
  {
    public override void Draw(bool isCurrentLine, Log log, ConsoleConfig config)
    {
      int width = config.Width - 1;
      string total = Value.ToString("C2");
      string spaces = new String(' ', width - total.Length - Display.Length - 1 - config.CountWidth - 1);
      log.Add(spaces);
      log.Add(Display, config.TotalColor);
      log.Add(" ");
      log.Add(total, config.TotalColor);
      log.Add(" ");
      log.Print(false);
    }

    public float Value { get; set; }

    public void Add(float value)
    {
      Value += value;
    }

    public Total(string text = "Total") : base(text)
    {
      Value = 0.00f;
    }
  }

  public class ConsoleHeader : ConsoleDrawable
  {
    private string EndSpace(ConsoleConfig config)
    {
      return new String(
        ' ',
        Glyph.CheckTrue.Length +
        Glyph.LineIndicator.Length +
        config.MaxDisplayWidth
        - Display.Length + 1
        + config.ValueWidth + 1
        + config.CountWidth + 1
      );
    }
    public override void Draw(bool isCurrentLine, Log log, ConsoleConfig config)
    {
      Console.ResetColor();
      Console.BackgroundColor = config.HeaderBackgroundColor;
      log.Add(" ");
      log.Add(Display, config.HeaderForegroundColor);
      log.Add(EndSpace(config));
      log.Print(false);
      Console.ResetColor();
    }
    public ConsoleHeader(string text) : base(text)
    {

    }
  }

  public class ListTitle : ConsoleDrawable
  {
    public override void Draw(bool isCurrentLine, Log log, ConsoleConfig config)
    {
      Console.ResetColor();
      log.Add(Display, config.ListTitleColor);
      // log.Print(!config.Drawn);
      log.Print(false);

    }
    public ListTitle(string text) : base(text)
    {

    }
  }

  public class BoolInput : ConsoleDrawable
  {
    public virtual bool Checked { get; set; }
    public string GlyphTrue { get; set; }
    public string GlyphFalse { get; set; }
    public string Check { get { return Checked ? GlyphTrue : GlyphFalse; } }
    public virtual void ToggleBoolValue()
    {
      Checked = !Checked;
    }

    private string endSpace(ConsoleConfig config)
    {
      int totalLen;
      if (config.IsHeader)
      {
        // _ denotes spaces
        // MAX-DISPLAY-WIDTH_VALUE-WIDTH_COUNT-WIDTH_
        totalLen = config.MaxDisplayWidth + 1 + config.ValueWidth + 1 + config.CountWidth + 1;
      }
      else
      {
        totalLen = config.MaxDisplayWidth;
      }

      return new String(' ', totalLen - Display.Length);
    }
    public override void Draw(bool isCurrentLine, Log log, ConsoleConfig config)
    {
      Console.ResetColor();
      ConsoleColor fg;

      if (config.IsHeader)
      {
        Console.BackgroundColor = config.HeaderBackgroundColor;
        fg = config.HeaderForegroundColor;
      }
      else
      {
        fg = Console.ForegroundColor;
      }

      log.Add(Glyph.GetInicator(isCurrentLine), config.CurLineIndicatorColor);
      log.Add(Check, config.CheckColor(Checked));
      log.Add(" ");
      log.Add(Display, fg);
      log.Add(endSpace(config));
      log.Print(false);

      Console.ResetColor();
    }
    public BoolInput(string text, ConsoleDrawable.BoolCheckType type = ConsoleDrawable.BoolCheckType.CheckBox) : base(text, type)
    {
      switch (type)
      {
        case ConsoleDrawable.BoolCheckType.CheckBox:
          GlyphTrue = Glyph.CheckTrue;
          GlyphFalse = Glyph.CheckFalse;
          break;
        case ConsoleDrawable.BoolCheckType.Radio:
          GlyphTrue = Glyph.RadioTrue;
          GlyphFalse = Glyph.RadioFalse;
          break;
      }
    }
  }

  // public abstract class BoolValueInput<T> : BoolInput
  // {
  //   public virtual T Value { get; set; }

  //   public BoolValueInput(
  //     string text,
  //     T value,
  //     ConsoleDrawable.BoolCheckType checkType = ConsoleDrawable.BoolCheckType.CheckBox
  //   ) : base(text, checkType)
  //   {
  //     Value = value;
  //   }
  // }

  public class RadioInput : BoolInput
  {
    public RadioInput(string text) : base(text, BoolCheckType.Radio)
    {

    }
  }

  public class ColorInput : RadioInput
  {

    private string endSpace(ConsoleConfig config)
    {
      int totalLen = config.MaxDisplayWidth;
      return new String(' ', totalLen - Display.Length);
    }
    public override void Draw(bool isCurrentLine, Log log, ConsoleConfig config)
    {
      Console.ResetColor();
      ConsoleColor color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Display);

      log.Add(Glyph.GetInicator(isCurrentLine), config.CurLineIndicatorColor);
      log.Add(Check, config.CheckColor(Checked));
      log.Add(" ");
      log.Add(Display, color);
      log.Add(endSpace(config));
      log.Print(false);

      Console.ResetColor();
    }
    public ColorInput(string text) : base(text)
    {

    }
  }

  public class BoolInputCounter : BoolInput
  {

    private bool _Checked;
    public int Count { get; private set; }

    public override bool Checked
    {
      get { return _Checked; }
      set
      {
        _Checked = value;

        if (value)
        {
          Count = 1;
        }
        else
        {
          Count = 0;
        }
      }
    }
    public void Inc()
    {
      if (Checked)
      {
        Count++;
      }
    }
    public void Dec()
    {
      if (Checked && Count > 1)
      {
        Count--;
      }
    }
    public override void ToggleBoolValue()
    {
      Checked = !Checked;
    }
    private string Dots(ConsoleConfig config)
    {
      int totalLen;
      // _ denotes spaces
      // MAX-DISPLAY-WIDTH_VALUE-WIDTH_COUNT-WIDTH_
      // DISPLAY .......... 001_
      totalLen = config.MaxDisplayWidth + 1 + config.ValueWidth + 1 + config.CountWidth + 1;

      return new String('.', totalLen - Display.Length - 1 - 1 - config.CountWidth - 1);
    }

    private string CountGap(ConsoleConfig config)
    {
      return new String(' ', config.CountWidth - Count.ToString().Length);
    }
    public override void Draw(bool isCurrentLine, Log log, ConsoleConfig config)
    {
      Console.ResetColor();
      ConsoleColor fg;

      if (config.IsHeader)
      {
        Console.BackgroundColor = config.HeaderBackgroundColor;
        fg = config.HeaderForegroundColor;
      }
      else
      {
        fg = Console.ForegroundColor;
      }

      log.Add(Glyph.GetInicator(isCurrentLine), config.CurLineIndicatorColor);
      log.Add(Check, config.CheckColor(Checked));
      log.Add(" ");
      log.Add(Display, fg);
      log.Add(" ");
      log.Add(Dots(config));
      log.Add(" ");
      log.Add(CountGap(config));
      log.Add(Count.ToString(), config.CountColor);
      log.Add(" ");
      log.Print(false);

      Console.ResetColor();
    }
    public BoolInputCounter(
      string text,
      ConsoleDrawable.BoolCheckType checkType = ConsoleDrawable.BoolCheckType.CheckBox
    ) : base(text, checkType)
    {

    }
  }

  public class MonitaryBoolCounter : BoolInputCounter// BoolValueInput<float>
  {
    public float Value { get; set; }

    // private bool _Checked;
    // public int Count { get; private set; }
    // public override bool Checked
    // {
    //   get { return _Checked; }
    //   set
    //   {
    //     _Checked = value;

    //     if (value)
    //     {
    //       Count = 1;
    //     }
    //     else
    //     {
    //       Count = 0;
    //     }
    //   }
    // }
    // public override void ToggleBoolValue()
    // {
    //   Checked = !Checked;
    // }
    // public void Inc()
    // {
    //   if (Checked)
    //   {
    //     Count++;
    //   }
    // }
    // public void Dec()
    // {
    //   if (Checked && Count > 1)
    //   {
    //     Count--;
    //   }
    // }

    private string DisplayValueGap(ConsoleConfig config)
    {
      int displayGap = config.MaxDisplayWidth - Display.Length + 1;
      int valueGap = config.ValueWidth - Money().Length;
      return new String(' ', displayGap + valueGap);
    }

    private string CountGap(ConsoleConfig config)
    {
      int spaceCount = config.CountWidth - Count.ToString().Length + 1;
      return new String(' ', spaceCount);
    }

    private string Money()
    {
      return Value == 0 ? "Free" : Value.ToString("C2");
    }

    public override void Draw(
      bool isCurrentLine,
      Log log,
      ConsoleConfig config
    )
    {
      Console.ResetColor();
      ConsoleColor fg;

      if (config.IsHeader)
      {
        Console.BackgroundColor = config.HeaderBackgroundColor;
        fg = config.HeaderForegroundColor;
      }
      else
      {
        fg = Console.ForegroundColor;
      }

      log.Add(Glyph.GetInicator(isCurrentLine), config.CurLineIndicatorColor);
      log.Add(Check, config.CheckColor(Checked));
      log.Add(" ");
      log.Add(Display, fg);
      log.Add(DisplayValueGap(config));
      log.Add(Money(), config.ValueColor);
      log.Add(CountGap(config));
      log.Add(Count.ToString(), config.CountColor);
      log.Add(" ");
      // log.Print(!config.Drawn);
      log.Print(false);

      Console.ResetColor();
    }

    public MonitaryBoolCounter(
      string text,
      float value = 0.00f,
      ConsoleDrawable.BoolCheckType type = ConsoleDrawable.BoolCheckType.CheckBox
    ) : base(text, type)
    {
      Value = value;
    }
  }

  public class CanvasBlockList
  {
    public ConsoleDrawable Header { get; set; }
    public virtual List<ConsoleDrawable> Items { get; set; }

    public void Add(ConsoleDrawable item)
    {
      Items.Add(item);
    }

    public void SetAll(bool allChecked = false)
    {
      foreach (var item in Items)
      {
        if (item is BoolInput)
        {
          (item as BoolInput).Checked = allChecked;
        }
      }
    }

    public CanvasBlockList(string header)
    {
      Header = new ListTitle(header);
      Items = new List<ConsoleDrawable>();
    }
  }

  // private void Method<TEnum>()
  //     where TEnum : struct, IConvertible, IComparable, IFormattable
  // {
  //   if (!typeof(TEnum).IsEnum)
  //   {
  //     throw new ArgumentException("TEnum must be an enum.");
  //   }

  //   // ...
  // }

  public class RadioCanvasBlockList<TValueType> : CanvasBlockList
  {
    private List<TValueType> Values = new List<TValueType>();

    private TValueType InitialValue { get; set; }

    public TValueType Value
    {
      get
      {
        TValueType result = InitialValue;

        for (int i = 0; i < Items.Count; i++)
        {
          if ((Items[i] as RadioInput).Checked)
          {
            result = Values[i];
            break;
          }
        }

        return result;
      }
    }

    public RadioCanvasBlockList(string header, Dictionary<string, TValueType> namesAndValues) : base(header)
    {
      int counter = 0;
      foreach (KeyValuePair<string, TValueType> entry in namesAndValues)
      {
        if (counter == 0)
        {
          InitialValue = entry.Value;
        }
        Items.Add(new RadioInput(entry.Key));
        Values.Add(entry.Value);
      }

      (Items[0] as RadioInput).Checked = true;
    }
  }

  public class RadioEnumCanvasBlockList<TEnum> : CanvasBlockList
    where TEnum : struct, IConvertible, IComparable, IFormattable
  {
    private TEnum InitialValue;

    private List<TEnum> Values { get; set; } = new List<TEnum>();

    public TEnum Value
    {
      get
      {
        TEnum result = InitialValue;

        for (int i = 0; i < Items.Count; i++)
        {
          if ((Items[i] as RadioInput).Checked)
          {
            result = Values[i];
            break;
          }
        }

        return result;
      }
    }

    public RadioEnumCanvasBlockList(string header, TEnum initialValue, ISet<TEnum> exclusions = null) : base(header)
    {
      InitialValue = initialValue;

      foreach (TEnum e in Enum.GetValues(typeof(TEnum)))
      {
        if (exclusions != null)
        {
          if (exclusions.Contains(e)) continue;
        }
        RadioInput radio = new RadioInput(Enum.GetName(typeof(TEnum), e));
        if (e.Equals(initialValue))
        {
          radio.Checked = true;
        }
        Items.Add(radio);
        Values.Add(e);
      }
    }

    public RadioEnumCanvasBlockList(string header, Dictionary<TEnum, string> valuesAndDisplayNames) : base(header)
    {
      foreach (KeyValuePair<TEnum, string> entry in valuesAndDisplayNames)
      {
        Items.Add(new RadioInput(entry.Value));
        Values.Add(entry.Key);
      }

      (Items[0] as RadioInput).Checked = true;
    }
  }

  public class CanvasColorBlockList : CanvasBlockList
  {
    private List<ConsoleColor> Values { get; set; } = new List<ConsoleColor>();
    private ConsoleColor InitialValue { get; set; }

    public ConsoleColor Value
    {
      get
      {
        ConsoleColor result = InitialValue;

        for (int i = 0; i < Items.Count; i++)
        {
          if ((Items[i] as RadioInput).Checked)
          {
            result = Values[i];
            break;
          }
        }

        return result;
      }
    }
    public CanvasColorBlockList(string text, ConsoleColor initialValue = ConsoleColor.Yellow) : base(text)
    {
      InitialValue = initialValue;

      foreach (ConsoleColor c in ConsoleColor.GetValues(typeof(ConsoleColor)))
      {
        ColorInput radio = new ColorInput(Enum.GetName(typeof(ConsoleColor), c));
        if (c.Equals(initialValue))
        {
          radio.Checked = true;
        }
        Items.Add(radio);
        Values.Add(c);
      }
    }
  }

  public class CanvasBlock
  {
    public ConsoleDrawable Header { get; set; }
    public List<CanvasBlockList> Lists { get; set; }

    public void Add(CanvasBlockList list)
    {
      Lists.Add(list);
    }

    public CanvasBlock(ConsoleDrawable header)
    {
      Header = header;
      Lists = new List<CanvasBlockList>();
    }
  }

  public class CanvasBlocks
  {
    public List<CanvasBlock> Blocks { get; set; }

    public void Add(CanvasBlock block)
    {
      Blocks.Add(block);
    }
    public CanvasBlocks()
    {
      Blocks = new List<CanvasBlock>();
    }
  }

  public static class ConCan
  {
    public enum Answer { Continue, Back, Quit, Yes, No }
  }


  public class ConsoleCanvas
  {
    private bool DrawTotal { get; set; }
    private bool Drawn { get; set; }
    private int TopLine { get; set; }
    private int CurLine { get; set; }
    private int BottomeLine;
    private Dictionary<int, ConsoleDrawable> Lines = new Dictionary<int, ConsoleDrawable>();
    private Dictionary<int, bool> Headers = new Dictionary<int, bool>();
    public Dictionary<int, CanvasBlockList> Options = new Dictionary<int, CanvasBlockList>();
    private List<CanvasBlock> Blocks { get; set; } = new List<CanvasBlock>();

    private Dictionary<int, TextInput> TextInput { get; set; } = new Dictionary<int, TextInput>();

    public ConsoleConfig config = new ConsoleConfig();

    private Log log = new Log();

    private void DoDrawTotal(int lineNumber)
    {
      Console.SetCursorPosition(0, lineNumber);
      Console.CursorTop = lineNumber;
      log.NewLine();
      Total total = new Total();
      foreach (ConsoleDrawable line in Lines.Values)
      {
        if (line is MonitaryBoolCounter)
        {
          MonitaryBoolCounter mbc = (line as MonitaryBoolCounter);
          total.Add(mbc.Value * mbc.Count);
        }
      }
      total.Draw(false, log, config);
    }

    private void Initialize()
    {
      Lines.Clear();
      Headers.Clear();
      int curLine = TopLine;
      int maxDisplayWidth = 0;

      foreach (var block in Blocks)
      {
        if (block.Header.Display.Length > maxDisplayWidth)
        {
          maxDisplayWidth = block.Header.Display.Length;
        }
        Headers.Add(curLine, true);
        Lines.Add(curLine, block.Header);
        curLine++;
        foreach (var list in block.Lists)
        {
          Lines.Add(curLine, list.Header);
          curLine++;
          foreach (var drawable in list.Items)
          {
            Lines.Add(curLine, drawable);

            if (drawable.Display.Length > maxDisplayWidth)
            {
              maxDisplayWidth = drawable.Display.Length;
            }
            if (drawable.CheckType == ConsoleDrawable.BoolCheckType.Radio)
            {
              Options.Add(curLine, list);
            }
            if (drawable is TextInput)
            {
              TextInput.Add(curLine, drawable as TextInput);
            }
            curLine++;
          }
        }
      }

      // foreach (var item in Lines)
      // {
      //   Console.BackgroundColor = Color.DarkYellow;
      //   Console.WriteLine(new String(' ', Console.WindowWidth));
      //   System.Threading.Thread.Sleep(10);
      // }


      // Console.WriteLine(" ");
      // Console.WriteLine(" ");
      // Console.WriteLine(" ");
      // Console.WriteLine(" ");
      // Console.WriteLine("THIS SHOULD BE THE END OF THE FILE");
      // System.Threading.Thread.Sleep(10);

      Console.SetCursorPosition(0, TopLine);
      Console.CursorTop = TopLine;
      // Console.WindowTop = TopLine; // Doesn't work on linux

      // System.Threading.Thread.Sleep(10);


      BottomeLine = curLine - 1;
      config.MaxDisplayWidth = maxDisplayWidth;
    }
    public void Draw()
    {

      int curLine = TopLine;

      // This is hackery to try to get the group to render
      // when it goes past the end of the page
      // Console.SetCursorPosition(0, curLine);
      // Console.CursorTop = curLine;

      foreach (KeyValuePair<int, ConsoleDrawable> entry in Lines)
      {
        // if (Drawn) // If we have drawn it we need to set the CursorTop...
        {
          Console.SetCursorPosition(0, curLine);
          Console.CursorTop = curLine;
        }


        config.IsHeader = Headers.ContainsKey(entry.Key);
        entry.Value.Draw(entry.Key == CurLine, log, config);

        // if (!Drawn)
        // {
        //   Console.WriteLine(new String(' ', Console.WindowWidth));
        // }

        curLine++;
      }

      if (DrawTotal)
      {
        DoDrawTotal(curLine);
      }

      Drawn = true;
      config.Drawn = true;

      Console.CursorTop = CurLine;
    }

    private void ProcessTextInput()
    {
      Console.CursorVisible = true;
      Console.CursorLeft = TextInput[CurLine].DisplayLength + 3;

      ConsoleKeyInfo keyInfo = Console.ReadKey();
      switch (keyInfo.Key)
      {
        case ConsoleKey.UpArrow:
          if (CurLine > TopLine)
          {
            CurLine--;
            Draw();
          }
          break;
        case ConsoleKey.DownArrow:
          if (CurLine < BottomeLine)
          {
            CurLine++;
            Draw();
          }
          break;
        case ConsoleKey.Enter:
          break;
        default:
          Console.Write("\b");
          Console.Write(" ");
          Console.Write("\b");
          TextInput[CurLine].ProcessText(Console.ReadLine());
          Draw();
          break;
      }
      System.Threading.Thread.Sleep(10);
    }

    public ConCan.Answer GetInput(bool resetTopLine = false, bool drawTotal = false)
    {
      if (resetTopLine)
      {
        TopLine = Console.CursorTop;
      }

      CurLine = TopLine + 2;

      DrawTotal = drawTotal;

      Console.CursorVisible = false;
      Initialize();
      Draw();

      System.Threading.Thread.Sleep(100);
      while (true)
      {
        while (TextInput.ContainsKey(CurLine))
        {
          ProcessTextInput();
        }

        Console.CursorVisible = false;

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
          case ConsoleKey.UpArrow:
            if (CurLine > TopLine)
            {
              CurLine--;
              Draw();
            }
            break;
          case ConsoleKey.DownArrow:
            if (CurLine < BottomeLine)
            {
              CurLine++;
              Draw();
            }
            break;
          case ConsoleKey.LeftArrow:
          case ConsoleKey.RightArrow:
            if (Lines[CurLine] is BoolInputCounter)
            {
              switch (keyInfo.Key)
              {
                case ConsoleKey.LeftArrow:
                  (Lines[CurLine] as BoolInputCounter).Dec();
                  break;
                default:
                  (Lines[CurLine] as BoolInputCounter).Inc();
                  break;
              }
              Draw();
            }
            break;
          case ConsoleKey.Spacebar:
            switch (Lines[CurLine].CheckType)
            {
              case ConsoleDrawable.BoolCheckType.CheckBox:
                (Lines[CurLine] as BoolInput).ToggleBoolValue();
                Draw();
                break;
              case ConsoleDrawable.BoolCheckType.Radio:
                Options[CurLine].SetAll();
                (Lines[CurLine] as BoolInput).Checked = true;
                Draw();
                break;
            }
            break;
          case ConsoleKey.Q:
            Console.CursorLeft = 0;
            Console.CursorTop = BottomeLine + 1;
            return ConCan.Answer.Quit;
          case ConsoleKey.Escape:
            return ConCan.Answer.Back;
          case ConsoleKey.Enter:
            return ConCan.Answer.Continue;

        }
      }
    }

    public void AddBlock(CanvasBlock block)
    {
      Blocks.Add(block);
    }
    public ConsoleCanvas()
    {
      Drawn = false;
      DrawTotal = false;
      TopLine = Console.CursorTop;
      // Console.WriteLine($"ConsoleCanvas Constructor cursorTop = {TopLine}");

      // Console.SetCursorPosition(0, TopLine - 3);
      // Console.Write("-- Hey There --");

      CurLine = TopLine;

    }
  }
}