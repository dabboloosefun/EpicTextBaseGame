public class Item{
    
    public int ID;
    public string Name;
    public Effect? ItemEffect; //nullable since not all items have effects
    public string Description;
    public int Count;
    

    public Item(int id, string name, Effect? itemEffect = null, string description="", int count=1){
        ID = id;
        Name = name;
        ItemEffect = itemEffect;
        Count = count;
        Description = description;
    }
    public void Increment(){
        Count +=1;
    }
    public void Decrement(){
        Count -= 1;
    }
    public string Info(){
        return $"{Count}x{Name}: {Description}";
    }

    public void UseItem(Character? character = null){ //in fight, prompt a target
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