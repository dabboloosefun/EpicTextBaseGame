public class Monster
{
    public int MaximumHitpoints;
    public int CurrentHitpoints;
    public string Name;
    public int ID;
    public int MaximumDamage;
    public Monster(int id, string name, int maximumDamage, int maximumHitpoints, int currentHitpoints)
    {
        this.ID = id;
        this.Name = name;
        this.MaximumDamage = maximumDamage;
        this.MaximumHitpoints = maximumHitpoints;
        this.CurrentHitpoints = currentHitpoints;
    } 
    public void DisplayStats()
    {
        Console.WriteLine(this.ID + this.Name + this.MaximumDamage + this.MaximumHitpoints + this.CurrentHitpoints);
    }
}