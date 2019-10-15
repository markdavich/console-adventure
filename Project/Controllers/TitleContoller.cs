using System;
using ConsoleAdventure.Services;
using ConsoleUtilities;

namespace ConsoleAdventure.Controllers
{
  public class UserInfoBlock
  {
    private ConsoleHeader Header;
    private CanvasBlockList NameList;
    private CanvasColorBlockList ColorList;
    public CanvasBlock Block;

    public string Name
    {
      get
      {
        return (NameList.Items[0] as TextInput).Value;
      }
    }

    public ConsoleColor Color
    {
      get
      {
        return (ColorList.Value);
      }
    }

    public UserInfoBlock()
    {
      Header = new ConsoleHeader("Welcome to the Game");
      Block = new CanvasBlock(Header);

      NameList = new CanvasBlockList("What's your Name?");
      NameList.Add(new TextInput(5, "Name:"));

      ColorList = new CanvasColorBlockList("What's Your Favorite Color?", ConsoleColor.Red);

      Block.Add(NameList);
      Block.Add(ColorList);
    }
  }
  public class TitleController
  {
    public TitleService.Banner Banner { get; set; }
    public string Name { get; private set; }
    private TitleService _titleService { get; set; } = new TitleService();

    public ConsoleColor UserColor { get; set; } = Color.Red;

    public string GetBanner(TitleService.Banner banner)
    {
      return _titleService.Banners[banner];
    }

    public string GetTopLineLeadingSpaces(string topLine, TitleService.Banner banner)
    {
      int mid = MaxLength(banner) / 2;
      string spaces = new string(' ', mid - topLine.Length / 2);
      return spaces;
    }

    public int MaxLength(TitleService.Banner banner)
    {
      int max = 0;
      string title = GetBanner(banner);
      string[] bannerArray = TitleArray(banner);
      foreach (string s in bannerArray)
      {
        if (s.Length > max)
        {
          max = s.Length;
        }
      }
      return max;
    }

    public string[] TitleArray(TitleService.Banner banner)
    {
      return GetBanner(banner).Split('\n');
    }

    public void Animate(string topLine, TitleService.Banner banner)
    {
      Log log = new Log();
      string spaces = GetTopLineLeadingSpaces(topLine, banner);
      int start = spaces.Length - 1;
      string newTopLine = spaces + topLine;

      Console.Clear();
      log.NewLine();
      log.Add(spaces);
      log.Print(false);

      for (int i = start; i < newTopLine.Length; i++)
      {
        Console.CursorLeft = i;
        // Console.WriteLine(newTopLine.Substring(0, i) + '\r'); // This is cool
        log.Add(newTopLine.Substring(i, 1));
        log.Print(false);
        System.Threading.Thread.Sleep(50);
        Console.CursorLeft = i;
      }

      string[] bannerArray = TitleArray(banner);

      Console.CursorVisible = false;

      for (int i = 0; i < bannerArray.Length; i++)
      {
        string dropDown = newTopLine + '\n';
        start = bannerArray.Length - i - 1;

        for (int j = start; j <= bannerArray.Length - 1; j++)
        {
          dropDown += bannerArray[j] + '\n';
        }

        Console.Clear();
        log.NewLine();
        Console.WriteLine(dropDown);
        System.Threading.Thread.Sleep(150);
      }
    }

    private void DrawCheatLines()
    {
      Log log = new Log();

      log.Add("There are ways to \"cheat\", or make things... \"easy\"");
      log.Print();

      log.Add("Players like ");
      log.Add("Easy", Color.DarkMagenta);
      log.Add("-E and Derek ");
      log.Add("Cheat", Color.DarkMagenta);
      log.Add("er");
      log.Print();

      log.Print("figured this out with out even trying...");
      log.NewLine();
      log.Print("That being said...", Color.DarkGreen);
      log.Print("Press any key to continue", Color.DarkGray);

      Console.ReadKey();
    }

    private void DrawHeader(TitleService.Banner banner, string name = "")
    {
      string topLine = _titleService.TopLine();
      string spaces = GetTopLineLeadingSpaces(topLine, banner);
      Log log = new Log();

      Console.Clear();

      log.NewLine();

      if (name == "")
      {
        log.Add(spaces);
        log.Add(topLine);
        log.Print();
      }
      else
      {
        name = _titleService.Pluralize(name);
        string temp = _titleService.TopLinePrefix + name + _titleService.TopLineSuffix;
        spaces = GetTopLineLeadingSpaces(temp, banner);
        log.Add(spaces);
        log.Add(_titleService.TopLinePrefix);
        log.Add(name, UserColor);
        log.Add(_titleService.TopLineSuffix);
        log.Print();
      }

      Console.WriteLine(GetBanner(banner));
    }

    public void DrawHeader()
    {
      DrawHeader(Banner, Name);
    }

    private void DrawWelcome(TitleService.Banner banner)
    {
      Log log = new Log();
      Animate(_titleService.TopLine(), banner);

      // Animate("Jeremy, Welcome to the Jungle...", banner);

      DrawHeader(banner);
      log.NewLine();
      DrawCheatLines();

      DrawHeader(banner);
      log.NewLine();
      ConsoleCanvas canvas = new ConsoleCanvas();
      UserInfoBlock userBlock = new UserInfoBlock();
      canvas.AddBlock(userBlock.Block);
      canvas.GetInput();

      Name = userBlock.Name;
      UserColor = userBlock.Color;

      DrawHeader(banner, Name);

      log.NewLine();
    }

    public TitleController()
    {
      Banner = TitleService.Banner.Scarry;
      DrawWelcome(Banner);
    }
  }
}