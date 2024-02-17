using System.Collections;
using System.ComponentModel.DataAnnotations;

public class Player
{
    public List<Weapon> Weapons;
    public List<string> Items;
    public Weapon? CurrentWeapon;
    public int CurrentHealth;
    public int MaxHealth;
    public Location CurrentLocation;

    public Player()
    {
        Weapons = new List<Weapon>();
        Items = new List<string>();
        MaxHealth = 100; // Max health is 100, we can change this later but 100 seems like good number for now
        CurrentHealth = MaxHealth; // Start with full health
        CurrentLocation = World.Locations[0]; // Home
        CurrentWeapon = World.Weapons[0]; // starter weapon
    }

    public void DisplayStats()
    {
        Console.WriteLine("╔══════════════════════════════════╗");
        Console.WriteLine("║           Current Stats          ║");
        Console.WriteLine("╠══════════════════════════════════╣");
        Console.WriteLine($"║ Health:      {CurrentHealth}/{MaxHealth,-16}║");
        Console.WriteLine($"║ Weapon:      {CurrentWeapon.Name,-20}║");
        Console.WriteLine($"║ Weapon Damage: {(int)CurrentWeapon.MaxDamage * 0.8}-{CurrentWeapon.MaxDamage, -16}║");
        Console.WriteLine("╚══════════════════════════════════╝");
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0; // HP wont go into negative when printing
            Console.WriteLine("game over. L"); 
        }
        else
        {
            Console.WriteLine($"You took {damage} damage. Current health: {CurrentHealth}");
        }
    }

    public void ListItems()
    {
        Console.WriteLine("You are carrying these items in your inventory:");
        for (int i = 0; i < Items.Count; i++)
        {
            Console.WriteLine($"{i}. {Items[i]}:\n");
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
            string userSelectInput;
            int selectedNumber;
            bool successfulParse;
            do
            {

                Console.WriteLine($"Enter the number of the weapon you wish to equip. 0-{Weapons.Count - 1}");
                userSelectInput = Console.ReadLine();
                successfulParse = int.TryParse(userSelectInput, out selectedNumber);
            } while (!successfulParse || !(0 <= selectedNumber && selectedNumber < Weapons.Count));

            Weapons.Add(CurrentWeapon);
            CurrentWeapon = Weapons[selectedNumber];
            Weapons.Remove(Weapons[selectedNumber]);
            Console.WriteLine($"You've now equipped this new weapon! {CurrentWeapon.Name}: Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}");
        }
    }

    public bool TryMoveTo()
    {
        Console.WriteLine(CurrentLocation.Compass());
        Console.WriteLine("In what direction would you like to go? n/e/s/w");
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
        Console.WriteLine("What would you like to do? 1/2/3/4");
        Console.WriteLine("╔═════════════════════════╗");
        Console.WriteLine("║ 1. Move                 ║");
        Console.WriteLine("║ 2. Check inventory      ║");
        Console.WriteLine("║ 3. Change equipment     ║");
        Console.WriteLine("║ 4. Check stats          ║");
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
                    Console.WriteLine(CurrentLocation.Name);
                    break;
                } 
                Console.WriteLine("That direction is invalid");
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
        }
    }
}

