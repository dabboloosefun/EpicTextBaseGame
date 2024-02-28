using System.Runtime.CompilerServices;

public class Location{
    public int ID;
    public string Name;
    public bool Proceed = false;
    public string Description;
    public Quest? QuestAvailableHere;
    public Monster? MonsterLivingHere;
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
        if (player.QuestList.Contains(QuestAvailableHere)) return;
        string? userInput = "";

        do
        {
            Console.WriteLine(Helper.CenterStr("Would you like to accept quest? Y/N"));
            userInput = Console.ReadLine().ToLower();
            Helper.ClearLastLine();
        } while (userInput != "y" && userInput != "n");
        if (userInput == "y") QuestAvailableHere.StartQuest(player);
        else Console.Clear();
    }

    public void StartEndGame(Player player)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Helper.CenterStr("The sole guard manning the gate is in your way"));
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.White;
        int completed = 0;
        foreach (Quest quest in player.QuestList)
        {
            if (quest.Cleared is true)
            {
                completed += 1;
            }
        }
        if (Proceed is false) {
            if (completed == 2)
            {
                Console.WriteLine(Helper.CenterStr("You're up to the challenge, proceed.\n"));
                Thread.Sleep(2000);
                Proceed = true;
            }
            else
            {
                Console.WriteLine(Helper.CenterStr("You're not allowed beyond this point!"));
                Console.WriteLine(Helper.CenterStr("I'll have you escorted back to the town.\n"));
                string userInput;
                do
                {
                    Console.WriteLine(Helper.CenterStr("Attack guard? Y/N"));
                    userInput = Console.ReadLine().ToLower();
                    Helper.ClearLastLine();
                } while (userInput != "y" && userInput != "n");
                if (userInput == "y")
                {
                    SuperAdventure.Fight(player, new Monster(22, "guard", 20, 50, 50, 100, 100, new List<LootDrop>(), @"
                                                       _.--.    .--._
                                                     .""  .""      "".  "".
                                                    ;  .""    /\    "".  ;
                                                    ;  '._,-/  \-,_.`  ;
                                                    \  ,`  / /\ \  `,  /
                                                     \/    \/  \/    \/
                                                     ,=_    \/\/    _=,
                                                     |  ""_   \/   _""  |
                                                     |_   '""-..-""'   _|
                                                     | ""-.        .-"" |
                                                     |    ""\    /""    |
                                                     |      |  |      |
                                             ___     |      |  |      |     ___
                                         _,-"",  "",   '_     |  |     _'   ,""  ,""-,_
                                       _(  \  \   \""=--""-.  |  |  .-""--=""/   /  /  )_
                                     ,""  \  \  \   \      ""-'--'-""      /   /  /  /  "".
                                    !     \  \  \   \                  /   /  /  /     !
                                    :      \  \  \   \                /   /  /  /      :    
"));
                    player.CurrentLocation.AdjustDescription("You leave the blood gurlging guard to die by the gate.");
                    Proceed = true;
                }
                else player.CurrentLocation = player.CurrentLocation.LocationToWest;
            }
        }
    }

    public void Map()
    {
        Console.Clear();
        string map = $@"
                                    *********┌─────────────┐***  ≈≈≈≈≈      LEGEND
                                    *** +    │ .....P..... │*  ≈≈≈≈≈        H: Home
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
                                    =====     ▀█└───┘     ≈≈≈  **S***       X: Player
                                    =====       |▐█▀   <=>≈≈≈  *******       Current Location: {Name}
                                           *     \     ≈≈≈≈ *********
                                       ▄▌ ***    |     ≈≈≈≈ *********
                                    **********   ▀ H ≈≈≈≈≈ **********
        ";
        int indexPos = 0;
        switch (ID)
        {
            case World.LOCATION_ID_HOME:
                indexPos = map.IndexOf("    |     ");
                break;

            case World.LOCATION_ID_TOWN_SQUARE:
                indexPos = map.IndexOf("  │   │█");
                break;

            case World.LOCATION_ID_ALCHEMIST_HUT:
                indexPos = map.IndexOf("│ ▄███▄ ");
                break;

            case World.LOCATION_ID_ALCHEMISTS_GARDEN:
                indexPos = map.IndexOf("   ...  ");
                break;

            case World.LOCATION_ID_BURROW:
                indexPos = map.IndexOf("*** +  ");
                break;

            case World.LOCATION_ID_FARMHOUSE:
                indexPos = map.IndexOf("===F___");
                break;

            case World.LOCATION_ID_FARM_FIELD:
                indexPos = map.IndexOf("  =====       │");
                break;

            case World.LOCATION_ID_FIELD_SOUTH:
                indexPos = map.IndexOf("      ▄▌ *");
                break;

            case World.LOCATION_ID_GUARD_POST:
                indexPos = map.IndexOf(" │---G╠");
                break;

            case World.LOCATION_ID_TOMB:
                indexPos = map.IndexOf("   <=>≈");
                break;

            case World.LOCATION_ID_BRIDGE:
                indexPos = map.IndexOf("--G╠═B═");
                break;

            case World.LOCATION_ID_SPIDER_FIELD:
                indexPos = map.IndexOf(" **S***");
                break;
        }
        var newmap = ReplaceAt(map, indexPos + 4, Convert.ToChar("X"));

        WriteLineWithColoredLetter(newmap);
    }

    public static void WriteLineWithColoredLetter(string letters)
    {
        Char[] array = letters.ToCharArray();
        char[] yellow = {'S', 'B', 'G', 'P', 'A', 'V', 'F', 'T', 'H', '1', '2', '3', '4', '5' };

        foreach (Char c in array)
        {
            if (c == 'X')
            {
                Console.ForegroundColor = System.ConsoleColor.DarkRed;
                Console.Write(c);
            }
            else if (yellow.Contains(c))
            {
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.Write(c);
            }
            else
            {
                Console.ForegroundColor = System.ConsoleColor.White;
                Console.Write(c);
            }
        }
        Console.WriteLine();
    }

    public static string ReplaceAt(string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
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