
// public enum ItemTypes //I tried this in a namespace but like that we would have to put everything in the same namespace or 
// //prefix every mention of ItemType by "namespacename." e.g. EpicTextBasedGame.ItemType.Potion.
// {
//     HEALINGPOTION,
//     DAMAGEPOTION,
//     BUFFCRITCHANCESPELL,
//     BUFFMAXDAMAGESPELL,
//     BUFFMAXHEALTHSPELL,
//     DEBUFFMAXDAMAGESPELL,

// }

public class Item{
    
    public int ID;
    public string Name;
    //public ItemTypes ItemType;
    public Effect ItemEffect;
    public string Description;
    public int Count;
    

    public Item(int id, string name, Effect itemEffect, string description="", int count=1){ //, ItemTypes itemType
        ID = id;
        Name = name;
        //ItemType = itemType;
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
        return $"{Count} {Name}: {Description}";
    }

    public void UseItem(Character character){ //in fight, prompt a target
        Console.WriteLine("using Item: {Name}");
        character.AddEffect(ItemEffect);

        
        //player.RemoveItem(player.Items[itemIndex]);

    }
}