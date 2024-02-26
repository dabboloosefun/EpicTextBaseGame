using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

public class Player : Character
{
    public List<Weapon> Weapons;
    public List<Item> Items;
    public Weapon? CurrentWeapon;
    public Location CurrentLocation;
    public List<Quest> QuestList;
    //public List<(Item item, int turns, bool applied)> ActiveEffects;
    

    public Player()
    {
        Name = "Player";
        Weapons = new List<Weapon>();
        Items = new List<Item>();
        MaxHealth = 100; // Max health is 100, we can change this later but 100 seems like good number for now
        CurrentHealth = MaxHealth; // Start with full health
        CurrentLocation = World.Locations[0]; // Home
        CurrentWeapon = World.Weapons[0]; // starter weapon
        QuestList = new List<Quest>{};
        ActiveEffects = new List<Effect>();
        Experience = 0;
        Level = 1;
    }

    public void LevelUp()
    {
        if (Experience >= 2 * (5 * Level))
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
        }
    }

    public void DisplayStats()
    {
        Console.WriteLine("\t\t\t\t\t╔═══════════════════════════════════╗");
        Console.WriteLine("\t\t\t\t\t║              Player               ║");
        Console.WriteLine("\t\t\t\t\t╠═══════════════════════════════════╣");
        Console.WriteLine("\t\t\t\t\t║ HEALTH:      {0, -8}             ║", $"{CurrentHealth}/{MaxHealth}");
        Console.WriteLine("\t\t\t\t\t║ WEAPON:      {0, -21}║", $"{CurrentWeapon.Name}");
        Console.WriteLine("\t\t\t\t\t║ DAMAGE:      {0, -10}           ║", $"{(int)(CurrentWeapon.MaxDamage * 0.8)}-{CurrentWeapon.MaxDamage}");
        Console.WriteLine("\t\t\t\t\t║ CRITCHANCE:  {0}                 ║", $"{(double)CurrentWeapon.CritChance}");
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

    public void PromptUseItem(ref bool actionDoneRef, Monster? monsterTarget = null){
        // string? userInput = "";
        // do
        // { //move elsewhere for potential out of combat uses. it's a useless prompt in combat
        //     Console.WriteLine("Would you like to use an item? yes/no"); 
        //     userInput = Console.ReadLine();
        // } while (userInput != "yes" && userInput != "no");
        // if (userInput == "no") return;
        // else if (userInput == "yes")
        // {
        if (Items.Count <= 0)
        {
            Console.WriteLine("You have no items");
            return;
        }
        ListItems();
        int selectedItemNumber;
        int selectedTargetNumber;
        bool successfulParse;
        do
        {
            Console.WriteLine($"Enter the number of the item you wish to use. 1-{Items.Count}");
            successfulParse = int.TryParse(Console.ReadLine(), out selectedItemNumber);
        } while (!successfulParse || !(1 <= selectedItemNumber && selectedItemNumber <= Items.Count));
        do
        {
            Console.WriteLine($"Who do you want to use the item on?");
            Console.WriteLine($"1. Yourself");
            Console.WriteLine($"2. Enemy");
            Console.WriteLine($"3. Cancel");
            successfulParse = int.TryParse(Console.ReadLine(), out selectedTargetNumber);
        } while (!successfulParse || !(1 <= selectedTargetNumber && selectedTargetNumber <= 3));
        if(selectedTargetNumber == 1)
        {
            Items[selectedItemNumber-1].UseItem(this); //displayed number is now 1 higher so we need displayednumber - 1
            RemoveItem(Items[selectedItemNumber-1]);
            //Items.RemoveAt(selectedItemNumber);
            //actionDoneRef = true;
            return;
        }
        else if(selectedTargetNumber == 2)
        {
            Items[selectedItemNumber-1].UseItem(monsterTarget);
            Console.WriteLine("");
            RemoveItem(Items[selectedItemNumber-1]);
            //Items.RemoveAt(selectedItemNumber);
            //actionDoneRef = true;
            return;
        }
        else if(selectedTargetNumber == 3)
        {
            Console.WriteLine("You put the item back in your backpack");
            return;
        }
            
        //}

    }
    public void ListWeapons()
    {
        Console.WriteLine(Helper.CenterStr($"Equipped weapon: {CurrentWeapon.Name}: Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}\n"));
        Console.WriteLine(Helper.CenterStr("You are carrying these weapons in your inventory:"));
        for (int i = 0; i < Weapons.Count - 1; i++)
        {
            Weapon currentWeapon = Weapons[i];
            Console.WriteLine(Helper.CenterStr($"{i+1}. {currentWeapon.Name}: Max damage: {currentWeapon.MaxDamage}, Crit chance: {currentWeapon.CritChance}\n"));
        }
    }

    public void PromptSelectWeapon()
    {
        string? userInput = "";

        do
        {
            Console.WriteLine(Helper.CenterStr("Would you like to swap your weapon? Y/N"));
            userInput = (Console.ReadLine()).ToLower();
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
            do
            {

                Console.WriteLine($"Enter the number of the weapon you wish to equip. 1-{Weapons.Count}");
                successfulParse = int.TryParse(Console.ReadLine(), out selectedNumber);
            } while (!successfulParse || !(0 <= selectedNumber && selectedNumber < Weapons.Count));

            Weapons.Add(CurrentWeapon);
            CurrentWeapon = Weapons[selectedNumber-1];
            Weapons.Remove(Weapons[selectedNumber-1]);
            Console.WriteLine($"You've now equipped this new weapon! {CurrentWeapon.Name}: Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}");
        }
    }

    public bool TryMoveTo(string direction, Player player)
    {
        Location? newLocation = CurrentLocation.GetLocationAt(direction);
        if (newLocation != null)
        {
            CurrentLocation = newLocation;
            double succesfulEncounter = 1;
            Random encounterChance = new Random();
            double encounterRoll = encounterChance.NextDouble();
            if (player.CurrentLocation.MonsterLivingHere != null && encounterRoll <= succesfulEncounter) SuperAdventure.Fight(player, player.CurrentLocation.MonsterLivingHere);
            if (player.CurrentLocation.Name == "Guard post") player.CurrentLocation.StartEndGame(player);
            return true;
        }
        return false;
    }

    public void AskPlayerAction(Player player)
    {
        Helper.GameOptions();

        string movement = "nswe";
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
                    if (TryMoveTo(playerAction, player))
                    {
                        CurrentLocation.Map();
                        Console.WriteLine(Helper.CenterStr(CurrentLocation.Description));
                        Helper.GameOptions();
                    }
                }
            }
            Int32.TryParse(playerAction, out intplayerAction);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
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
                if (CurrentLocation.QuestAvailableHere != null) CurrentLocation.StartLocationQuest(player);
                else if (player.CurrentLocation.MonsterLivingHere != null && encounterRoll <= succesfulEncounter) SuperAdventure.Fight(player, player.CurrentLocation.MonsterLivingHere);
                else Console.WriteLine(Helper.CenterStr("Nothing to interact with here"));
                break;

            case 6:
                Program.Main();
                break;
        }
    }
}

