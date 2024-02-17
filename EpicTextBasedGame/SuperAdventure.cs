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
                monster.DisplayStats();
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Heal");
                Console.WriteLine("3. Spell");
                Console.WriteLine("What do you want to do?");
                    string attack_input = Console.ReadLine().ToLower();
                    switch (attack_input){
                    
                    case "1":
                    case "attack":
                        monster.TakeDamage(player.CurrentWeapon.RollDamage());
                        Actiondone = true;
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
                    case "Spell":
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
                        // Hier moet een roll damage van monster komen
                        monster.TakeDamage(10);
                        Mactiondone = true;
                        break;
                    case "Heal":
                        int regenamount = monster.CurrentHealth - monster.MaxHealth / 5;
                        monster.RegenarateHealth(regenamount);
                        Mactiondone = true;
                        break;
                    case "Debuff":

                        Mactiondone = true;
                        break;
                    case "Buff":

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
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(CenterStr("██████╗ ███████╗██████╗ ███████╗███████╗██████╗ ██╗  ██╗███████╗██████╗"));
        Console.WriteLine(CenterStr("██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝██╔══██╗██║ ██╔╝██╔════╝██╔══██╗"));
        Console.WriteLine(CenterStr("██████╔╝█████╗  ██████╔╝███████╗█████╗  ██████╔╝█████╔╝ █████╗  ██████╔╝"));
        Console.WriteLine(CenterStr("██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══╝  ██╔══██╗██╔═██╗ ██╔══╝  ██╔══██╗"));
        Console.WriteLine(CenterStr("██████╔╝███████╗██║  ██║███████║███████╗██║  ██║██║  ██╗███████╗██║  ██║"));
        Console.WriteLine(CenterStr("╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝"));
        Console.WriteLine("\n\n\n\n\n\n\n\n\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Thread thread = null;
        Task.Run(() =>
        {
            thread = Thread.CurrentThread;
            WriteBlinkingTextOptions();
        });
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Environment.Exit(0);
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
        int count = 0;
        bool visible = true;
        while (count < 10)
        {
            Console.Write("\r" + (visible ? text : new String(' ', text.Length)));
            Thread.Sleep(500);
            visible = !visible;
            count++;
        }
    }
}