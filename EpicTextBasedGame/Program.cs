class Program
{
    
    public static void Main()
    {
        
        // Our main code code here, preferably everything is dynamic (so lots of methods).
        Effect critChaceBuffEffect3T = new Effect("BuffCritChance0.2_3T", 0.2, EffectTypes.BUFFCRITCHANCE, 3);
        Item CritPotion3T = new Item(1, "CritPotion", critChaceBuffEffect3T, "increases crit chance by 0.2. lasts 3 turns");
        Effect HealOverTimeEffect3T = new Effect("HealOverTime2_3T", 2, EffectTypes.HEALOVERTIME, 3);
        Item HealOverTimePotion3T = new Item(2, "lingering healing potion", HealOverTimeEffect3T, "Heals user 2 health points every turn");
        Player player = new Player();
        player.AddItem(CritPotion3T);
        player.AddItem(HealOverTimePotion3T);
        player.AddItem(new Item(3, "Healing Potion1", Effect.BaseEffects["HealInstant10"], "heals 10 instantly", 1));
        player.AddItem(new Item(3, "Healing Potion2", Effect.BaseEffects["HealInstant10"], "heals 10 instantly", 3));
        player.AddItem(new Item(3, "Healing Potion2", Effect.BaseEffects["HealInstant20"], "heals 20 instantly", 2)); //shouldn't work
        player.AddItem(new Item(4, "Healing Potion3", Effect.BaseEffects["HealInstant10"], "heals 10 instantly", 4));
        player.AddItem(new Item(5, "Healing Potion4", Effect.BaseEffects["HealInstant20"], "heals 20 instantly", 9));
        player.AddItem(new Item(5, "Healing Potion5", Effect.BaseEffects["HealInstant20"], "heals 20 instantly", 12));
        player.AddItem(new Item(6, "Healing Potion6", Effect.BaseEffects["HealInstant20"], "heals 20 instantly", 25));

        //player.AddItem(new Item(3, "Healing Potion", BaseEffects.HealInstant10, "heals 10 instantly", 1));
        bool victory = false;
        Helper.TitleScreen();

        // Little intro can go here.

        while (!victory)
        {
            //enable to force a fight at the start
            //SuperAdventure.Fight(player, new Monster(1, "Goblin", 1, 80, 100));
            
            // Main Gameplay loop hierin > bij elke stap checken of er op escape wordt gedrukt dan of menu openen of game afsluiten.
            double succesfulEncounter = 1;
            Random encounterChance = new Random();  // Dit kan allemaal afgestaan worden aan de monster class, dan kunnen we elk monster een persoonlijk encounter chance geven.
            double encounterRoll = encounterChance.NextDouble();
            if (player.CurrentLocation.MonsterLivingHere != null && encounterRoll <= succesfulEncounter) SuperAdventure.Fight(player, player.CurrentLocation.MonsterLivingHere);
            if (player.CurrentLocation.QuestAvailableHere != null) player.CurrentLocation.StartLocationQuest(player);
            if (player.CurrentLocation.Name == "Guard post") player.CurrentLocation.StartEndGame(player);
            player.AskPlayerAction();
        }

        Helper.OutroScreen();
    }
}
