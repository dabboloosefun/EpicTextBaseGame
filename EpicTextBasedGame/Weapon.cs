public class Weapon
{
    public int ID;
    public string Name;
    public int MaximumDamage; //? Weapon() call in World.cs gives 3 arguments
    public Weapon(int id, string name, int maximumDamage){
        ID = id;
        Name = name;
        MaximumDamage = maximumDamage;
    }

    public int RollDamage(int minOffset = 0, int maxOffset = 0){
        Random rand = new();
        return rand.Next(MaximumDamage-minOffset, MaximumDamage+maxOffset);
    }

}