﻿using System.Runtime.CompilerServices;

public class Location{
    public int ID;
    public string Name;
    public string Description;
    public Quest QuestAvailableHere;
    public Monster MonsterLivingHere;
    //these are filled in World.cs
    public Location? LocationToNorth;
    public Location? LocationToEast;
    public Location? LocationToSouth;
    public Location? LocationToWest;

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
        Description = newDescription;
    }

    public Monster GetMonster() => MonsterLivingHere; // Miss nuttig als je combat wilt starten

    public void StartLocationQuest(Player player)
    {
        //if player.Inventory contains certain item > I'd rather have you be prompted to start a quest regardless (MMO style: speak to NPC: y/n)
        QuestAvailableHere.StartQuest(player);

    }

    public string Map()
    {
        string map = $@"
                                    *********┌─────────────┐***  ≈≈≈≈≈      LEGEND
                                    ***      │ .....P..... │*  ≈≈≈≈≈        H: Home
                                             │     ...     │  ≈≈≈≈≈         T: Town Square
                                             └──┐   ▄   ┌──┘ ≈≈≈≈≈          F: Farmhouse
                                     <=>        │ ▄███▄ │    ≈≈≈≈≈          V: Farmer's Field
                                        ██      │  ▀A▀  │    ≈≈≈≈≈          A: Alchemist's Hut
                                         ▐      └───|───┘    ≈≈≈≈≈          P: Alchemist's Garden
                                                   /        ≈≈≈≈≈           G: Guard Post
                                    _____     ▐▀  /         ≈≈≈≈    *       B: Bridge
                                    =====▐▀   ▀█ | █▀█     ≈≈≈≈   ***       S: Forest
                                    =====F___   ┌───┐      ≈≈≈   ****       
                                    =====    \__│   │▄  ▄▄≈≈≈≈  *****
                                    __V__     ▀▀│ T │---G╠═B═╣--*****
                                    =====       │   │█  ▀▀≈≈≈  ******
                                    =====     ▀█└───┘     ≈≈≈  **S***
                                    =====       |▐█▀     ≈≈≈  *******       Current Location: {Name}
                                           *     \     ≈≈≈≈ *********
                                       ▄▌ ***    |     ≈≈≈≈ *********
                                    **********   ▀   ≈≈≈≈≈ **********
                                    ***********  H   ≈≈≈≈≈ **********
        ";
        return map;
    }

    public string Compass()
    {
        string compass = Helper.CenterStr("From here you can go:\n");
        if (LocationToNorth != null)
        {
            compass += "\t\t\t\t\t\t\t    N\n\t\t\t\t\t\t\t    |\n";
        }
        if (LocationToWest != null)
        {
            compass += "\t\t\t\t\t\t\tW---|";
        }
        else
        {
            compass += "\t\t\t\t\t\t\t    |";
        }
        if (LocationToEast != null)
        {
            compass += "---E";
        }
        compass += "\t\t\t\t\t\t\t\t\n";
        if (LocationToSouth != null)
        {
            compass += "\t\t\t\t\t\t\t    |\n\t\t\t\t\t\t\t    S\n";
        }
        return compass;
    }

    public Location? GetLocationAt(string location)
    {
        if (location == "n") return LocationToNorth;
        if (location == "e") return LocationToEast;
        if (location == "s") return LocationToSouth;
        if (location == "w") return LocationToWest;
        return null;
    }
}