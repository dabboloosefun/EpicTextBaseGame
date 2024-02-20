using System.Collections;
using System.ComponentModel.DataAnnotations;

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
    }

    public void DisplayStats()
    {
        Console.WriteLine("╔═══════════════════════════════════╗");
        Console.WriteLine("║               Stats               ║");
        Console.WriteLine("╠═══════════════════════════════════╣");
        Console.WriteLine("║ Health:      {0, -8}             ║", $"{CurrentHealth}/{MaxHealth}");
        Console.WriteLine("║ Weapon:      {0, -21}║", $"{CurrentWeapon.Name}");
        Console.WriteLine("║ Damage:      {0, -10}           ║", $"{(int)CurrentWeapon.MaxDamage * 0.8}-{CurrentWeapon.MaxDamage}");
        Console.WriteLine("║ Crit Chance:      {0}               ║", $"{(double)CurrentWeapon.CritChance}");
        Console.WriteLine("╚═══════════════════════════════════╝");
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
            if(itemInList.ID == item.ID) //using Name should also be fine unless items could have same name but different stats
            {   //considering items in List will now stack, using Items.Count+1 for ID should still work, multiples will have same ID
                itemInList.Increment();
                itemAlreadyInList = true;
            }
        }
        if (!itemAlreadyInList) Items.Add(item);
    }
    
    public void RemoveItem(Item item){  //call like RemoveItem(Player.Items.Find(searchItem => searchItem.ID ==  ID you want to remove ))
                                        //or better RemoveItem(Player.Items[index]) using userInput in new UseItem() method
        bool itemFoundInList = false;
        foreach(Item itemInList in Items){
            if(itemInList.ID == item.ID) //using Name should also be fine unless items could have same name but different stats
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
        Console.WriteLine("You are carrying these items in your inventory:");
        for (int i = 0; i < Items.Count; i++)
        {
            Console.WriteLine($"{i}: {Items[i].Info()}\n");
        }
    }

    public void PromptUseItem(ref bool actionDoneRef, Monster? monsterTarget = null){
        string? userInput = "";
        do
        { //move elsewhere for potential out of combat uses. it's a useless prompt in combat
            Console.WriteLine("Would you like to use an item? yes/no"); 
            userInput = Console.ReadLine();
        } while (userInput != "yes" && userInput != "no");
        if (userInput == "no") return;
        else if (userInput == "yes")
        {
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
                Console.WriteLine($"Enter the number of the item you wish to use. 0-{Items.Count - 1}");
                successfulParse = int.TryParse(Console.ReadLine(), out selectedItemNumber);
            } while (!successfulParse || !(0 <= selectedItemNumber && selectedItemNumber < Items.Count));
            do
            {
                Console.WriteLine($"Who do you want to use the item on?");
                Console.WriteLine($"1. Yourself");
                Console.WriteLine($"2. Enemy");
                Console.WriteLine($"3. Cancel");
                successfulParse = int.TryParse(Console.ReadLine(), out selectedTargetNumber);
            } while (!successfulParse || !(1 <= selectedTargetNumber && selectedTargetNumber <= 2));
            if(selectedTargetNumber == 1)
            {
                Items[selectedItemNumber].UseItem(this);
                Items.RemoveAt(selectedItemNumber);
                actionDoneRef = true;
                return;
            }
            else if(selectedTargetNumber == 2)
            {
                Items[selectedItemNumber].UseItem(monsterTarget);
                Console.WriteLine("");
                Items.RemoveAt(selectedItemNumber);
                actionDoneRef = true;
                return;
            }
            else if(selectedTargetNumber == 3)
            {
                Console.WriteLine("You put the item back in your backpack");
                return;
            }
            
        }

    }
    public void ListWeapons()
    {
        Console.WriteLine($"Equipped weapon: {CurrentWeapon.Name}: Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}\n");
        Console.WriteLine("You are carrying these weapons in your inventory:");
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapon currentWeapon = Weapons[i];
            Console.WriteLine($"{i}. {currentWeapon.Name}: Max damage: {currentWeapon.MaxDamage}, Crit chance: {currentWeapon.CritChance}\n");
        }
    }

    public void PromptSelectWeapon()
    {
        string? userInput = "";

        do
        {
            Console.WriteLine("Would you like to swap your weapon? yes/no");
            userInput = Console.ReadLine();
        } while (userInput != "yes" && userInput != "no");

        if (userInput == "no") return;
        else if (userInput == "yes")
        {
            if (Weapons.Count <= 0)
            {
                Console.WriteLine("No other weapon to equip");
                return;
            }
            int selectedNumber;
            bool successfulParse;
            do
            {

                Console.WriteLine($"Enter the number of the weapon you wish to equip. 0-{Weapons.Count - 1}");
                successfulParse = int.TryParse(Console.ReadLine(), out selectedNumber);
            } while (!successfulParse || !(0 <= selectedNumber && selectedNumber < Weapons.Count));

            Weapons.Add(CurrentWeapon);
            CurrentWeapon = Weapons[selectedNumber];
            Weapons.Remove(Weapons[selectedNumber]);
            Console.WriteLine($"You've now equipped this new weapon! {CurrentWeapon.Name}: Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}");
        }
    }

    public bool TryMoveTo()
    {
        Console.WriteLine($@"
*********┌─────────────┐***  ≈≈≈≈≈      LEGEND
***      │ .....P..... │*  ≈≈≈≈≈        H: Home
         │     ...     │  ≈≈≈≈≈         T: Town Square
         └──┐   ▄   ┌──┘ ≈≈≈≈≈          F: Farmhouse
 <=>        │ ▄███▄ │    ≈≈≈≈≈          V: Farmer's Field
    ██      │  ▀A▀  │    ≈≈≈≈≈          A: Alchemist's Hut
     ▐      └───|───┘    ≈≈≈≈≈          P: Alchemist's Garden
               /        ≈≈≈≈≈           G: Guard Post
_____     ▐▀  /         ≈≈≈≈    *       B: Bridge
=====▐▀   ▀█ | █▀█     ≈≈≈≈   ***       S: Forest
=====F___   ┌───┐      ≈≈≈   ****       
=====    \__│   │▄  ▄▄≈≈≈≈  *****
__V__     ▀▀│ T │---G╠═B═╣--*****
=====       │   │█  ▀▀≈≈≈  ******
=====     ▀█└───┘     ≈≈≈  **S***
=====       |▐█▀     ≈≈≈  *******       Current Location: {CurrentLocation.Name}
       *     \     ≈≈≈≈ *********
   ▄▌ ***    |     ≈≈≈≈ *********
**********   ▀   ≈≈≈≈≈ **********
***********  H   ≈≈≈≈≈ **********
");
        Console.WriteLine(CurrentLocation.Compass());
        Console.WriteLine(SuperAdventure.CenterStr("In what direction would you like to go?"));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(SuperAdventure.CenterStr("[N][E][S][W]"));
        Console.ForegroundColor = ConsoleColor.White;
        string direction;
        do
        {
            direction = Console.ReadLine().ToLower();
        }while (direction != "n" && direction != "s" && direction != "w" && direction != "e");

        Location? newLocation = CurrentLocation.GetLocationAt(direction);
        if (newLocation != null)
        {
            CurrentLocation = newLocation;
            return true;
        }
        return false;
    }

    public int AskPlayerAction()
    {
        Console.WriteLine(SuperAdventure.CenterStr("What would you like to do?"));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(SuperAdventure.CenterStr("[1][2][3][4][5]"));
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔═════════════════════════╗");
        Console.WriteLine("║ [1] Move                ║");
        Console.WriteLine("║ [2] Check inventory     ║");
        Console.WriteLine("║ [3] Change equipment    ║");
        Console.WriteLine("║ [4] Check stats         ║");
        Console.WriteLine("║ [5] Quit to titlescreen ║");
        Console.WriteLine("╚═════════════════════════╝");

        int playerAction;
        bool succesfulParse;
        do
        {
            succesfulParse = int.TryParse(Console.ReadLine(), out playerAction);
        } while((playerAction < 1 && playerAction > 4) || !succesfulParse);

        return playerAction;
    }

    public void CommenceAction(int playerAction)
    {
        switch (playerAction)
        {
            case 1:
                if(TryMoveTo())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(SuperAdventure.CenterStr($"Now entering: {CurrentLocation.Name}"));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(SuperAdventure.CenterStr(CurrentLocation.Description));
                    break;
                } 
                Console.WriteLine(SuperAdventure.CenterStr("That direction is invalid"));
                break;

            case 2:
                ListWeapons();
                ListItems();
                break;

            case 3:
                PromptSelectWeapon();
                break;

            case 4:
                DisplayStats();
                break;
            case 5:
                Program.Main();
                break;
            default:
                Console.WriteLine("Input error");
                break;
        }
    }
}

