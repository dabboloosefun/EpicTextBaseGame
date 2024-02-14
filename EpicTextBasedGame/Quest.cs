public class Quest
{
    int ID;
    string Name;
    string Description;
    bool Cleared = false;
    public Quest(int id, string name, string description)
    {
        ID = id;
        Name = name;
        Description = description;
    }

    public void ClearQuest(Player player, string bonusModifier = "")
    {
        Cleared = true;

        if(bonusModifier == "Within3Hits")
        {
            Player.Inventory.Add("Jar Of Llightning");
        }

        if(bonusModifier == "SparedGoblin")
        {
            Player.Inventory.Add("Goblin's Blessing");
        }

        if(bonusModifier == "SlayedGoblin")
        {
            Player.CurrentHealth -= 1;
            Player.Inventory.Add("Goblin's Curse");
        }
    }
}