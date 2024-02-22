public class Monster : Character
{

    public int ID;
    public int MaxDamage;
    public List<(Item item, int turns, bool applied)> ActiveEffects;
    public Monster(int id, string name, int maxDamage, int currentHealth , int maxHealth)
    {
        this.ID = id;
        this.Name = name;
        this.MaxDamage = maxDamage;
        this.MaxHealth = maxHealth;
        this.CurrentHealth = currentHealth;
        this.ActiveEffects = new List<(Item item, int turns, bool applied)>(); 
    }


    public void DisplayStats()
    {
        Console.WriteLine("\t\t\t\t\t╔═══════════════════════════════════╗");
        Console.WriteLine("\t\t\t\t\t║               {0, -20}║", $"{this.Name}");
        Console.WriteLine("\t\t\t\t\t╠═══════════════════════════════════╣");
        Console.WriteLine("\t\t\t\t\t║ HEALTH:        {0, -8}           ║", $"{this.CurrentHealth}/{this.MaxHealth}");
        Console.WriteLine("\t\t\t\t\t║ MAXDMG:        {0, -10}         ║", $"{this.MaxDamage}");
        Console.WriteLine("\t\t\t\t\t╚═══════════════════════════════════╝");
    }
    
    public int RollDamageMonster(int minimumDamage = 0)
    {
        Random rand = new Random();
        minimumDamage = minimumDamage == 0 ? (Convert.ToInt32(this.MaxDamage * 0.8)) : minimumDamage;
        int rolledDamage = rand.Next(minimumDamage, Convert.ToInt32(this.MaxDamage * 1.2));

        if (rand.NextDouble() <= 0.9) return (rolledDamage * 2); // For now 0.9, can be changed to needing to be declared
        else {return rolledDamage;}
    }



    // Ter gebruik voor Monster attack buffs
    public void RaiseMaxDamage(int raisedDamage)
    {
        this.MaxDamage += raisedDamage;
        Console.WriteLine($"{this.Name}'s attack has been raised!");
    }


    // Ter gebruik voor Monster attack debuffs
    public void LowerMaxDamage(int loweredMaxDamage)
    {
        this.MaxDamage -= loweredMaxDamage;
        if (this.MaxDamage < 0)
        {
            this.MaxDamage = 0;
        }
        Console.WriteLine($"{this.Name}'s attack has been lowered!");
    }
}