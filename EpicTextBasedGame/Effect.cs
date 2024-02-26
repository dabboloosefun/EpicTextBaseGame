using Microsoft.VisualBasic;


public enum EffectTypes{
HEALINSTANT,
DAMAGEINSTANT,
HEALOVERTIME,
DAMAGEOVERTIME,
BUFFCRITCHANCE,
BUFFMAXDAMAGE,
DEBUFFMAXDAMAGE,
BUFFMAXHEALTH,
DEBUFFMAXHEALTH
}

//potions with the same name but different power/length shouldn't stack. 
//Here we can define common effects with an ID and use the ID to make them stack.
// public static class BaseEffects{
//     public static Effect Heal10Instant = new Effect(110, 10, EffectTypes.HEALINSTANT, 1);
//     public static Effect Heal20Instant = new Effect(120, 20, EffectTypes.HEALINSTANT, 1);
//     public static Effect Heal30Instant = new Effect(130, 30, EffectTypes.HEALINSTANT, 1);
//     public static Effect Heal50Instant = new Effect(150, 50, EffectTypes.HEALINSTANT, 1);
//     public static Effect Heal100Instant = new Effect(200, 100, EffectTypes.HEALINSTANT, 1);
//     public static Effect Damage10Instant = new Effect(210, 10, EffectTypes.DAMAGEINSTANT, 1);

//     public static Effect Heal5OverTime3T = new Effect(1053, 5, EffectTypes.HEALOVERTIME, 3);
//     public static Effect Heal20OverTime2T = new Effect(1202, 20, EffectTypes.HEALOVERTIME, 2);

// }



public class Effect{
    public int ID; 
    public string Name;
    public double Power;
    public EffectTypes EffectType;
    public int TurnsLeft;
    public bool EffectIsApplied;
    public Character? AffectedCharacter;
    public static Dictionary<int, Effect> CustomEffects = new(); // can be called without Wffect object
    public static Dictionary<string, Effect> BaseEffects = new()
    { // can be called without Wffect object
        {"HealInstant10", new Effect(1, 10, EffectTypes.HEALINSTANT, 1)},
        {"HealInstant20", new Effect(2, 20, EffectTypes.HEALINSTANT, 1)},
        {"HealInstant30", new Effect(3, 30, EffectTypes.HEALINSTANT, 1)},
        {"HealInstant50", new Effect(4, 50, EffectTypes.HEALINSTANT, 1)},
        {"HealInstant100", new Effect(5, 100, EffectTypes.HEALINSTANT, 1)},
        {"DamageInstant10", new Effect(6, 10, EffectTypes.DAMAGEINSTANT, 1)},
        {"HealOverTime50_3T", new Effect(7, 5, EffectTypes.HEALOVERTIME, 3)},
        {"HealOverTime20_2T", new Effect(8, 20, EffectTypes.HEALOVERTIME, 2)}
    };
    private Effect(int id, double power, EffectTypes effectType, int turnsLeft)
    { //used only for manual initialization in above dictionary
        ID = id;
        Power = power;
        EffectType = effectType; 
        TurnsLeft = turnsLeft;
    }

    //If we want to use an effect it's best to add it manually to above dictionary and use that effect directly in an Item constructor
    //(so without new Effect()). The below constructor should probably only ever be used if we want to dynamically add new effects 
    //with e.g. incrementing strength based on enemy level as a reward like so:
    //string rewardStrength = monster.Power * 5;
    //ReaperPotion = new Item(6(id), "ReaperPotion", new Effect("DamageInstant"+ Convert.ToString(rewardStrength), rewardStrength, EffectTypes.DAMAGEINSTANT, 1) , $"Deals {rewardStrength} Damage");
    //the point of this all is so that dynamically created effects will always have the same ID if they have the same name and can thus technically stack.
    public Effect(string name, double power, EffectTypes effectType, int turnsLeft)
    { 
        Name = name;
        Power = power;
        EffectType = effectType; 
        TurnsLeft = turnsLeft;
        if (BaseEffects.ContainsKey(name)){
            ID = BaseEffects[name].ID;
        }
        else
        {
            ID = BaseEffects.Count+1;
            BaseEffects.Add(name, this);
        }
    }

