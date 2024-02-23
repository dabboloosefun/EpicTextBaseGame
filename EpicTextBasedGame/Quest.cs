using System.IO.Compression;

public class Quest
{
    public int ID;
    public string Name;
    public string Description;
    public bool Cleared = false;
    public Monster Target;
    public int TargetKills;
    public int CurrentKills;
    public Quest(int id, string name, string description, Monster target, int targetKills)
    {
        ID = id;
        Name = name;
        Description = description;
        Target = target;
        TargetKills = targetKills;
        CurrentKills = 0;
    }

    public void StartQuest(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Helper.CenterStr($"Quest started: {Name}"));
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.White;
        player.QuestList.Add(this);
        if (player.CurrentLocation.Name == "Farmhouse") player.CurrentLocation.AdjustDescription("The farmer is waiting impatiently");
    }
    
    public void UpdateQuest(Player player)
    {
        CurrentKills++;
        if (CurrentKills == TargetKills) EndQuest(player);
    }

    public void EndQuest(Player player)
    {
        player.Experience += 50;
        Cleared = true;
        if (Name == "Collect spider silk") Helper.OutroScreen();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Helper.CenterStr($"Quest cleared: {Name}!"));
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.White;
        if (player.CurrentLocation.Name == "Farmer's field") player.CurrentLocation.LocationToEast.AdjustDescription("The farmer is pleased");
    }
}