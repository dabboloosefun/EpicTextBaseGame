//Heal and spell for Fight still needs to be implemented. It is now commented.
using System.Runtime.InteropServices;

public class SuperAdventure
{
    public static void Fight(Player player, Monster monster)
    {
        Console.WriteLine($"A {monster.Name} has appeared");
        bool playerturn = true;
        while (player.CurrentHealth > 0 && monster.CurrentHealth > 0)
        {
            if (playerturn){
            bool Actiondone = false;
            while(!Actiondone){
                player.UpdateEffects();
                monster.UpdateEffects();
                monster.DisplayStats();
                player.DisplayStats();
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use item");
                //Console.WriteLine("2. Heal");
                //Console.WriteLine("3. Spell");
                Console.WriteLine("What do you want to do?");
                    string attack_input = Console.ReadLine().ToLower();
                    switch (attack_input){
                    
                    case "1":
                    case "attack":
                        monster.TakeDamage(player.CurrentWeapon.RollDamage());
                        Actiondone = true;
                        break;

                    case "2":
                    case "use item":
                        player.PromptUseItem(ref Actiondone, monster);
                        break;
/*
                    case "2":
                    case "heal":
                    if (player.Items.Contains("Potion")){ 
                        player.RegenarateHealth(50)
                        Actiondone = true;
                        break;
                    }
                    else{
                        Console.WriteLine("You do not have a way to heal yourself. Good luck with your low health!")
                        break;
                    }
            }
*/
/*
                    case "3":
                    case "spell":
                        Console.WriteLine("What spell do you wish to cast?");
                        Console.WriteLine("1. Buff (Increases your attacking power).");
                        Console.WriteLine("2. Debuff the attack of the enemy");
                        string spell_input = Console.ReadLine();
                        if (spell_input == "1" || spell_input == "buff"){

                        }
                        else if (spell_input == "2" || spell_input == "debuff"){

                        }
*/
                    }
                }
            } // While loop voor player turn
            else if (!playerturn)
            {   
                bool Mactiondone = false;
                while (!Mactiondone){
                int Monster_choice;
                string Monster_action = "";
                
                if (monster.CurrentHealth < monster.MaxHealth / 2)
                {
                    Random rand = new Random();
                    Monster_choice = rand.Next(1, 81);
                }
                else
                {
                    Random rand = new Random();
                    Monster_choice = rand.Next(1, 101);
                }

                if (Monster_choice <= 60)
                {
                    Monster_action = "Attack";
                }
                else if (Monster_choice <= 80 && Monster_choice > 60)
                {
                    Monster_action = "Heal";
                }
                else if (Monster_choice > 80)
                {
                    if (Monster_choice > 90)
                    {
                        Monster_action = "Debuff";
                    }
                    else
                    {
                        Monster_action = "Buff";
                    }
                }

                switch (Monster_action)
                {
                    case "Attack":
                        player.TakeDamage(monster.RollDamageMonster());
                        Mactiondone = true;
                        break;
                    case "Heal":
                        int regenamount = monster.CurrentHealth - monster.MaxHealth / 5;
                        monster.RegenarateHealth(regenamount);
                        Mactiondone = true;
                        break;
                    case "Debuff":
                        // implement needed
                        Mactiondone = true;
                        break;
                    case "Buff":
                        // implement needed
                        Mactiondone = true;
                        break;
                }
                }
            } // While loop voor enemy
            playerturn = !playerturn; // Draait player turn om
        } // While loop voor fight
    }






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
            try{
                thread = Thread.CurrentThread;
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
                Console.ForegroundColor = ConsoleColor.White;
                thread.Interrupt();
                QuitGame(); 
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                thread.Interrupt();
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
        Thread thread = null;
        Task.Run(() =>
        {
            try{
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
            }
            catch(ThreadInterruptedException)
            {
                return;
            }
        });
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                thread.Interrupt();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                break;
            }
        }
    }
    public static void OutroScreen()
    {
        WinScreen();
        Thread thread = null;
        int count = 0;
        while (true)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            thread = Thread.CurrentThread;
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
                try{
                    Thread.Sleep(3000);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    BlinkText("Press ESC to return to titlescreen", true);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                catch(ThreadInterruptedException)
                {
                    return;
                }
            });
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                Console.ForegroundColor = ConsoleColor.White;
                thread.Interrupt();
                Console.Clear();
                Program.Main();
            }
        }
    }
    public static void Deathscreen()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterStr(@"▄██   ▄    ▄██████▄  ███    █▄       ████████▄   ▄█     ▄████████ ████████▄  "));
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
        Console.Clear();
        Thread.Sleep(1000);
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

    public static void BlinkText(string text, bool center=false)
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
    //[DllImport("User32.dll", CharSet = CharSet.Unicode)]
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