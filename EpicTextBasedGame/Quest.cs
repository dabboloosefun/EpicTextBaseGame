using System.IO.Compression;
using Microsoft.VisualBasic;

public class Quest
{
    public int ID;
    public string Name;
    public string Description;
    public bool Cleared = false;
    public Monster Target;
    public int TargetKills;
    public int CurrentKills;
    public object? QuestReward;
    public Quest(int id, string name, string description, Monster target, int targetKills, object? questReward = null)
    {
        ID = id;
        Name = name;
        Description = description;
        Target = target;
        TargetKills = targetKills;
        CurrentKills = 0;
        QuestReward = questReward;
    }

    public void StartQuest(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Helper.CenterStr($"Quest started: {Name}"));
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(Helper.CenterStr(Description));
        Console.WriteLine("\n");
        player.QuestList.Add(this);
        if (player.CurrentLocation.ID == World.LOCATION_ID_FARMHOUSE) player.CurrentLocation.AdjustDescription("The farmer is waiting impatiently.");
        if (player.CurrentLocation.ID == World.LOCATION_ID_ALCHEMIST_HUT) player.CurrentLocation.AdjustDescription("The alchemist is gazing at you intently.");
        if (player.CurrentLocation.ID == World.LOCATION_ID_BRIDGE) player.CurrentLocation.AdjustDescription("The councilman seems to be at odds with eachother.");
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
        if (QuestReward != null)
        {
            if (QuestReward is Weapon)
            {
                Weapon reward = (Weapon)QuestReward;
                player.Weapons.Add(reward);
                Console.WriteLine(Helper.CenterStr($"You got a(n) {reward.Name}!"));
                Console.WriteLine("\n");
            } 
            if (QuestReward is Item) 
            {
                Item reward = (Item)QuestReward;
                player.Items.Add(reward);
                Console.WriteLine(Helper.CenterStr($"You got a(n) {reward}!"));
                Console.WriteLine("\n");
            }
        }
        if (Name == "Collect spider silk") EndBoss(player);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Helper.CenterStr($"Quest cleared: {Name}!"));
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.White;
        if (player.CurrentLocation.ID == World.LOCATION_ID_FARMHOUSE) player.CurrentLocation.AdjustDescription("The farmer is pleased.");
        if (player.CurrentLocation.ID == World.LOCATION_ID_ALCHEMIST_HUT) player.CurrentLocation.AdjustDescription("The alchemist pays you no heed.");
        Thread.Sleep(1000);
    }

    public void EndBoss(Player player)
    {
        player.CurrentLocation = World.Locations[10];
        SuperAdventure.Fight(player, player.CurrentLocation.MonsterLivingHere);
        Helper.OutroScreen();
    }
}