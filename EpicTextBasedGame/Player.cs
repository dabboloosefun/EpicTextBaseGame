using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

public class Player : Character
{
    public List<Weapon> Weapons;
    public List<Item> Items;
    public Weapon? CurrentWeapon;
    public Location CurrentLocation;
    public List<Quest> QuestList;
    public int Coins;
    public bool Minotaur = false;

    public Player()
    {
        Name = "Player";
        Weapons = new List<Weapon>();
        Items = new List<Item>();
        MaxHealth = 100; // Max health is 100, we can change this later but 100 seems like good number for now
        CurrentHealth = MaxHealth; // Start with full health
        CurrentLocation = World.Locations[0]; // Home
        CurrentWeapon = World.Weapons[4]; // starter weapon
        QuestList = new List<Quest>{};
        ActiveEffects = new List<Effect>();
        Experience = 0;
        Level = 1;
        Coins = 0;
    }

    public void AlchemistFight(Player player)
    {
        Console.Clear();
        string userInput;
        Console.WriteLine(Helper.CenterStr("The alchemist seems to hate your guts.\n"));
        do
        {
            Console.WriteLine(Helper.CenterStr("Show him how it's done? Y/N"));
            userInput = Console.ReadLine().ToLower();
            Helper.ClearLastLine();
        } while (userInput != "y" && userInput != "n");

        if (userInput == "n")
        {
            Console.Clear();
            return;
        }

        else if (userInput == "y")
        {
            SuperAdventure.Fight(player, new Monster(44, "Alchemist", 19, 25, 25, 400, 150, new List<LootDrop>(), @"
                                                  /|
                                                 / |
                                                /  |
                                               /   \
                                              /    |
                                             /      \
                                            /______  |
                                           /|@/@ \_\__\
                                          ///<__ / \  \\
                                         //  \\_\|/ \___\\_
                                        // __/\__/ /       \_
                                  _____||/      \/          \_
                               <|/\_\\   \  \     \            \
                                 \\_\-\   |  \    /     \ \__   |
                                     \/_____/|    \      \_|    |
                                              \   /        |    |
                                              |\  \        |____|
                                              \ \ /        /   |
                                              |\  \        \`///
                                              /   /            \
                                             /    .             \
                                            /     .              \
                                           / /                    \_
                                          /_/                      _\
                                          | \      |              /
                                          |  \_____|             / \
                                          |    |   \____________/   \
                                          |__  |               \     \
                                       ___/____|              __\    _|
                                       L____ __|              L____/`
"));
        }
    }

    public void MinotaurFight(Player player)
    {
        if (CurrentLocation.ID == World.LOCATION_ID_BURROW) {
            foreach (Quest quest in player.QuestList)
            {
                if ((quest.Target.ID == World.MONSTER_ID_MINOTAUR) && (quest.Cleared is false))
                {
                    Minotaur = true;
                }
            }
        }
    }

    public bool LevelUp()
    {
        if (Experience >= Level / Math.Pow(0.3, 2.0))
        {
            Level += 1;
            MaxHealth += 10;
            CurrentHealth = MaxHealth;
            CurrentWeapon.RaiseMaxDamage(1);
            Experience = 0;
            Console.WriteLine(Helper.CenterStr("╔═════════════════════════╗"));
            Console.WriteLine(Helper.CenterStr("║*************************║"));
            Console.WriteLine(Helper.CenterStr("║        LEVEL UP         ║"));
            Console.WriteLine(Helper.CenterStr("║*************************║"));
            Console.WriteLine(Helper.CenterStr("╚═════════════════════════╝"));
            Console.WriteLine("\n");
            return true;
        }
        return false;
    }

    public void DisplayStats()
    {
        Console.WriteLine("\t\t\t\t\t╔═══════════════════════════════════╗");
        Console.WriteLine("\t\t\t\t\t║              Player               ║");
        Console.WriteLine("\t\t\t\t\t╠═══════════════════════════════════╣");
        Console.WriteLine("\t\t\t\t\t║ HEALTH:      {0, -8}             ║", $"{CurrentHealth}/{MaxHealth}");
        Console.WriteLine("\t\t\t\t\t║ WEAPON:      {0, -21}║", $"{CurrentWeapon.Name}");
        Console.WriteLine("\t\t\t\t\t║ DAMAGE:      {0, -10}           ║", $"{(int)(CurrentWeapon.MaxDamage * 0.8)}-{CurrentWeapon.MaxDamage}");
        Console.WriteLine("\t\t\t\t\t║ CRITCHANCE:  {0, -10}           ║", $"{(double)CurrentWeapon.CritChance}");
        Console.WriteLine("\t\t\t\t\t║ EXP:         {0, -21}║", $"{Experience}");
        Console.WriteLine("\t\t\t\t\t║ LVL:         {0, -21}║", $"{Level}");
        Console.WriteLine("\t\t\t\t\t╚═══════════════════════════════════╝");
    }

    public void AddWeapon(Weapon weapon)
    {
        Weapons.Add(weapon);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        Weapons.Remove(weapon);
    }

    public void RemoveWeapon(int id)
    { 
        foreach(Weapon weapon in Weapons) {
            if (weapon.ID == id) Weapons.Remove(weapon);
        }
    }

    public void AddItem(Item item){ //note: change for stacking
        bool itemAlreadyInList = false;
        foreach(Item itemInList in Items){
            if(itemInList.ID == item.ID && itemInList.ItemEffect?.ID == item.ItemEffect?.ID) //using Name should also be fine unless items could have same name but different stats
            {   //considering items in List will now stack, using Items.Count+1 for ID should still work, multiples will have same ID
                itemInList.Increment(item.Count);
                itemAlreadyInList = true;
            }
            else if(itemInList.ID == item.ID && itemInList.ItemEffect?.ID != item.ItemEffect?.ID){
                //Items with different effects shouldn't have the same Item ID
                return;
            }
        }
        if (!itemAlreadyInList) Items.Add(item);
    }
    
    public void RemoveItem(Item item){  //call like RemoveItem(Player.Items.Find(searchItem => searchItem.ID ==  ID you want to remove ))
                                        //or better RemoveItem(Player.Items[index]) using userInput in new UseItem() method
        bool itemFoundInList = false;

        for (int i = Items.Count-1; i>=0; i--)
        {
            if(Items[i].ID == item.ID) //using Name should also be fine unless items could have same name but different stats
            {   //considering items in List will now stack, using Items.Count+1 for ID should still work, multiples will have same ID
                itemFoundInList = true;
                item.Decrement();
                if(item.Count <= 0){
                    Items.Remove(item);
                }
            }
        }
        if(!itemFoundInList){
            Console.WriteLine($"Attempted to remove {item.Name} from Player inventory but it couldn't be found.");
        }
        
    }
    public void ListItems()
    {
        Console.WriteLine(Helper.CenterStr("You are carrying these items in your inventory:"));
        for (int i = 0; i <= Items.Count - 1; i++)
        {
            Console.WriteLine(Helper.CenterStr($"{i+1}: {Items[i].Info()}")); //only add 1 to displayed item number
        }
    }

    public bool PromptUseItem(Player player, Monster monster, Monster? monsterTarget = null){
        if (Items.Count <= 0)
        {
            Console.WriteLine("You have no items");
            return false;
        }
        SuperAdventure.FightStats(player, monster);
        ListItems();
        int selectedItemNumber;
        int selectedTargetNumber;
        bool successfulParse;
        do
        {
            Console.WriteLine(Helper.CenterStr($"Enter the number of the item you wish to use. 1-{Items.Count}"));
            successfulParse = int.TryParse(Console.ReadLine(), out selectedItemNumber);
            Helper.ClearLastLine();
        } while (!successfulParse || !(1 <= selectedItemNumber && selectedItemNumber <= Items.Count));
        Console.Clear();
        do
        {
            Helper.ProjectMonser(monster);
            SuperAdventure.FightStats(player, monster);
            Console.WriteLine($"Who do you want to use the item on?");
            Console.WriteLine($"1. Yourself");
            Console.WriteLine($"2. Enemy");
            Console.WriteLine($"3. Cancel");
            successfulParse = int.TryParse(Console.ReadLine(), out selectedTargetNumber);
            Console.Clear();
        } while (!successfulParse || !(1 <= selectedTargetNumber && selectedTargetNumber <= 3));
        if(selectedTargetNumber == 1) //player
        {
            Helper.ProjectMonser(monster);
            Items[selectedItemNumber-1].UseItem(this); //displayed number is now 1 higher so we need displayednumber - 1
            RemoveItem(Items[selectedItemNumber-1]);
            return true;
        }
        else if(selectedTargetNumber == 2) //enemy
        {
            Helper.ProjectMonser(monster);
            Items[selectedItemNumber-1].UseItem(monsterTarget);
            Console.WriteLine("");
            RemoveItem(Items[selectedItemNumber-1]);
            return true;
        }
        else if(selectedTargetNumber == 3) //dont use item
        {
            Helper.ProjectMonser(monster);
            return false;
        }
        return false;
        //}

    }
    public void ListWeapons()
    {
        Console.WriteLine(Helper.CenterStr($"Equipped weapon: {CurrentWeapon.Name}: Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}\n"));
        Console.WriteLine(Helper.CenterStr("You are carrying these weapons in your inventory:"));
        int weaponNumber = 1;
        foreach (Weapon weapon in Weapons)
        {
            Console.WriteLine(Helper.CenterStr($"[{weaponNumber}] {weapon.Name}: Max damage: {weapon.MaxDamage}, Crit chance: {weapon.CritChance}\n"));
            weaponNumber++;
        }
    }

    public void PromptSelectWeapon()
    {
        string? userInput = "";

        do
        {
            Console.WriteLine(Helper.CenterStr("Would you like to swap your weapon? Y/N"));
            userInput = Console.ReadLine().ToLower();
            Helper.ClearLastLine();
        } while (userInput != "y" && userInput != "n");

        if (userInput == "n")
        {
            Console.Clear();
            return;
        } 

        else if (userInput == "y")
        {
            if (Weapons.Count <= 0)
            {
                Console.WriteLine(Helper.CenterStr("No other weapon to equip................."));
                return;
            }
            int selectedNumber;
            bool successfulParse;
            ListWeapons();
            do
            {
                Console.WriteLine(Helper.CenterStr($"Enter the number of the weapon you wish to equip."));
                successfulParse = int.TryParse(Console.ReadLine(), out selectedNumber);
                Helper.ClearLastLine();
            } while (!successfulParse ||  (selectedNumber <= 0 || selectedNumber > Weapons.Count));

            Weapons.Add(CurrentWeapon);
            CurrentWeapon = Weapons[selectedNumber-1];
            Weapons.Remove(Weapons[selectedNumber-1]);
            Console.Clear();
            Console.WriteLine(Helper.CenterStr($"You've equipped: {CurrentWeapon.Name}, Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}"));
        }
    }

    public bool TryMoveTo(string direction, Player player)
    {
        Location? newLocation = CurrentLocation.GetLocationAt(direction);
        if (newLocation != null)
        {
            CurrentLocation = newLocation;
            MinotaurFight(player);
            double succesfulEncounter = 1;
            Random encounterChance = new Random();
            double encounterRoll = encounterChance.NextDouble();
            if (player.CurrentLocation.MonsterLivingHere != null && encounterRoll <= succesfulEncounter) SuperAdventure.Fight(player, player.CurrentLocation.MonsterLivingHere);
            if (player.CurrentLocation.Name == "Guard post") player.CurrentLocation.StartEndGame(player);
            CurrentLocation.Map();
            return true;
        }
        return false;
    }

    public void AskPlayerAction(Player player)
    {
        Helper.GameOptions();

        string movement = "nswem";
        string playerAction;
        int intplayerAction;

        do
        {
            playerAction = Console.ReadLine();
            if (playerAction is string)
            {
                playerAction = playerAction.ToLower();
                if (movement.Contains(playerAction))
                {
                    if (playerAction == "m")
                    {
                        CurrentLocation.Map();
                        Helper.WriteInCenter(player.CurrentLocation.Description);
                        Helper.GameOptions();
                    } 
                    else if (TryMoveTo(playerAction, player))
                    {
                        Helper.WriteInCenter(player.CurrentLocation.Description);
                        Helper.GameOptions();
                    }
                }
            }
            Int32.TryParse(playerAction, out intplayerAction);
            Helper.ClearLineDo();
        } while ((intplayerAction < 1) || (intplayerAction > 6));


        CommenceAction(intplayerAction, player);
    }

    public void CommenceAction(int playerAction, Player player)
    {
        switch (playerAction)
        {
            case 1:
                Console.Clear();
                Console.WriteLine(CurrentLocation.Compass());
                break;

            case 2:
                Console.Clear();
                Console.WriteLine(Helper.CenterStr($"You are carrying {player.Coins} coins"));
                ListWeapons();
                ListItems();
                break;

            case 3:
                Console.Clear();
                PromptSelectWeapon();
                break;

            case 4:
                Console.Clear();
                DisplayStats();
                break;

            case 5:
                Console.Clear();
                //can interact to start quest or fight monster present in the location again.
                double succesfulEncounter = 1;
                Random encounterChance = new Random();
                double encounterRoll = encounterChance.NextDouble();
                if (player.CurrentLocation.ID == World.LOCATION_ID_TOWN_SQUARE) //town square
                {
                    Merchant.Market(player);
                    if (CurrentLocation.QuestAvailableHere != null) CurrentLocation.StartLocationQuest(player);
                }
                else if ((CurrentLocation.ID == World.LOCATION_ID_ALCHEMIST_HUT) && (player.QuestList.Any(x => x.ID == World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN && x.Cleared is true)))
                {
                    AlchemistFight(player);
                }
                else if (CurrentLocation.QuestAvailableHere != null) CurrentLocation.StartLocationQuest(player);
                else if (player.CurrentLocation.MonsterLivingHere != null && encounterRoll <= succesfulEncounter)
                {
                    SuperAdventure.Fight(player, player.CurrentLocation.MonsterLivingHere);
                    CurrentLocation.Map();
                } 
                
                else Console.WriteLine(Helper.CenterStr("Nothing to interact with here"));
                break;

            case 6:
                Program.Main();
                break;
        }
    }
}

