public class Monster
{
    public int monsterHP;
    public int monsterDMG;
    public Monster(string healthPoints, string damagePoints)
    {
        monsterHP = healthPoints;
        monsterDMG = damagePoints;
    } 
    public void DisplayStats()
    {
        Console.WriteLine(monsterHP + monsterDMG);
    }
}