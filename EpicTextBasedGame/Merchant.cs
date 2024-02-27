public class Merchant{
    public List<Item> RegularWares;
    static int RandomPotionsMade = 0; //to give random potions new ID's
    
    public Merchant(){

    }

    public Item MakeRandomPotion(int cost, EffectTypes? effectType = null){
        int turnsLeft = 1;
        Random rand = new();
        int power = rand.Next(cost-60,cost+60);
        if(effectType==null){
            Array values = Enum.GetValues(typeof(EffectTypes));

            effectType = (EffectTypes)values.GetValue(rand.Next(values.Length));
        }
        if(effectType==EffectTypes.BUFFCRITCHANCE) power /= 100;
        if(effectType != EffectTypes.HEALINSTANT && effectType != EffectTypes.DAMAGEINSTANT){
            turnsLeft = rand.Next(1, 5);
        }
        
        Item mysteryPotion = new Item("?", new Effect($"Mystery Potion{RandomPotionsMade}", power, (EffectTypes)effectType, turnsLeft), "does something!");
        return mysteryPotion;
    }
}