public class Weapon
{
    public int ID;
    public string Name;
    public int MaxDamage;
    public int CritChance; //Unused for now, but might be useful later
    public Weapon(int id, string name, int maxDamage)
    {
        this.ID = id;
        this.Name = name;
        this.MaxDamage = maxDamage;
    }


    // Ter gebruik in monster.TakeDamage(player.currentWeapon.RollDamage()) method
    public int RollDamage(int minimumDamage = 0)
    {
        Random rand = new Random();
        minimumDamage = minimumDamage = 0 ? (this.MaxDamage - 3) : minimumDamage;
        return rand.Next(minimumDamage, this.MaxDamage + 1);
    }


    // Ter gebruik voor als player damage wordt gebuffed 
    public void RaiseMaxDamage(int raisedDamage)
    {
        this.MaxDamage += raisedDamage;
        Console.WriteLine($"{Name}'s damage has been raised!"); 
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