public class Location{
    public int ID;
    public string Name;
    public string Description;
    public Quest QuestAvailableHere;
    public Monster MonsterLivingHere;
    //these are filled in World.cs
    public Location LocationToNorth;
    public Location LocationToEast;
    public Location LocationToSouth;
    public Location LocationToWest;

    public Location(int id, string name, string description, Quest quest, Monster monster)
    {
        ID = id;
        Name = name;
        Description = description;
        QuestAvailableHere = quest;
        MonsterLivingHere = monster;
    }

    public void AdjustDescription(string newDescription) //Flexibiliteit is altijd fijn.
    {
        Description = newDescription
    }

    public Monster GetMonster() => MonsterLivingHere; // Miss nuttig als je combat wilt starten

    public void StartQuest()
    {
        //if player.Inventory contains certain item > I'd rather have you be prompted to start a quest regardless (MMO style: speak to NPC: y/n)
        QuestAvailableHere.StartQuest();

    }

    public string Compass()
    {
        string compass = "From here you can go:\n";
        if (LocationToNorth != null)
        {
            compass += "    N\n    |\n";
        }
        if (LocationToWest != null)
        {
            compass += "W---|";
        }
        else
        {
            compass += "    |";
        }
        if (LocationToEast != null)
        {
            compass += "---E";
        }
        s += "\n";
        if (LocationToSouth != null)
        {
            compass += "    |\n    S\n";
        }
        return compass;
    }

    public Location GetLocationAt(string location)
    {
        if (location == "N") return LocationToNorth;
        if (location == "E") return LocationToEast;
        if (location == "S") return LocationToSouth;
        if (location == "W") return LocationToWest;
        return null;
    }
}