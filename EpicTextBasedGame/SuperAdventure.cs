//Heal and spell for Fight still needs to be implemented. It is now commented.

using System.Runtime.CompilerServices;

public class SuperAdventure
{
    public static void Fight(Player player, Monster monster)
    {
        if (monster.CurrentHealth == 0) return;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(monster.Image);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(Helper.CenterStr($"A {monster.Name} has appeared"));
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
        CheckQuestExpResetArea(monster, player);
    }

    public static void CheckQuestExpResetArea(Monster monster, Player player)
    {
        //exp is given
        if (monster.CurrentHealth == 0) player.Experience += monster.GiveExp;
        if (player.Experience >= 2 * (5 * player.Level))
        {
            player.Level += 1;
            player.MaxHealth += 10;
            player.CurrentHealth = player.MaxHealth;
            player.CurrentWeapon.RaiseMaxDamage(1);
            player.Experience = 0;
            Console.WriteLine(Helper.CenterStr("╔═════════════════════════╗"));
            Console.WriteLine(Helper.CenterStr("║*************************║"));
            Console.WriteLine(Helper.CenterStr("║        LEVEL UP         ║"));
            Console.WriteLine(Helper.CenterStr("║*************************║"));
            Console.WriteLine(Helper.CenterStr("╚═════════════════════════╝"));
            Console.WriteLine("\n");
        }

        if (player.QuestList.Any(x => x.Target == monster))
        {
            foreach (Quest quest in player.QuestList)
            {
                if ((quest.Target == monster) && (quest.Cleared is false))
                {
                    quest.UpdateQuest(player);
                    if (player.QuestList.Any(x => x.Cleared is false))
                    {
                        Console.WriteLine(Helper.CenterStr($"You've killed: {quest.CurrentKills}/{quest.TargetKills} {quest.Target.Name}"));
                        monster.CurrentHealth = monster.MaxHealth;
                    }  
                }
            }
        }
        else monster.CurrentHealth = monster.MaxHealth;
    }
}