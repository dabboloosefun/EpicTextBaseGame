﻿class Program
{
    public static void Main()
    {
        // Our main code code here, preferably everything is dynamic (so lots of methods).
        Player player = new Player();
        bool victory = false;
        SuperAdventure.TitleScreen();

        // Little intro can go here.

        while (!victory)
        {
            // Main Gameplay loop hierin > bij elke stap checken of er op escape wordt gedrukt dan of menu openen of game afsluiten.
            double succesfulEncounter = 0.2;
            Random encounterChance = new Random();  // Dit kan allemaal afgestaan worden aan de monster class, dan kunnen we elk monster een persoonlijk encounter chance geven.
            double encounterRoll = encounterChance.NextDouble();
            // if (player.currentLocation.Monster != null && encounterRoll <= succesfulEncounter) SuperAdventure.Fight(player.currentLocation.Monster);

            int playerAction = player.AskPlayerAction();
            player.CommenceAction(playerAction);
        }

        // SuperAdventure.VictoryScreen();
    }
}
