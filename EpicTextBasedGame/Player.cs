public class Player
{
    public List<Weapon> Weapons;
    public List<string> Items;
    public Weapon CurrentWeapon;
    public Player()
    {
        Weapons = new List<Weapon>();
        Items = new List<string>();
    }

    public void ListWeapons()
    {
        Console.WriteLine($"Equiped weapon: {CurrentWeapon.Name}: Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}\n");
        for(int i = 0; i<Weapons.Count(); i++) //we can just use the index in the list to select a new weapon with
        {
            Weapon currentWeapon = Weapons[i];
            Console.WriteLine($"{i}. {currentWeapon.Name}: Max damage: {currentWeapon.MaxDamage}, Crit chance: {currentWeapon.CritChance}\n"); 
        }
    }

    // Readability people>>>
    public void PromptSelectWeapon()
    {
        string userInput = "";
 
        do{
            Console.WriteLine("Would you like to swap your weapon? yes/no");
            userInput = Console.ReadLine().ToLower();
        } while (userInput != "yes" && userInput != "no");

        if(userInput == "no") return;
        else if(userInput == "yes")
        {
            string userSelectInput;
            int selectedNumber;
            bool succesfulParse;
            do{
                
                Console.WriteLine($"Enter the number of the weapon you wish to equip. 0-{Weapons.Count()-1}");
                userSelectInput = Console.ReadLine();
                succesfulParse = int.TryParse(userSelectInput, out selectedNumber);
            } while (!succesfulParse || !(0 <= selectedNumber && selectedNumber < Weapons.Count())); //if Weapons.Count() = 3, valid inputs are 0, 1, 2. < is exclusive
            Weapons.Add(CurrentWeapon);
            CurrentWeapon = Weapons[selectedNumber];
            Weapons.Remove(Weapons[selectedNumber]); //even if weapons have the same name, they still have different ID so this hould work > Why remove the weapon from the list? You still have it in your inv
            Console.WriteLine($"You've now equiped this new weapon! {CurrentWeapon.Name}: Max damage: {CurrentWeapon.MaxDamage}, Crit chance: {CurrentWeapon.CritChance}");
        }
    }
}