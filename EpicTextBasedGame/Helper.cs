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
        Console.WriteLine("\n\n\n\n\n\n\n\n\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Thread? thread = null;
        Task.Run(() =>
        {
            if (thread == null) thread = Thread.CurrentThread;
            WriteBlinkingTextOptions();
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
            thread = Thread.CurrentThread;
            Console.ForegroundColor = ConsoleColor.White;
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
        });
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                if (thread != null) thread.Interrupt();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                break;
            }
        }
    }
    public static void OutroScreen()
    {
        WinScreen();
        Thread? thread = null;
        while (true)
        {
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
                if (thread == null) thread = Thread.CurrentThread;
                Task.Delay(3000).Wait();
                Console.ForegroundColor = ConsoleColor.Yellow;
                BlinkText("Press ENTER to return to titlescreen", true);
                Console.ForegroundColor = ConsoleColor.DarkRed;
            });
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
    public static void Deathscreen()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Task.Delay(1000).Wait();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr(@"▄██   ▄    ▄██████▄  ███    █▄       ████████▄   ▄█     ▄████████ ████████▄  "));
        Console.WriteLine(CenterStr(@"███   ██▄ ███    ███ ███    ███      ███   ▀███ ███    ███    ███ ███   ▀███"));
        Console.WriteLine(CenterStr(@"███▄▄▄███ ███    ███ ███    ███      ███    ███ ███▌   ███    █▀  ███    ███"));
        Console.WriteLine(CenterStr(@"▀▀▀▀▀▀███ ███    ███ ███    ███      ███    ███ ███▌  ▄███▄▄▄     ███    ███"));
        Console.WriteLine(CenterStr(@"▄██   ███ ███    ███ ███    ███      ███    ███ ███▌ ▀▀███▀▀▀     ███    ███"));
        Console.WriteLine(CenterStr(@"███   ███ ███    ███ ███    ███      ███    ███ ███    ███    █▄  ███    ███"));
        Console.WriteLine(CenterStr(@"███   ███ ███    ███ ███    ███      ███   ▄███ ███    ███    ███ ███   ▄███"));
        Console.WriteLine(CenterStr(@"  ▀█████▀   ▀██████▀  ████████▀       ████████▀  █▀     ██████████ ████████▀"));
        Task.Delay(3000).Wait();
        Console.Clear();
        Program.Main();
    }
    public static void WinScreen()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Task.Delay(1000).Wait();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr(@"▄██   ▄    ▄██████▄  ███    █▄        ▄█     █▄   ▄██████▄  ███▄▄▄▄  "));
        Console.WriteLine(CenterStr(@"███   ██▄ ███    ███ ███    ███      ███     ███ ███    ███ ███▀▀▀██▄"));
        Console.WriteLine(CenterStr(@"███▄▄▄███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@"▀▀▀▀▀▀███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@"▄██   ███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@"███   ███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@"███   ███ ███    ███ ███    ███      ███ ▄█▄ ███ ███    ███ ███   ███"));
        Console.WriteLine(CenterStr(@" ▀█████▀   ▀██████▀  ████████▀        ▀███▀███▀   ▀██████▀   ▀█   █▀"));
        Task.Delay(3000).Wait();
        Console.Clear();
    }
    public static void RollStr(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            Console.Write(text[i]);
            Task.Delay(30).Wait();
        }

    }
    public static string CenterStr(string textToEnter)
    {
        return (String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
    }
    public static void WriteBlinkingTextOptions()
    {
        bool visible = true;
        while (true)
        {
            Console.Write("\r" + (visible ? CenterStr("Press ENTER to start newgame (ESC to quit)") : new String(' ', CenterStr("Press ENTER to start newgame (ESC to quit)").Length)));
            Task.Delay(500).Wait();
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
            Task.Delay(500).Wait();
            visible = !visible;
        }
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
}