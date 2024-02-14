public class Monster
{
    public int monsterHP;
    public int monsterDMG;
    public Monster(string healthPoints, string damagePoints)
    {
        monsterHP = Convert.ToInt32(healthPoints);
        monsterDMG = Convert.ToInt32(damagePoints);
    } 
    public void DisplayStats()
    {
        Console.WriteLine(monsterHP + monsterDMG);
    }
}