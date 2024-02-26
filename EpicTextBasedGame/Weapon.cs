public class Weapon
{
    public int ID;
    public string Name;
    public int MaxDamage;
    public double CritChance;
    public Weapon(int id, string name, int maxDamage, double critChance)
    {
        this.ID = id;
        this.Name = name;
        this.MaxDamage = maxDamage;
        this.CritChance = critChance;
    }


    // Ter gebruik in monster.TakeDamage(player.currentWeapon.RollDamage()) method
    public int RollDamage(int minimumDamage = 0)
    {
        Random rand = new Random();
        minimumDamage = minimumDamage == 0 ? (Convert.ToInt32(this.MaxDamage * 0.8)) : minimumDamage;
        int rolledDamage = rand.Next(minimumDamage, Convert.ToInt32(this.MaxDamage));

        if (rand.NextDouble() <= this.CritChance) return (rolledDamage * 2);
        return rolledDamage;
    }


    // Ter gebruik voor als player damage wordt gebuffed 
    public void RaiseMaxDamage(int raisedDamage)
    {
        this.MaxDamage += raisedDamage;
        //Console.WriteLine($"{Name}'s damage has been raised!");
    }


    // Ter gebruik voor als player damage wordt gedebuffed
    public void LowerMaxDamage(int loweredDamage)
    {
        this.MaxDamage -= loweredDamage;
        if (this.MaxDamage < 0)
        {
            this.MaxDamage = 0;
        }
        Console.WriteLine($"{Name}'s damage has been lowered!");
    }


    // Might be useful if you ever would want to upgrade a weapon (or maybe a new weapon would be easier but eh.)
    public void ChangeName(string newName)
    {
        this.Name = newName;
    }
}