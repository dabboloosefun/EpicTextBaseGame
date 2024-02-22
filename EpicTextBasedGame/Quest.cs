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
        Console.ForegroundColor = ConsoleColor.White;
        player.QuestList.Add(this);
    }
    
    public void UpdateQuest()
    {
        CurrentKills++;
        if (CurrentKills == TargetKills) EndQuest();;   
    }

    public void EndQuest()
    {
        Cleared = true;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Helper.CenterStr($"You've cleared {Name}!"));
        Console.ForegroundColor = ConsoleColor.White;
    }
}