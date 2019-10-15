using System.Collections.Generic;

namespace ConsoleAdventure.Services
{
  public class TitleService
  {
    public enum Banner { Scarry, OldTimey, Action, End }

    public const string Scarry = @"
  ________                          __           ___________              __  .__      
  \______ \   ______  _  ______   _/  |_  ____   \_   _____/____ ________/  |_|  |__   
   |    |  \ /  _ \ \/ \/ /    \  \   __\/  _ \   |    __)_\__  \\_  __ \   __\  |  \  
   |    `   (  <_> )     /   |  \  |  | (  <_> )  |        \/ __ \|  | \/|  | |   Y  \ 
  /_______  /\____/ \/\_/|___|  /  |__|  \____/  /_______  (____  /__|   |__| |___|  / 
          \/                  \/                         \/     \/                 \/  ";

    public const string OldTimey = @"
  _____                                           _______                      _     
 (____ \                           _             (_______)               _    | |    
   _   \ \   ___   _ _ _  ____    | |_    ___     _____     ____   ____ | |_  | | _  
  | |   | | / _ \ | | | ||  _ \   |  _)  / _ \   |  ___)   / _  | / ___)|  _) | || \ 
  | |__/ / | |_| || | | || | | |  | |__ | |_| |  | |_____ ( ( | || |    | |__ | | | |
  |_____/   \___/  \____||_| |_|   \___) \___/   |_______) \_||_||_|     \___)|_| |_|";


    public const string Action = @"
     ___                    __         ____         __  __ 
    / _ \___ _    _____    / /____    / __/__ _____/ /_/ / 
   / // / _ \ |/|/ / _ \  / __/ _ \  / _// _ `/ __/ __/ _ \
  /____/\___/__,__/_//_/  \__/\___/ /___/\_,_/_/  \__/_//_/";

    public const string End = @"
   ____  ____ __    __ __  _     _____  ____     ____   ____  _____  _____  _   _ 
  | _) \/ () \\ \/\/ /|  \| |   |_   _|/ () \   | ===| / () \ | () )|_   _|| |_| |
  |____/\____/ \_/\_/ |_|\__|     |_|  \____/   |____|/__/\__\|_|\_\  |_|  |_| |_|";


    public Dictionary<Banner, string> Banners = new Dictionary<Banner, string>()
    {
      { Banner.Scarry, Scarry }, 
      { Banner.OldTimey, OldTimey },
      { Banner.Action, Action },
      { Banner.End, End }
    };

    public string Pluralize(string s)
    {
      return $"{s}'s";
    }

    public string TopLinePrefix {get; set;} = "Welcome to ";
    public string TopLineName {get; set;} = "Your";
    public string TopLineSuffix {get; set;} = " Adventure...";
    
    public string TopLine(){
      return TopLinePrefix + TopLineName + TopLineSuffix;
    }
  }
}