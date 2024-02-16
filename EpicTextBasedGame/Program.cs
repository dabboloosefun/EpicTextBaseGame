class Program
{
    public static void Main()
    {
        // Our main code code here, preferably everything is dynamic (so lots of methods).
        Monster Bob = new Monster(1, "Sick Goblin", 5, 5, 10);
        Bob.DisplayStats();

        Player player = new Player();
        player.Weapons.Add(new Weapon(World.Weapons.Count+1, "rusty sword", 2)); //World is static so we don't need to instantiate it
        //World.cs defines weapon ID's by type but I think it would be better to define ID's by incrementing the amount of existing weapons
        player.Weapons.Add(new Weapon(World.Weapons.Count+1, "Gun", 20));
        player.Weapons.Add(new Weapon(World.Weapons.Count+1, "Magic stick", 0));

        //you wakt up, given choices, and one of them is
        Console.WriteLine("3. View Weapons");
        //if (userInput == 3)
        player.ListWeapons();
        player.PromptSelectWeapon();
    }
}
