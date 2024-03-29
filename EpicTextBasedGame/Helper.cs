﻿using System.Runtime.InteropServices;

public class Helper
{
    public static void TitleScreen()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr("██████╗ ███████╗██████╗ ███████╗███████╗██████╗ ██╗  ██╗███████╗██████╗"));
        Console.WriteLine(CenterStr("██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝██╔══██╗██║ ██╔╝██╔════╝██╔══██╗"));
        Console.WriteLine(CenterStr("██████╔╝█████╗  ██████╔╝███████╗█████╗  ██████╔╝█████╔╝ █████╗  ██████╔╝"));
        Console.WriteLine(CenterStr("██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══╝  ██╔══██╗██╔═██╗ ██╔══╝  ██╔══██╗"));
        Console.WriteLine(CenterStr("██████╔╝███████╗██║  ██║███████║███████╗██║  ██║██║  ██╗███████╗██║  ██║"));
        Console.WriteLine(CenterStr("╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝"));
        Console.WriteLine("\n");
        WriteInCenter(@"
                     /   ))     |\         )               ).           
               c--. (\  ( `.    / )  (\   ( `.     ).     ( (           
               | |   ))  ) )   ( (   `.`.  ) )    ( (      ) )          
               | |  ( ( / _..----.._  ) | ( ( _..----.._  ( (           
 ,-.           | |---) V.'-------.. `-. )-/.-' ..------ `--) \._        
 | /===========| |  (   |      ) ( ``-.`\/'.-''           (   ) ``-._   
 | | / / / / / | |--------------------->  <-------------------------_>=-
 | \===========| |                 ..-'./\.`-..                _,,-'    
 `-'           | |-------._------''_.-'----`-._``------_.-----'         
               | |         ``----''            ``----''                  
               | |                                                       
               c--`                                                      
");
        Console.WriteLine("\n\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Thread? thread = null;
        Task.Run(() =>
        {
            try
            {
                if (thread == null) thread = Thread.CurrentThread;
                WriteBlinkingTextOptions();
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
        });
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (thread != null) thread.Interrupt();
                QuitGame();
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                if (thread != null) thread.Interrupt();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.CursorVisible = false;
                break;
            }
        }
        Intro();
    }
    public static void Intro()
    {
        Thread? thread = null;
        Task.Run(() =>
        {
            try
            {
                thread = Thread.CurrentThread;
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\n\n\n\n\n\n\n\n");
                Console.WriteLine(Helper.CenterStr(@"“all hope abandon ye who enter here”"));
                Thread.Sleep(4000);
                Console.Clear();
                RollStr(CenterStr("You're falling..........\n"));
                RollStr(CenterStr("The void envelops you...\n"));
                RollStr(CenterStr("Then.....a voice........\n"));
                RollStr(CenterStr("Coming from deep within.\n"));
                RollStr(CenterStr("2 to cross the bridge...\n"));
                RollStr(CenterStr("1 to gain redemption....\n"));
                RollStr(CenterStr("The choice is yours.....\n"));
                RollStr(CenterStr("You wake up in your home\n"));
                RollStr(CenterStr("Clutching your chest....\n"));
                RollStr(CenterStr("Welcome to..............\n"));
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\n\n");
                Console.WriteLine(CenterStr(@"    )     (    (     "));
                Console.WriteLine(CenterStr(@" ( /(     )\ ) )\ )  "));
                Console.WriteLine(CenterStr(@" )\())(  (()/((()/(  "));
                Console.WriteLine(CenterStr(@"((_)\ )\  /(_))/(_)) "));
                Console.WriteLine(CenterStr(@" _((_|(_)(_)) (_))   "));
                Console.WriteLine(CenterStr(@"| || | __| |  | |    "));
                Console.WriteLine(CenterStr(@"| __ | _|| |__| |__  "));
                Console.WriteLine(CenterStr(@"|_||_|___|____|____| "));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\n\n");
                BlinkText("Press ENTER to continue", true);
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
        });
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                if (thread != null) thread.Interrupt();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                break;
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                if (thread != null) thread.Interrupt();
                Console.Clear();
                Task.Run(() =>
                {
                    try
                    {
                        thread = Thread.CurrentThread;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(CenterStr("You're falling.........."));
                        Console.WriteLine(CenterStr("The void envelops you..."));
                        Console.WriteLine(CenterStr("Then.....a voice........"));
                        Console.WriteLine(CenterStr("Coming from deep within."));
                        Console.WriteLine(CenterStr("2 to cross the bridge..."));
                        Console.WriteLine(CenterStr("1 to gain redemption...."));
                        Console.WriteLine(CenterStr("The choice is yours....."));
                        Console.WriteLine(CenterStr("You wake up in your home"));
                        Console.WriteLine(CenterStr("Clutching your chest...."));
                        Console.WriteLine(CenterStr("Welcome to.............."));
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\n\n\n");
                        Console.WriteLine(CenterStr(@"    )     (    (     "));
                        Console.WriteLine(CenterStr(@" ( /(     )\ ) )\ )  "));
                        Console.WriteLine(CenterStr(@" )\())(  (()/((()/(  "));
                        Console.WriteLine(CenterStr(@"((_)\ )\  /(_))/(_)) "));
                        Console.WriteLine(CenterStr(@" _((_|(_)(_)) (_))   "));
                        Console.WriteLine(CenterStr(@"| || | __| |  | |    "));
                        Console.WriteLine(CenterStr(@"| __ | _|| |__| |__  "));
                        Console.WriteLine(CenterStr(@"|_||_|___|____|____| "));
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n\n\n");
                        BlinkText("Press ENTER to continue", true);
                    }
                    catch (ThreadInterruptedException)
                    {
                        return;
                    }
                });
                while (true)
                {
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        if (thread != null) thread.Interrupt();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
                break;
            }
        }
    }
    public static void OutroScreen()
    {
        WinScreen();
        Thread? thread = null;
        Console.CursorVisible = false;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr(" ██████╗██████╗ ███████╗██████╗ ██╗████████╗███████╗"));
        Console.WriteLine(CenterStr("██╔════╝██╔══██╗██╔════╝██╔══██╗██║╚══██╔══╝██╔════╝"));
        Console.WriteLine(CenterStr("██║     ██████╔╝█████╗  ██║  ██║██║   ██║   ███████╗"));
        Console.WriteLine(CenterStr("██║     ██╔══██╗██╔══╝  ██║  ██║██║   ██║   ╚════██║"));
        Console.WriteLine(CenterStr("╚██████╗██║  ██║███████╗██████╔╝██║   ██║   ███████║"));
        Console.WriteLine(CenterStr(" ╚═════╝╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝   ╚═╝   ╚══════╝"));
        Console.WriteLine(CenterStr(@"     _                "));
        Console.WriteLine(CenterStr(@"    | |               "));
        Console.WriteLine(CenterStr(@"  __| | _____   _____ "));
        Console.WriteLine(CenterStr(@" / _` |/ _ \ \ / / __|"));
        Console.WriteLine(CenterStr(@"| (_| |  __/\ V /\__ \"));
        Console.WriteLine(CenterStr(@" \__,_|\___| \_/ |___/"));
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\n");
        Console.WriteLine(CenterStr(@"Mathijs Hoek"));
        Console.WriteLine(CenterStr(@"Yannick Lankhorst"));
        Console.WriteLine(CenterStr(@"Julian Kreugel"));
        Console.WriteLine(CenterStr(@"Utku Özyurt"));
        Console.WriteLine(CenterStr(@"Vincent van Oosten"));
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr(@" _____                 _       _   _____ _                 _        "));
        Console.WriteLine(CenterStr(@"/  ___|               (_)     | | |_   _| |               | |       "));
        Console.WriteLine(CenterStr(@"\ `--. _ __   ___  ___ _  __ _| |   | | | |__   __ _ _ __ | | _____ "));
        Console.WriteLine(CenterStr(@" `--. \ '_ \ / _ \/ __| |/ _` | |   | | | '_ \ / _` | '_ \| |/ / __|"));
        Console.WriteLine(CenterStr(@"/\__/ / |_) |  __/ (__| | (_| | |   | | | | | | (_| | | | |   <\__ \"));
        Console.WriteLine(CenterStr(@"\____/| .__/ \___|\___|_|\__,_|_|   \_/ |_| |_|\__,_|_| |_|_|\_\___/"));
        Console.WriteLine(CenterStr(@"      | |                                                           "));
        Console.WriteLine(CenterStr(@"      |_|                                                           "));
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(CenterStr("Cigdem Okuyucu"));
        Console.WriteLine(CenterStr("Karlijn van den Hoff"));
        Task.Run(() =>
        {
            try
            {
                if (thread == null) thread = Thread.CurrentThread;
                Thread.Sleep(3000);
                Console.ForegroundColor = ConsoleColor.Yellow;
                BlinkText("Press ENTER to return to titlescreen", true);
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
        });
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (thread != null) thread.Interrupt();
                Console.Clear();
                Program.Main();
            }
        }
    }

    public static void FightWinScreen(Player player, Monster monster)
    {
        Console.WriteLine("\u001b[2J\u001b[3J");
        Console.Clear();
        Thread.Sleep(500);
        Console.WriteLine("\n\n");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr(@"        )       *        )   (   (            (       ) "));
        Console.WriteLine(CenterStr(@"     ( /(     (  `    ( /(   )\ ))\ )   (     )\ ) ( /( "));
        Console.WriteLine(CenterStr(@" (   )\())(   )\))(   )\()) (()/(()/(   )\   (()/( )\())"));
        Console.WriteLine(CenterStr(@" )\ ((_)\ )\ ((_)()\ ((_)\   /(_))(_)|(((_)(  /(_)|(_)\ "));
        Console.WriteLine(CenterStr(@"((_) _((_|(_)(_()((_)_ ((_) (_))(_))  )\ _ )\(_))  _((_)"));
        Console.WriteLine(CenterStr(@"| __| \| | __|  \/  \ \ / / / __| |   (_)_\(_)_ _|| \| |"));
        Console.WriteLine(CenterStr(@"| _|| .` | _|| |\/| |\ V /  \__ \ |__  / _ \  | | | .` |"));
        Console.WriteLine(CenterStr(@"|___|_|\_|___|_|  |_| |_|   |___/____|/_/ \_\|___||_|\_|"));
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n\n");
        monster.DropLoot(player);
        if (player.LevelUp() is true) Thread.Sleep(1000);
        else Thread.Sleep(500);
    }

    public static void Deathscreen()
    {
        Console.CursorVisible = false;
        Console.WriteLine("\u001b[2J\u001b[3J");
        Console.Clear();
        Thread.Sleep(1000);
        Console.WriteLine("\n\n\n\n\n\n\n\n");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr(@"▄██   ▄    ▄██████▄  ███    █▄       ████████▄   ▄█     ▄████████ ████████▄ "));
        Console.WriteLine(CenterStr(@"███   ██▄ ███    ███ ███    ███      ███   ▀███ ███    ███    ███ ███   ▀███"));
        Console.WriteLine(CenterStr(@"███▄▄▄███ ███    ███ ███    ███      ███    ███ ███▌   ███    █▀  ███    ███"));
        Console.WriteLine(CenterStr(@"▀▀▀▀▀▀███ ███    ███ ███    ███      ███    ███ ███▌  ▄███▄▄▄     ███    ███"));
        Console.WriteLine(CenterStr(@"▄██   ███ ███    ███ ███    ███      ███    ███ ███▌ ▀▀███▀▀▀     ███    ███"));
        Console.WriteLine(CenterStr(@"███   ███ ███    ███ ███    ███      ███    ███ ███    ███    █▄  ███    ███"));
        Console.WriteLine(CenterStr(@"███   ███ ███    ███ ███    ███      ███   ▄███ ███    ███    ███ ███   ▄███"));
        Console.WriteLine(CenterStr(@"  ▀█████▀   ▀██████▀  ████████▀       ████████▀  █▀     ██████████ ████████▀"));
        Thread.Sleep(3000);
        Console.Clear();
        Program.Main();
    }

    public static void WinScreen()
    {
        Console.CursorVisible = false;
        Console.WriteLine("\u001b[2J\u001b[3J");
        Console.Clear();
        Thread.Sleep(1000);
        Console.WriteLine("\n\n\n\n\n\n\n\n");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr(@"▄██   ▄    ▄██████▄  ███    █▄        ▄█     █▄   ▄██████▄  ███▄▄▄▄  "));
        Console.WriteLine(CenterStr(@"███   ██▄ ███    ███ ███    ███      ███     ███ ███    ███ ███▀▀▀██▄"));
        Console.WriteLine(CenterStr(@"███▄▄▄███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@"▀▀▀▀▀▀███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@"▄██   ███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@"███   ███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@"███   ███ ███    ███ ███    ███      ███ ▄█▄ ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@" ▀█████▀   ▀██████▀  ████████▀        ▀███▀███▀   ▀██████▀   ▀█   █▀"));
        Thread.Sleep(3000);
        Console.Clear();
    }
    public static void RollStr(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            Console.Write(text[i]);
            Thread.Sleep(30);
        }

    }
    public static string CenterStr(string textToEnter)
    {
        return (String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
    }

    public static void WriteInCenter(string data)
    {
        foreach (var model in data.Split('\n'))
        {
            Console.SetCursorPosition((Console.WindowWidth - model.Length) / 2, Console.CursorTop);
            Console.WriteLine(model);
        }
    }

    public static void WriteBlinkingTextOptions()
    {
        bool visible = true;
        while (true)
        {
            Console.Write("\r" + (visible ? CenterStr("Press ENTER to start newgame (ESC to quit)") : new String(' ', CenterStr("Press ENTER to start newgame (ESC to quit)").Length)));
            Thread.Sleep(500);
            visible = !visible;
        }
    }

    public static void BlinkText(string text, bool center = false)
    {
        if (center is true) text = CenterStr(text);
        bool visible = true;
        while (true)
        {
            Console.Write("\r" + (visible ? text : new String(' ', text.Length)));
            Thread.Sleep(500);
            visible = !visible;
        }
    }

    public static void GameOptions()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Helper.CenterStr("[N][E][S][W] TO MOVE [M] FOR MAP"));
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(Helper.CenterStr("╔══════════════════════════════════════════════════╗"));
        Console.WriteLine(Helper.CenterStr("║ [1] MOVEMENT OPTIONS    [4] STATS                ║"));
        Console.WriteLine(Helper.CenterStr("║ [2] INVENTORY           [5] INTERACT             ║"));
        Console.WriteLine(Helper.CenterStr("║ [3] CHANGE EQUIPMENT    [6] QUIT TO TITLESCREEN  ║"));
        Console.WriteLine(Helper.CenterStr("╚══════════════════════════════════════════════════╝"));
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void ProjectMonser(Monster monster)
    {
        Console.Clear();
        Console.WriteLine("\u001b[2J\u001b[3J");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(monster.Image);
        Console.ForegroundColor = ConsoleColor.White;
    }

    [DllImport("User32.dll", CharSet = CharSet.Unicode)]
    static extern int MessageBox(IntPtr h, string m, string c, int type);

    public static void QuitGame()
    {
        int answer = MessageBox((IntPtr)0, "Are you sure you want to quit?", "Quit game", 49);
        if (answer == 1)
        {
            Console.Clear();
            Environment.Exit(0);
        }
        else Program.Main();
    }

    public static void ClearLastLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.CursorTop - 1);
    }

    public static void ClearLineDo()
    {
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        
    }
}