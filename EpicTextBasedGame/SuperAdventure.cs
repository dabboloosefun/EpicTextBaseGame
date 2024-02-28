//Heal and spell for Fight still needs to be implemented. It is now commented.

using System.Runtime.CompilerServices;

public class SuperAdventure
{
    public static void Fight(Player player, Monster monster)
    {
        if (player.CurrentLocation.MonsterLivingHere is not null)
        {
            if ((player.CurrentLocation.MonsterLivingHere.ID == World.MONSTER_ID_MINOTAUR) && (player.Minotaur == false)) return;
            else if (monster.CurrentHealth == 0) return;
        }
        Helper.ProjectMonser(monster);
        Console.WriteLine(Helper.CenterStr($"A(n) {monster.Name} has appeared"));
        bool playerturn = true;
        while (player.CurrentHealth > 0 && monster.CurrentHealth > 0)
        {
            if (playerturn){
            bool Actiondone = false;
            while(!Actiondone){
                FightStats(player, monster);
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use item");
                Console.WriteLine("What do you want to do?");
                    string attack_input = Console.ReadLine().ToLower();
                    switch (attack_input){
                    
                    case "1":
                    case "attack":
                        Helper.ProjectMonser(monster);
                        monster.TakeDamage(player.CurrentWeapon.RollDamage());
                        player.UpdateEffects();
                        Actiondone = true;
                        break;

                    case "2":
                    case "use item":
                        Console.Clear();
                        Console.WriteLine("\u001b[2J\u001b[3J");
                        bool succesfulitem = player.PromptUseItem(player, monster);
                        if(succesfulitem){
                            player.UpdateEffects();
                            Actiondone = true;
                        }
                        break;

                    default:
                        Helper.ProjectMonser(monster);
                        break;
                    }
                }
            } // While loop voor player turn
            else if (!playerturn)
            {   
                bool Mactiondone = false;
                while (!Mactiondone){
                int Monster_choice;
                string Monster_action = "";

        
                Random rand = new Random();
                Monster_choice = rand.Next(1, 101);

                if (monster.CurrentHealth < monster.MaxHealth / 2) // If monster is under half health, it will not buff and more likely attack the player
                {
                    if (Monster_choice <= 70)
                    {
                        Monster_action = "Attack";
                    }
                    else if (Monster_choice > 70)
                    {
                        Monster_action = "Heal";
                    }
                }

                else
                {
                    if (Monster_choice <= 60)
                    {
                        Monster_action = "Attack";
                    }
                    else if (Monster_choice <= 75 && Monster_choice > 60)
                    {
                        Monster_action = "Heal";
                    }
                    else if (Monster_choice > 75)
                    {
                        Monster_action = "Buff";
                    }
                }

                switch (Monster_action)
                {
                    case "Attack":
                        player.TakeDamage(monster.RollDamageMonster());
                        monster.UpdateEffects();
                        Mactiondone = true;
                        break;
                    case "Heal":
                        int regenamount = ((monster.MaxHealth - monster.CurrentHealth) / 5) + 2; // +2 to ensure it will always heal the enemy
                        monster.RegenarateHealth(regenamount);
                        monster.UpdateEffects();
                        Mactiondone = true;
                        break;
                    case "Buff":
                        monster.RaiseMaxDamage((monster.MaxHealth/10) * 2 + 5);
                        monster.UpdateEffects();
                        Mactiondone = true;
                        break;
                }
                }
            } // While loop voor enemy
            playerturn = !playerturn; // Draait player turn om
        } // While loop voor fight
        ResolveArea(monster, player);
    }

    public static void ResolveArea(Monster monster, Player player)
    {
        //monster bugg reset
        monster.MaxDamage -= monster.BuffedDmg;
        monster.BuffedDmg = 0;
        //exp is given
        if (monster.CurrentHealth <= 0) 
        {
            player.Experience += monster.GiveExp;
            player.Coins += monster.GiveCoins;
            monster.DropLoot(player);
            Helper.FightWinScreen(player);
        }

        if (player.QuestList.Any(x => x.Target == monster))
        {
            foreach (Quest quest in player.QuestList)
            {
                if ((quest.Target == monster) && (quest.Cleared is false))
                {
                    quest.UpdateQuest(player);
                    if (quest.Cleared is false)
                    {
                        Console.WriteLine(Helper.CenterStr($"You've killed: {quest.CurrentKills}/{quest.TargetKills} {quest.Target.Name}(s)"));
                        monster.CurrentHealth = monster.MaxHealth;
                        Thread.Sleep(1000);
                    }  
                }
            }
        }
        else monster.CurrentHealth = monster.MaxHealth;
    }

    public static void FightStats(Player player, Monster monster)
    {
        Console.WriteLine("╔═══════════════════════════════════╗\t\t\t\t\t\t  ╔═══════════════════════════════════╗");
        Console.WriteLine("║              Player               ║\t\t\t\t\t\t  ║               {0, -20}║", $"{monster.Name}");
        Console.WriteLine("╠═══════════════════════════════════╣\t\t\t\t\t\t  ╠═══════════════════════════════════╣");
        Console.WriteLine("║ HEALTH:      {0, -8}             ║\t\t\t\t\t\t  ║ HEALTH:        {1, -8}           ║", $"{player.CurrentHealth}/{player.MaxHealth}", $"{monster.CurrentHealth}/{monster.MaxHealth}");
        Console.WriteLine("║ WEAPON:      {0, -21}║\t\t\t\t\t\t  ║ DAMAGE:        {1, -10}         ║", $"{player.CurrentWeapon.Name}", $"{monster.MaxDamage}");
        Console.WriteLine("║ DAMAGE:      {0, -10}           ║\t\t\t\t\t\t  ╚═══════════════════════════════════╝", $"{(int)(player.CurrentWeapon.MaxDamage * 0.8)}-{player.CurrentWeapon.MaxDamage}");
        Console.WriteLine("║ CRITCHANCE:  {0, -10}           ║", $"{(double)(player.CurrentWeapon.CritChance)}");
        Console.WriteLine("║ EXP:         {0, -21}║", $"{player.Experience}");
        Console.WriteLine("║ LVL:         {0, -21}║", $"{player.Level}");
        Console.WriteLine("╚═══════════════════════════════════╝");
    }
}