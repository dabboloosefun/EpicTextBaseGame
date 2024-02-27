public static class World
{

    public static readonly List<Weapon> Weapons = new List<Weapon>();
    public static readonly List<Monster> Monsters = new List<Monster>();
    public static readonly List<Quest> Quests = new List<Quest>();
    public static readonly List<Location> Locations = new List<Location>();
    public static readonly Random RandomGenerator = new Random();

    public const int WEAPON_ID_RUSTY_SWORD = 1;
    public const int WEAPON_ID_CLUB = 2;

    public const int MONSTER_ID_RAT = 1;
    public const int MONSTER_ID_SNAKE = 2;
    public const int MONSTER_ID_GIANT_SPIDER = 3;
    public const int MONSTER_ID_FACELESS = 4;
    public const int MONSTER_ID_ALIEN = 5;

    public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
    public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;
    public const int QUEST_ID_COLLECT_SPIDER_SILK = 3;

    public const int LOCATION_ID_HOME = 1;
    public const int LOCATION_ID_TOWN_SQUARE = 2;
    public const int LOCATION_ID_GUARD_POST = 3;
    public const int LOCATION_ID_ALCHEMIST_HUT = 4;
    public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
    public const int LOCATION_ID_FARMHOUSE = 6;
    public const int LOCATION_ID_FARM_FIELD = 7;
    public const int LOCATION_ID_BRIDGE = 8;
    public const int LOCATION_ID_SPIDER_FIELD = 9;
    public const int LOCATION_ID_FIELD_SOUTH = 10;
    public const int LOCATION_ID_ARENA = 11;

    static World()
    {
        PopulateWeapons();
        PopulateMonsters();
        PopulateQuests();
        PopulateLocations();
    }


    public static void PopulateWeapons()
    {
        Weapons.Add(new Weapon(WEAPON_ID_RUSTY_SWORD, "Rusty sword", 5, 0.05));
        Weapons.Add(new Weapon(WEAPON_ID_CLUB, "Club", 10, 0.05));
    }

    public static void PopulateMonsters()
    {
        Monster rat = new Monster(MONSTER_ID_RAT, "rat", 3, 8, 8, 8, new List<LootDrop>(), @"
                                 __             _,-""~^""-.
                               _// )      _,-""~`         `.
                             ."" ( /`""-,-""`                 ;
                            / 6                             ;
                           /           ,             ,-""     ;
                          (,__.--.      \           /        ;
                           //'   /`-.\   |          |        `._________
                             _.-'_/`  )  )--...,,,___\     \-----------,)
                           (((""~` _.-'.-'           __`-.   )         //
                                (((""`             (((---~""`         //
                                                                    ((________________
                                                                    `----""""""""~~~~^^^```
");


        Monster snake = new Monster(MONSTER_ID_SNAKE, "snake", 10, 10, 10, 10, new List<LootDrop>(), @"                                                           
                                                            _.--....
                                                 _....---;:'::' ^__/
                                               .' `'`___....---=-'`
                                              /::' (`
                                              \'   `:.
                                               `\::.  ';-"""":::-._  {}
                                            _.--'`\:' .'`-.`'`.' `{I}
                                         .-' `' .;;`\::.   '. _: {-I}`\
                                       .'  .:.  `:: _):::  _;' `{=I}.:|
                                      /.  ::::`"":::` ':'.-'`':. {_I}::/
                                      |:. ':'  :::::  .':'`:. `'|':|:'
                                       \:   .:. ''' .:| .:, _:./':.|
                                        '--.:::...---'\:'.:`':`':./
                                                       '-::..:::-'
");


        Monster giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER, "giant spider", 6, 16, 16, 16, new List<LootDrop>(), @"
                                 />\\//\\/>\                     /<\//\\//<\
                                  \Y  \>
                                />  //\ />\ \>    .;`'`/`;<\   ;/> /> \>/ \\: \>
                               />  //: />  \> \>    o o    /> /> />\>  \\  \>
                              />  //  /> \> \> \>  oO Oo />        \\  \>
                             />  //  />   `;.;. . (  ..  )  .;`;`     \>  \\  \>
                           />  //: />           \\  \>
                          /> // />             \>                          <\ \\ <\
                          \\ \>
                         />// />                  /`//\                      <\ \\<\
                        \\ \>
                        />///>                   _//\ \/                       <\\\<\
                       \\\>
                       />//>                                                     <\\<\
                      \\>
                        \\(                                                       )//
                          \\                                                     // 
");


        Monster faceless = new Monster(MONSTER_ID_FACELESS, "reaper", 20, 30, 30, 30, new List<LootDrop>(), @"
                                                                 .""--..__
                                             _                     []       ``-.._
                                          .'` `'.                  ||__           `-._
                                         /    ,-.\                 ||_ ```---..__     `-.
                                        /    /:::\\               /|//}          ``--._  `.
                                        |    |:::||              |////}                `-. \
                                        |    |:::||             //'///                    `.\
                                        |    |:::||            //  ||'                      `|
                                        /    |:::|/        _,-//\  ||
                                       /`    |:::|`-,__,-'`  |/  \ ||
                                     /`  |   |'' ||           \   |||
                                   /`    \   |   ||            |  /||
                                 |`       |  |   |)            \ | ||
                                |          \ |   /      ,.__    \| ||
                                /           `         /`    `\   | ||
                               |                     /        \  / ||
                               |                     |        | /  ||
                               /         /           |        `(   ||
                              /          .           /          )  ||
                             |            \          |     ________||
                            /             |          /     `-------.|
                           |\            /          |              ||
                           \/`-._       |           /              ||
                            //   `.    /`           |              ||
                           //`.    `. |             \              ||
                          ///\ `-._  )/             |              ||
                         //// )   .(/               |              ||
                         ||||   ,'` )               /              //
                         ||||  /                    /             || 
                         `\\` /`                    |             // 
                             |`                     \            ||  
                            /                        |           //  
                          /`                          \         //   
                        /`                            |        ||    
                        `-.___,-.      .-.        ___,'        (/    
                                 `---'`   `'----'`
");
        Monster alien = new Monster(MONSTER_ID_ALIEN, "alien", 25, 60, 60, 0, new List<LootDrop>(), @"
                                                                                     / /
                                                                                  | | |  /
                                                                                   \|_|_/
                                                                                 ,--/.__/--'
                                                 _.-/   _   \-._                    .'|
                                               .'::(_,-' `-._)::`.                  |:|
                                              (:::::::::::::::::::)                .':|
                                               \_:::;;;::::;;;:::/    /            |::|
                                       \        ,---'..\::/..`-.'    /             |::|
                                        \       \_;:....|'...:_ )   /             .'=||
                                         \.       )---. )_.--< (   /'             ||=||
                                          \\     //|:: /--\:::\\\ //             _||= |
                                           \\   ||::\:|----|:/:||/--.______,--==' \ - /
                                    -._     \`.  \\:|:|-- -|:\:/-.,,\\  .----'//'_.`-'
                                \.     `-.   \ \ _|:|:|-- -|::||::\,,||-'////---' |/'
                                 \\       `._)\ / |\/:|-/|--\:/|. :\_,'---'       /
                                  \\_      /,,\/:.'\\/-.'`-.-//  \ |
                                  /`\-    //,,,' |-.\-'\--/|-/ ./| |             /
                                   /||-   ||, /| |\. |.-==-.| . /| |            | /
                           __  |    /||-  ||,,\- | .  \;;;;/ .  .':/         _,-=/-'
                          /  \//    /||-  ' `,-|::\ . \,..,/   /: /         /.-'
                          ,--||      /||-/.-.'  \  `._ `--' _.' .'|        //
                          .--||`.    /||//\ '   |-'.___`___' _,'|//       /;
                            /\| \     ///\ /     \\_`-.`--`-'_==|'       /;'
                           / |'\ \.   //\ /       \_\__)\`==-_'_|       / /
                            .'  \.=`./|\ /          \`-- \--._/_/------( /
                                 \.=| `_/|-          |\`-| -/| `--------'
                                  \\` ./|-/-         |\`-| |-|     ________
                                   `--\ |=|'        _|\`-| |-|----'.-._ ..\`-.
                                       -|-|-     .-':`-;-| |./.-.-( | ||..|=-\\
                                       `'= /'   / ,--.:|-| ||_|_|_|_|-'__ .`-._)
                                        /|-|- .' /\ \ \|-` \\ ____,---'  `-. ..|
                                         /\=\/..'\ \_>-'`-\ \'              \ .|
                                         `,-':/\`.>' |\/ \/\ \              `- |
                                         //::/\ \/_/|-' \/| \ `.            / ||
                                        / (:|\ \/) \ \|.'-'  `-\\          |;:|\_
                                       || |:`-/:.'-|-' \|       \\          `;_\-`-._
                                       \\=/:_/::/\| \|          |\\            `-._ =`-._
                                        \_)' |:|                | //               `--.__`-.
                                             |:|                                         )\|
                                             /;/                                         / (\_
                                            / /                                         |\\;;_`-.
                                          _/ /                                          ' `---\.-\
                                         /::||
                                        /:::/
                                       //;;'
                                      `-'
");


        Monsters.Add(rat);
        Monsters.Add(snake);
        Monsters.Add(giantSpider);
        Monsters.Add(faceless);
        Monsters.Add(alien);
    }

    public static void PopulateQuests()
    {
        Quest clearAlchemistGarden =
            new Quest(
                QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
                "Clear the alchemist's garden",
                "Kill rats in the alchemist's garden ",
                MonsterByID(1),
                3);



        Quest clearFarmersField =
            new Quest(
                QUEST_ID_CLEAR_FARMERS_FIELD,
                "Clear the farmer's field",
                "Kill snakes in the farmer's field",
                MonsterByID(2),
                3);


        Quest clearSpidersForest =
            new Quest(
                QUEST_ID_COLLECT_SPIDER_SILK,
                "Collect spider silk",
                "Kill spiders in the spider forest",
                MonsterByID(3),
                3);


        Quests.Add(clearAlchemistGarden);
        Quests.Add(clearFarmersField);
        Quests.Add(clearSpidersForest);
    }

    public static void PopulateLocations()
    {
        // Create each location
        Location home = new Location(LOCATION_ID_HOME, "Home", "Your house. You really need to clean up the place.", null, null);

        Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town square", "You see a fountain.", null, null);

        Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut", "There are many strange plants on the shelves.", QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN), null);

        Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden", "Many plants are growing here.", null, MonsterByID(MONSTER_ID_RAT));

        Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse", "There is a small farmhouse, with a farmer in front.", QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD), null);

        Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmer's field", "You see rows of vegetables growing here.", null, MonsterByID(MONSTER_ID_SNAKE));

        Location fieldsouth = new Location(LOCATION_ID_FIELD_SOUTH, "Cornfield", "The corn rustles in the wind.", null, MonsterByID(MONSTER_ID_FACELESS));

        Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard post", "There is a large, tough-looking guard here.", null, null);

        Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "A stone bridge crosses a wide river.", QuestByID(QUEST_ID_COLLECT_SPIDER_SILK), null);

        Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest", "You see spider webs covering covering the trees in this forest.", null, MonsterByID(MONSTER_ID_GIANT_SPIDER));

        Location arena = new Location(LOCATION_ID_ARENA, "EndArena", "The final boss", null, MonsterByID(MONSTER_ID_ALIEN));

        // Link the locations together
        home.LocationToNorth = townSquare;

        townSquare.LocationToNorth = alchemistHut;
        townSquare.LocationToSouth = home;
        townSquare.LocationToEast = guardPost;
        townSquare.LocationToWest = farmhouse;

        farmhouse.LocationToEast = townSquare;
        farmhouse.LocationToWest = farmersField;

        fieldsouth.LocationToNorth = farmersField;

        farmersField.LocationToEast = farmhouse;
        farmersField.LocationToSouth = fieldsouth;

        alchemistHut.LocationToSouth = townSquare;
        alchemistHut.LocationToNorth = alchemistsGarden;

        alchemistsGarden.LocationToSouth = alchemistHut;

        guardPost.LocationToEast = bridge;
        guardPost.LocationToWest = townSquare;

        bridge.LocationToWest = guardPost;
        bridge.LocationToEast = spiderField;

        spiderField.LocationToWest = bridge;

        // Add the locations to the static list
        Locations.Add(home);
        Locations.Add(townSquare);
        Locations.Add(guardPost);
        Locations.Add(alchemistHut);
        Locations.Add(alchemistsGarden);
        Locations.Add(farmhouse);
        Locations.Add(farmersField);
        Locations.Add(bridge);
        Locations.Add(spiderField);
        Locations.Add(fieldsouth);
        Locations.Add(arena);
    }

    public static Location LocationByID(int id)
    {
        foreach (Location location in Locations)
        {
            if (location.ID == id)
            {
                return location;
            }
        }

        return null;
    }

    public static Weapon WeaponByID(int id)
    {
        foreach (Weapon item in Weapons)
        {
            if (item.ID == id)
            {
                return item;
            }
        }

        return null;
    }

    public static Monster MonsterByID(int id)
    {
        foreach (Monster monster in Monsters)
        {
            if (monster.ID == id)
            {
                return monster;
            }
        }

        return null;
    }

    public static Quest QuestByID(int id)
    {
        foreach (Quest quest in Quests)
        {
            if (quest.ID == id)
            {
                return quest;
            }
        }

        return null;
    }
}
