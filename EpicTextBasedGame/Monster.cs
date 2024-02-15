public class Monster
{
    public int MaxHealth;
    public int CurrentHealth;
    public string Name;
    public int ID;
    public int MaxDamage;
    public Monster(int id, string name, int maxDamage, int currentHealth , int maxHealth)
    {
        this.ID = id;
        this.Name = name;
        this.MaxDamage = maxDamage;
        this.MaxHealth = maxHealth;
        this.CurrentHealth = currentHealth;
    }


    public void DisplayStats()
    {
        Console.WriteLine($"Name: {this.Name}\nHealth: {this.CurrentHealth}/{this.MaxHealth}");
    }


    // Te gebruiken voor Monster healing in gevecht-scenario
    public void RegenarateHealth(int recoveredHealth)
    {
        this.CurrentHealth += recoveredHealth;
        if (this.CurrentHealth > this.MaxHealth)
        {
            this.CurrentHealth = this.MaxHealth;
        }
        Console.WriteLine($"{this.Name} HP: {this.CurrentHealth}");
    }


    // Te gebruiken voor als Monster wordt beschadigd in gevecht-scenario
    public void TakeDamage(int totalDamage)
    {
        this.CurrentHealth -= totalDamage;
        if (this.CurrentHealth < 0)
        {
            this.CurrentHealth = 0;
        }
        Console.WriteLine($"{this.Name} HP: {this.CurrentHealth}");
    }


    // Te gebruiken voor als Monster MaxHealth wordt gebuffed in gevecht-scenario
    public void RaiseMaxHealth(int raisedMaxHealth)
    {
        this.MaxHealth += raisedMaxHealth;
        Console.WriteLine($"{this.Name}'s maximum health has been raised to {this.MaxHealth}!");
    }


    // Te gebruiken voor als Monster MaxHealth wordt gedebuffed in gevecht-scenario.
    public void LowerMaxHealth(int loweredMaxHealth)
    {
        this.MaxHealth -= loweredMaxHealth;
        if (this.MaxHealth < 1)
        {
            this.MaxHealth = 1;
            Console.WriteLine($"{this.Name} is already at 1 max health!");
        }
        Console.WriteLine($"{this.Name}'s maximum health has been lowered to {this.MaxHealth}!");
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