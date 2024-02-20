class Program
{
    public static void Main()
    {
        // Our main code code here, preferably everything is dynamic (so lots of methods).
        Effect critChaceBuffEffect3T = new Effect(1, 0.2, EffectTypes.BUFFCRITCHANCE, 3);
        Item CritPotion3T = new Item(1, "CritPotion", critChaceBuffEffect3T, "increases crit chance by 0.2. lasts 3 turns");
        Player player = new Player();
        player.AddItem(CritPotion3T);
        bool victory = false;
        SuperAdventure.TitleScreen();

        // Little intro can go here.

        while (!victory)
        {
            //enable to force a fight at the start
            //SuperAdventure.Fight(player, new Monster(1, "Goblin", 1, 80, 100));
            
            // Main Gameplay loop hierin > bij elke stap checken of er op escape wordt gedrukt dan of menu openen of game afsluiten.
            double succesfulEncounter = 0.2;
            Random encounterChance = new Random();  // Dit kan allemaal afgestaan worden aan de monster class, dan kunnen we elk monster een persoonlijk encounter chance geven.
            double encounterRoll = encounterChance.NextDouble();
            if (player.CurrentLocation.MonsterLivingHere != null && encounterRoll <= succesfulEncounter) SuperAdventure.Fight(player, player.CurrentLocation.MonsterLivingHere);

            int playerAction = player.AskPlayerAction();
            player.CommenceAction(playerAction);
        }

        SuperAdventure.OutroScreen();
    }
}
