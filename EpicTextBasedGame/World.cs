public static class World
{

    public static readonly List<Weapon> Weapons = new List<Weapon>();
    public static readonly List<Monster> Monsters = new List<Monster>();
    public static readonly List<Quest> Quests = new List<Quest>();
    public static readonly List<Location> Locations = new List<Location>();
    public static readonly Random RandomGenerator = new Random();

    public const int WEAPON_ID_RUSTY_SWORD = 1;
    public const int WEAPON_ID_CLUB = 2;
    public const int WEAPON_ID_POISON_DAGGER = 3;
    public const int WEAPON_ID_HAND_OF_GOD = 4;
    public const int WEAPON_ID_SCYTHE = 666;

    public const int MONSTER_ID_RAT = 1;
    public const int MONSTER_ID_SNAKE = 2;
    public const int MONSTER_ID_GIANT_SPIDER = 3;
    public const int MONSTER_ID_FACELESS = 4;
    public const int MONSTER_ID_ALIEN = 5;
    public const int MONSTER_ID_JEFF = 6; 
    public const int MONSTER_ID_SKELL = 7;
    public const int MONSTER_ID_MINOTAUR = 8;

    public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
    public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;
    public const int QUEST_ID_COLLECT_SPIDER_SILK = 3;
    public const int QUEST_ID_SLAY_MINOTAUR = 4;

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
    public const int LOCATION_ID_BURROW = 12;
    public const int LOCATION_ID_TOMB = 13;


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
        Weapons.Add(new Weapon(WEAPON_ID_SCYTHE, "Grave Scythe", 50, 0.3));
        Weapons.Add(new Weapon(WEAPON_ID_POISON_DAGGER, "Poison dagger", 8, 0.3));
        Weapons.Add(new Weapon(WEAPON_ID_HAND_OF_GOD, "Hand of God", 1000, 1));
    }

    public static void PopulateMonsters()
    {
        Monster rat = new Monster(MONSTER_ID_RAT, "rat", 4, 12, 12, 12, 10, new List<LootDrop>{new LootDrop(30, WeaponByID(2))}, @"
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
        Monster snake = new Monster(MONSTER_ID_SNAKE, "snake", 10, 12, 12, 12, 16, new List<LootDrop>{new LootDrop(50, WeaponByID(3))}, @"                                                           
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
        Monster giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER, "giant spider", 14, 26, 26, 26, 35, new List<LootDrop>(), @"
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
        Monster faceless = new Monster(MONSTER_ID_FACELESS, "reaper", 20, 30, 30, 30, 50, new List<LootDrop>{new LootDrop(100, WeaponByID(666))}, @"
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
        Monster alien = new Monster(MONSTER_ID_ALIEN, "alien", 25, 80, 80, 0, 0, new List<LootDrop>(), @"
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
        Monster jeff = new Monster(MONSTER_ID_JEFF, "jeff", 5, 80, 80, 20, 60, new List<LootDrop>(), @"
                               ,
                         (`.  : \               __..----..__
                          `.`.| |:          _,-':::''' '  `:`-._
                            `.:\||       _,':::::'         `::::`-.
                              \\`|    _,':::::::'     `:.     `':::`.
                               ;` `-''  `::::::.                  `::\
                            ,-'      .::'  `:::::.         `::..    `:\
                          ,' /_) -.            `::.           `:.     |
                        ,'.:     `    `:.        `:.     .::.          \
                   __,-'   ___,..-''-.  `:.        `.   /::::.         |
                  |):'_,--'           `.    `::..       |::::::.      ::\
                   `-'                 |`--.:_::::|_____\::::::::.__  ::|
                                       |   _/|::::|      \::::::|::/\  :|
                                       /:./  |:::/        \__:::):/  \  :\
                                     ,'::'  /:::|        ,'::::/_/    `. ``-.__
                                    ''''   (//|/\      ,';':,-'         `-.__  `'--..__
                                                                             `''---::::'
");
        Monster skeleton = new Monster(MONSTER_ID_SKELL, "skeleton", 15, 30, 30, 20, 20, new List<LootDrop>(), @"
                                              _.--""""-._
                  .                         .""         "".
                 / \    ,^.         /(     Y             |      )\
                /   `---. |--'\    (  \__..'--   -   -- -'""""-.-'  )
                |        :|    `>   '.     l_..-------.._l      .'
                |      __l;__ .'      ""-.__.||_.-'v'-._||`""----""
                 \  .-' | |  `              l._       _.'
                  \/    | |                   l`^^'^^'j
                        | |                _   \_____/     _
                        j |               l `--__)-'(__.--' |
                        | |               | /`---``-----'""1 |  ,-----.
                        | |               )/  `--' '---'   \'-'  ___  `-.
                        | |              //  `-'  '`----'  /  ,-'   I`.  \
                      _ L |_            //  `-.-.'`-----' /  /  |   |  `. \
                     '._' / \         _/(   `/   )- ---' ;  /__.J   L.__.\ :
                      `._;/7(-.......'  /        ) (     |  |            | |
                      `._;l _'--------_/        )-'/     :  |___.    _._./ ;
                        | |                 .__ )-'\  __  \  \  I   1   / /
                        `-'                /   `-\-(-'   \ \  `.|   | ,' /
                                           \__  `-'    __/  `-. `---'',-'
                                              )-._.-- (        `-----'
                                             )(  l\ o ('..-.
                                       _..--' _'-' '--'.-. |
                                __,,-'' _,,-''            \ \
                               f'. _,,-'                   \ \
                              ()--  |                       \ \
                                \.  |                       /  \
                                  \ \                      |._  |
                                   \ \                     |  ()|
                                    \ \                     \  /
                                     ) `-.                   | |
                                    // .__)                  | |
                                 _.//7'                      | |
                               '---'                         j_|
                                                            (| |
                                                             |  \
                                                             |lllj
                                                             ||||| 
");
        Monster minotaur = new Monster(MONSTER_ID_MINOTAUR, "minotaur", 13, 70, 70, 100, 40, new List<LootDrop>(), @"
                                                                                    _
                                                                                  _( (~\
                           _ _                        /                          ( \> > \
                       -/~/ / ~\                     :;                \       _  > /(~\/
                      || | | /\ ;\                   |l      _____     |;     ( \/ /   /
                      _\\)\)\)/ ;;;                  `8o __-~     ~\   d|      \   \  //
                     ///(())(__/~;;\                  ""88p;.  -. _\_;.oP        (_._/ /
                    (((__   __ \\   \                  `>,% (\  (\./)8""         ;:'  i
                    )))--`.'-- (( ;,8 \               ,;%%%:  ./V^^^V'          ;.   ;.
                    ((\   |   /)) .,88  `: ..,,;;;;,-::::::'_::\   ||\         ;[8:   ;
                     )|  ~-~  |(|(888; ..``'::::8888oooooo.  :\`^^^/,,~--._    |88::| |
                      \ -===- /|  \8;; ``:.      oo.8888888888:`((( o.ooo8888Oo;:;:'  |
                     |_~-___-~_|   `-\.   `        `o`88888888b` )) 888b88888P""""'     ;
                      ;~~~~;~~         ""`--_`.       b`888888888;(.,""888b888""  ..::;-'
                       ;      ;              ~""-....  b`8888888:::::.`8888. .:;;;''
                          ;    ;                 `:::. `:::OOO:::::::.`OO' ;;;''
                     :       ;                     `.      ""``::::::''    .'
                        ;                           `.   \_              /
                      ;       ;                       +:   ~~--  `:'  -';
                                                       `:         : .::/
                          ;                            ;;+_  :::. :..;;;
");


        Monsters.Add(rat);
        Monsters.Add(snake);
        Monsters.Add(giantSpider);
        Monsters.Add(faceless);
        Monsters.Add(alien);
        Monsters.Add(jeff);
        Monsters.Add(skeleton);
        Monsters.Add(minotaur);
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
                3,
                WeaponByID(2));


        Quest clearSpidersForest =
            new Quest(
                QUEST_ID_COLLECT_SPIDER_SILK,
                "Collect spider silk",
                "Kill spiders in the spider forest",
                MonsterByID(3),
                3);


        Quest slayminotaur =
            new Quest(
                QUEST_ID_SLAY_MINOTAUR,
                "Slay the rampaging minotaur",
                "Kill the minotaur harassing the local population, find out where this creature is burrowed",
                MonsterByID(8),
                1);


        Quests.Add(clearAlchemistGarden);
        Quests.Add(clearFarmersField);
        Quests.Add(clearSpidersForest);
        Quests.Add(slayminotaur);
    }

    public static void PopulateLocations()
    {
        // Create each location
        Location home = new Location(LOCATION_ID_HOME, "Home", "Your house. It seems all your candles have dimmed...\nYou can feel the wind breezing through the broken walls", null, MonsterByID(MONSTER_ID_MINOTAUR));

        Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town square", "You see a fountain adorned with stone eyes.\nThe statue atop seems to resemble a bull standing on its hind legs.\nThere are merchants selling their wares available to interact with.", QuestByID(QUEST_ID_SLAY_MINOTAUR), null);

        Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut", "There are many strange plants on the shelves.\nYou peer into a cualdron... you regret that decision.\nThe alchemist would like to speak to you about a pervasive problem he's having.", QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN), null);

        Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden", "The garden smells of rot and mold.\nIt seems that the various plants have not been cared for. Or so you think, you're no alchemist.", null, MonsterByID(MONSTER_ID_RAT));

        Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse", "The farmhouse rests nicely atop the hill.\nThe dead cows rotting in the cell do not elude you however. The farmer too, seems to be extremely distraught...", QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD), null);

        Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmer's field", "You see rows of vegetables growing here.\nEven more rows however have been gnawed on, some are even burnt down.", null, MonsterByID(MONSTER_ID_SNAKE));

        Location fieldsouth = new Location(LOCATION_ID_FIELD_SOUTH, "Cornfield", "The corn rustles in the wind.\nWho knows what's hiding in there...", null, MonsterByID(MONSTER_ID_FACELESS));

        Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard post", "There is a large, tough-looking guard here. His armor is battered and bruised.\nYet he still stands tall.", null, null);

        Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "You try to ignore the bodies piled up under the bridge...\nThe village elder and his councilfolk approach you about pest control, something to do with arachnids.", QuestByID(QUEST_ID_COLLECT_SPIDER_SILK), null);

        Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest", "You see webs covering covering the grass and trees.\nIt seems several people did not...", null, MonsterByID(MONSTER_ID_GIANT_SPIDER));

        Location arena = new Location(LOCATION_ID_ARENA, "EndArena", "The final boss", null, MonsterByID(MONSTER_ID_ALIEN));

        Location burrow = new Location(LOCATION_ID_BURROW, "Burrow", "The area is strewn with feces.\nThe previous owners lurking nearby.", null, MonsterByID(MONSTER_ID_JEFF));

        Location tomb = new Location(LOCATION_ID_TOMB, "Tomb", "The scent of death seems to sap your life-force from the inside.", null, MonsterByID(MONSTER_ID_SKELL));
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
        alchemistsGarden.LocationToWest = burrow;

        burrow.LocationToEast = alchemistsGarden;

        guardPost.LocationToEast = bridge;
        guardPost.LocationToSouth = tomb;
        guardPost.LocationToWest = townSquare;

        tomb.LocationToNorth = guardPost;

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
        Locations.Add(burrow);
        Locations.Add(tomb);
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
