using Microsoft.VisualBasic;

public enum EffectTypes{
HEALOVERTIME,
DAMAGEOVERTIME,
BUFFCRITCHANCE,
BUFFMAXDAMAGE,
DEBUFFMAXDAMAGE,
BUFFMAXHEALTH,
DEBUFFMAXHEALTH
}

public class Effect{
    //public int ID; //no use for ID yet
    //I don't want to bother right now to implement turn-extension of effects, so for now they stack additively and seperately
    //we will likely need ID if we want to be able to extend the amount of turns an effect lasts
    public double Power;
    public EffectTypes EffectType;
    public int TurnsLeft;
    public bool EffectIsApplied;
    public Character? AffectedCharacter;
    public Effect(double power, EffectTypes effectType, int turnsleft){ //int id, 
        //ID = id;
        Power = power;
        EffectType = effectType; 
        TurnsLeft = turnsleft;
    }

    public void ApplyEffect(){
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

            UpdateEffect(); //update once at application so it applies immediately. then update again at the start of each turn
            
        }
    }
   
    public void UpdateEffect(){
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

    public void RemoveEffect(){
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
}