
public enum ItemTypes //I tried this in a namespace but like that we would have to put everything in the same namespace or 
//prefix every mention of ItemType by "namespacename." e.g. EpicTextBasedGame.ItemType.Potion.
{
    HEALINGPOTION,
    DAMAGEPOTION,
    BUFFCRITCHANCESPELL,
    BUFFMAXDAMAGESPELL,
    BUFFMAXHEALTHSPELL,
    DEBUFFMAXDAMAGESPELL,

}

public class Item{
    
    public int ID;
    public string Name;
    public int Power;
    public int AmountOfTurnsActice;
    public ItemTypes ItemType;
    public string Description;
    public int Count;

    public Item(int id, string name, ItemTypes itemtypeint, int power, int amountOfTurnsActice=0, string description="", int count=1){
        ID = id;
        Name = name;
        Power = power;
        AmountOfTurnsActice = amountOfTurnsActice;
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
        return $"{Count} {Name}: Power{Power}. {Description}";
    }

    public void UseItem(int itemIndex, Player player, Monster? monster = null){ //in fight, prompt a target
        if(ItemType == ItemTypes.HEALINGPOTION)
        {
            if(monster == null) player.RegenarateHealth(Power);
            else monster.RegenarateHealth(Power);
            
        }
        if(ItemType == ItemTypes.DAMAGEPOTION)
        {
            if(monster == null) player.TakeDamage(Power);
            else monster.TakeDamage(Power);
            
        }
        else if(ItemType == ItemTypes.BUFFCRITCHANCESPELL)
        {
            //if(monster == null) player.AddEffect(this, AmountOfTurnsActice);
            //else monster.AddEffect(this, AmountOfTurnsActice);
        }
        
        player.RemoveItem(player.Items[itemIndex]);

    }
}