class Program
{
    
    public static void Main()
    {
      
        //Effect critChaceBuffEffect3T = new Effect("BuffCritChance0.2_3T", 0.2, EffectTypes.BUFFCRITCHANCE, 3);
        //Item CritPotion3T = new Item(1, "CritPotion", critChaceBuffEffect3T, "increases crit chance by 0.2. lasts 3 turns");
        //Effect HealOverTimeEffect3T = new Effect("HealOverTime2_3T", 2, EffectTypes.HEALOVERTIME, 3);
        //Item HealOverTimePotion3T = new Item(2, "lingering healing potion", HealOverTimeEffect3T, "Heals user 2 health points every turn");
        Player player = new Player();
        //player.AddItem(CritPotion3T);
        //player.AddItem(HealOverTimePotion3T);
        player.AddItem(Item.BaseItems["Healing Potion10"].Copy());
        player.AddItem(Item.BaseItems["Healing Potion10"].Copy());
        player.AddItem(Item.BaseItems["Healing Potion20"].Copy());
        player.AddItem(Item.BaseItems["Healing Potion123"].Copy());
        player.AddItem(Item.BaseItems["Healing Potion123"].Copy());
        player.AddItem(Item.BaseItems["Healing Potion123"].Copy());
        //player.AddItem(Item.BaseItems["Healing Potion100"].Copy());
        player.AddItem(new Item("Healing Potion100", Effect.BaseEffects["HealInstant100"], "heals 100 instantly", 1)); //stacks fine
        player.AddItem(new Item("Healing Potion100", new Effect("HealInstant100", 100, EffectTypes.HEALINSTANT, 1), "heals 100 instantly", 1)); //stacks fine
        player.AddItem(Item.BaseItems["Healing Potion100"].Copy());

        //dynamically created effects can now be compared with eachother for stacking because Effect class handles ID generation, 
        //but dynamically created items can't be compared yet because Item class doesn't handle ID generation. I will add this

        //player.AddItem(new Item(3, "Healing Potion", BaseEffects.HealInstant10, "heals 10 instantly", 1));
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
