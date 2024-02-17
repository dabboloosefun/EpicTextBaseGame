public class Player
{
    public List<Weapon> Weapons;
    public List<string> Items;
    public Weapon? CurrentWeapon;
    public int CurrentHealth;
    public int MaxHealth;

    public Player()
    {
        Weapons = new List<Weapon>();
        Items = new List<string>();
        MaxHealth = 100; // Max health is 100, we can change this later but 100 seems like good number for now
        CurrentHealth = MaxHealth; // Start with full health
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
        string userInput = "";

        do
        {
            Console.WriteLine("Would you like to swap your weapon? yes/no");
            userInput = Console.ReadLine().ToLower();
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
}
