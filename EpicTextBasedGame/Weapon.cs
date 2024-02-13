public class Weapon
{
    int ID;
    string Name;
    int Strength; //? Weapon() call in World.cs gives 3 arguments
    public Weapon(int id, string name, int strength){
        ID = id;
        Name = name;
        Strength = strength;
    }

}