    public void ApplyEffect()
    {
        if (AffectedCharacter == null) Console.WriteLine("Error in ApplyEffect(): effect wasn't linked properly to a character");
        else
        {
            Console.WriteLine($"AffectedCharacter: {AffectedCharacter.Name}");
            EffectIsApplied = true;
            //UpdateEffect() handles HEALOVERTIME and DAMAGEOVERTIME
            if (EffectType == EffectTypes.BUFFCRITCHANCE)
            {
                if(AffectedCharacter.GetType() == typeof(Player))
                {
                    Weapon? weapon = ((Player)AffectedCharacter).CurrentWeapon;
                    if (weapon != null) 
                    {
                        weapon.CritChance += Power;
                        Console.WriteLine($"At ApplyEffect(): Your weapons crit chance is now {weapon.CritChance}");
                    }
                    else Console.WriteLine("You don't have a weapon equipped!");
                }
                else Console.WriteLine("Monsters can't critical hit! Also why would you want them to?");
                
            }
            else if(EffectType == EffectTypes.BUFFMAXDAMAGE)
            {
                if(AffectedCharacter.GetType() == typeof(Player))
                {
                    Weapon? weapon = ((Player)AffectedCharacter).CurrentWeapon;
                    if(weapon != null) weapon.RaiseMaxDamage((int)Power);
                    else Console.WriteLine("You don't have a weapon equipped!");
                }
                else 
                {
                    ((Monster)AffectedCharacter).RaiseMaxDamage((int)Power);
                }
            }
            else if(EffectType == EffectTypes.DEBUFFMAXDAMAGE)
            {
                if(AffectedCharacter.GetType() == typeof(Player))
                {
                    Weapon? weapon = ((Player)AffectedCharacter).CurrentWeapon;
                    if (weapon != null) weapon.LowerMaxDamage((int)Power);
                    else Console.WriteLine("You don't have a weapon equipped!");
                }
                else
                {
                    ((Monster)AffectedCharacter).LowerMaxDamage((int)Power);
                }
            }
            else if(EffectType == EffectTypes.BUFFMAXHEALTH)
            {
                AffectedCharacter.RaiseMaxHealth((int) Power);
            }
            else if(EffectType == EffectTypes.DEBUFFMAXHEALTH)
            {
                AffectedCharacter.LowerMaxHealth((int) Power);
            }
            else if(EffectType == EffectTypes.HEALINSTANT)
            {
                AffectedCharacter.RegenarateHealth((int)Power);
            }
            else if(EffectType == EffectTypes.DAMAGEINSTANT)
            {
                AffectedCharacter.TakeDamage((int)Power);
            }

            UpdateEffect(); //update once at application so it applies immediately. then update again at the start of each turn
            
        }
    }
   
    public void UpdateEffect()
    {
        if (AffectedCharacter == null) Console.WriteLine("Error in UpdateEffect(): effect wasn't linked properly to a character");
        else
        {
            //currently if effects last 3 turns they are removed at the start of the 4th rather than the end of the third. 
            //this is fine for most spells but for over-time effects it might be better to notify user at last application so they don't expect another heal 
            //don't know how to do this yet!
            if(EffectIsApplied)
            {
                if(TurnsLeft <=0)
                {
                    EffectIsApplied = false;
                    RemoveEffect();
                }
                TurnsLeft -= 1;
            }

            if (EffectType == EffectTypes.HEALOVERTIME && EffectIsApplied)
            {
                AffectedCharacter.RegenarateHealth((int)Power);
            }
            else if (EffectType == EffectTypes.DAMAGEOVERTIME && EffectIsApplied)
            {
                AffectedCharacter.TakeDamage((int)Power);
            }

            //3 turns buffcritchance
            //apply current turn, TurnsLeft 3->2
            //apply 1 more turn, TurnsLeft 2->1
            //apply 1 more turn, TurnsLeft 1->0
            //Start of next turn, EffectIsApplied = false, call RemoveEffect()

            //3 turns healovertime
            //apply current turn, TurnsLeft 3->2
            //apply 1 more turn, TurnsLeft 2->1
            //apply 1 more turn, TurnsLeft 1->0
            //Start of next turn, EffectIsApplied = false, call RemoveEffect(), over-time effects aren't called
            
        }
    }

    public void RemoveEffect()
    {
        if (AffectedCharacter == null) {
            Console.WriteLine("Error in RemoveEffect(): effect wasn't linked properly to a character");
        }
        else
        {
            if (EffectType == EffectTypes.BUFFCRITCHANCE)
                {
                    Weapon? weapon = ((Player)AffectedCharacter).CurrentWeapon;
                    if (weapon != null)
                    {
                        weapon.CritChance -= Power;
                    }
                }
            else if(EffectType == EffectTypes.BUFFMAXDAMAGE)
            {
                Weapon? weapon = ((Player)AffectedCharacter).CurrentWeapon;
                if (weapon != null)
                {
                    weapon.LowerMaxDamage((int)Power);
                }
                else //if it doesnt have a weapon its a Monster
                {
                    ((Monster)AffectedCharacter).LowerMaxDamage((int)Power);
                }
            }
            else if(EffectType == EffectTypes.DEBUFFMAXDAMAGE)
            {
                Weapon? weapon = ((Player)AffectedCharacter).CurrentWeapon;
                if (weapon != null)
                {
                    weapon.RaiseMaxDamage((int)Power);
                }
                else //if it doesnt have a weapon its a Monster
                {
                    ((Monster)AffectedCharacter).RaiseMaxDamage((int)Power);
                }
            }
            else if(EffectType == EffectTypes.BUFFMAXHEALTH)
            {
                AffectedCharacter.LowerMaxHealth((int)Power);
            }
            else if(EffectType == EffectTypes.DEBUFFMAXHEALTH)
            {
                AffectedCharacter.RaiseMaxHealth((int)Power);
            }

            AffectedCharacter.ActiveEffects.Remove(this);
            if (EffectType != EffectTypes.HEALOVERTIME && EffectType != EffectTypes.DAMAGEOVERTIME){
                Console.WriteLine($"{AffectedCharacter} ran out of {EffectType}({Power})");
            }
        }
    }

    public Effect Copy(){
        return new Effect(ID, Power, EffectType, TurnsLeft);
    }
}