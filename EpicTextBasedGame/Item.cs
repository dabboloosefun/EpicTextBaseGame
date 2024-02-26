public class Item{
    
    public int ID;
    public string Name;
    public Effect? ItemEffect; //nullable since not all items have effects
    public string Description;
    public int Count;
    public static Dictionary<string, Item> BaseItems = new()
    {
        {"Healing Potion10", new Item(1, "Healing Potion10", Effect.BaseEffects["HealInstant10"].Copy(), "heals 10 instantly", 1)},
        {"Healing Potion20", new Item(2, "Healing Potion20", Effect.BaseEffects["HealInstant20"].Copy(), "heals 20 instantly", 1)}, //stacks on "Healing Potion1"
        {"Healing Potion30", new Item(3, "Healing Potion30", Effect.BaseEffects["HealInstant30"].Copy(), "heals 30 instantly", 1)},
        {"Healing Potion50", new Item(4, "Healing Potion50", Effect.BaseEffects["HealInstant50"].Copy(), "heals 50 instantly", 1)},
        {"Healing Potion100", new Item(5, "Healing Potion100", Effect.BaseEffects["HealInstant100"].Copy(), "heals 100 instantly", 1)}, //stacks on "Healing Potion5"
        {"Healing Potion123", new Item(6, "Healing Potion123", new Effect("HealInstant123", 123, EffectTypes.HEALINSTANT, 1), "heals 123 instantly", 1)}
    };

    // player.AddItem(new Item(3, "Healing Potion1", Effect.BaseEffects["HealInstant10"], "heals 10 instantly", 1));
    //     player.AddItem(new Item(3, "Healing Potion2", Effect.BaseEffects["HealInstant10"], "heals 10 instantly", 3)); //stacks on "Healing Potion1"
    //     player.AddItem(new Item(3, "Healing Potion2", Effect.BaseEffects["HealInstant20"], "heals 20 instantly", 2)); //shouldn't work
    //     player.AddItem(new Item(4, "Healing Potion3", Effect.BaseEffects["HealInstant10"], "heals 10 instantly", 4));
    //     player.AddItem(new Item(5, "Healing Potion4", Effect.BaseEffects["HealInstant20"], "heals 20 instantly", 9));
    //     player.AddItem(new Item(5, "Healing Potion5", Effect.BaseEffects["HealInstant20"], "heals 20 instantly", 12)); //stacks on "Healing Potion5"
    //     player.AddItem(new Item(6, "Healing Potion6", Effect.BaseEffects["HealInstant20"], "heals 20 instantly", 25));

    private Item(int id, string name, Effect? itemEffect = null, string description="", int count=1)
    {
        ID = id;
        Name = name;
        ItemEffect = itemEffect;
        Count = count;
        Description = description;
    }

    public Item(string name, Effect? itemEffect = null, string description="", int count=1){
             Name = name;
            ItemEffect = itemEffect; 
            Count = count;
            Description = description;
        if (BaseItems.ContainsKey(name))
        {
            ID = BaseItems[name].ID;
        }
        else
        {
            ID = BaseItems.Count+1;
            //BaseItems.Add(name, new Item(ID, Name, ItemEffect, Description, Count));
            BaseItems.Add(name, this);
        }
       
    }

    public Item Copy(){
        return new Item(Name, ItemEffect, Description, Count);
    }
    public void Increment(int amount=1)
    {
        Count += amount;
    }
    public void Decrement(int amount=1)
    {
        Count -= amount;
    }
    public string Info()
    {
        return $"{Count}x{Name}: {Description}";
    }

    public void UseItem(Character? character = null)
    { //in fight, prompt a target
        Console.WriteLine($"using Item: {Name}");
        if (ItemEffect == null && character == null){
            //if item is e.g. a key to open some door
        }
        else if(ItemEffect == null && character != null){
            //if item is supposed to e.g. be given to an npc
        }
        else if(ItemEffect != null && character != null)
        {
            //if item has an applicable effect
            Console.WriteLine($"Item.cs: Applying {ItemEffect.EffectType} to {character.Name}");
            character.AddEffect(ItemEffect);
        }
        else{
            Console.WriteLine("this item has an applicable effect and likely needs a target. pass a target");
        }
        
       //item removal handled in Player.PromptUseItem() at the moment

    }
}