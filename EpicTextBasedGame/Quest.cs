public class Quest
{
    public int ID;
    public string Name;
    public string Description;
    public bool Cleared = false;
    public Quest(int id, string name, string description)
    {
        ID = id;
        Name = name;
        Description = description;
    }

    public void StartQuest(){

    }
    public void ClearQuest(Player player, string clearModifier = "")
    {
        Cleared = true;

        if(clearModifier == "Within3Hits")
        {
            Player.Inventory.Add("Jar Of Llightning");
        }

        if(clearModifier == "SparedGoblin")
        {
            Player.Inventory.Add("Goblin's Blessing");
        }

        if(clearModifier == "SlayedGoblin")
        {
            Player.CurrentHealth -= 1;
            Player.Inventory.Add("Goblin's Curse");
        }
    }

    
}