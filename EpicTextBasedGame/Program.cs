﻿class Program
{
    
    public static void Main()
    {
        Player player = new Player();
        foreach (Monster monster in World.Monsters)
        {
            monster.CurrentHealth = monster.MaxHealth;
        }
        
        //player.AddItem(new Item("Healing Potion100", Effect.BaseEffects["HealInstant100"], "heals 100 instantly", 1)); //stacks fine
        //since Item and Effect ID's  are now generated by their own classes we could use the below method and things should still stack fine
        player.AddItem(new Item("Healing Potion25", new Effect("HealInstant25", 25, EffectTypes.HEALINSTANT, 1), "heals 25 instantly", 1));
        player.AddItem(Item.BaseItems["Healing Potion25"].Copy()); 
        player.AddItem(new Item("Lingering Heaing Potion25", new Effect("HealOverTime25", 25, EffectTypes.HEALOVERTIME, 3), "heals 25 health every turn for 3 turns", 2));
        player.Coins = 60;
        player.Weapons.Add(World.WeaponByID(World.WEAPON_ID_HAND_OF_GOD));

        //player.AddItem(new Item("Healing Potion", BaseEffects.HealInstant10, "heals 10 instantly", 1));
        Helper.TitleScreen();
        player.CurrentLocation.Map();
        while (true)
        {
            //enable to force a fight at the start
            //SuperAdventure.Fight(player, new Monster(1, "Goblin", 1, 80, 100));
            player.AskPlayerAction(player);
        }
    }
}
