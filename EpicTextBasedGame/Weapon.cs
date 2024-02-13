public class Weapon
{
    int ID;
    string Name;
    int MaximumDamage; //? Weapon() call in World.cs gives 3 arguments
    public Weapon(int id, string name, int maximumDamage){
        ID = id;
        Name = name;
        MaximumDamage = maximumDamage;
    }

}