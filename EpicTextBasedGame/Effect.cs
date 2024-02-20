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
    public int ID; //no use for ID yet
    public double Power;
    public EffectTypes EffectType;
    public int TurnsLeft;
    public bool EffectIsApplied;
    public Character? AffectedCharacter;
    public Effect(int id, double power, EffectTypes effectType, int turnsleft){
        ID = id;
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
                        Console.WriteLine($"Your weapons crit chance is now {weapon.CritChance}");
                    }
                    else Console.WriteLine("You don't have a weapon equipped!");
                }
                Console.WriteLine("Monsters can't critical hit! Also why would you want them to?");
                
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
            if (EffectType == EffectTypes.HEALOVERTIME && EffectIsApplied)
            {
                AffectedCharacter.RegenarateHealth((int)Power);
            }
            else if (EffectType == EffectTypes.DAMAGEOVERTIME && EffectIsApplied)
            {
                AffectedCharacter.TakeDamage((int)Power);
            }

            if(EffectIsApplied)
            {
                TurnsLeft -= 1;
                if(TurnsLeft <=0)
                {
                    EffectIsApplied = false;
                }
            }
            // else if (!EffectIsApplied) 
            // {
            //     Console.WriteLine($"Attempting removing effect {EffectType} from {AffectedCharacter}");
            //     RemoveEffect();
            // }

            //3 turns buffcritchance
            //apply current turn, TurnsLeft 3->2
            //apply 1 more turn, TurnsLeft 2->1
            //apply 1 more turn, TurnsLeft 1->0, EffectIsApplied = false
            //Start of next turn, call RemoveEffect()

            //3 turns healovertime
            //apply current turn, TurnsLeft 3->2
            //apply 1 more turn, TurnsLeft 2->1
            //apply 1 more turn, TurnsLeft 1->0, EffectIsApplied = false
            //don't apply next turn bc EffectApplied = false. Then call RemoveEffect()
        }
    }

    public void RemoveEffect(){
        if (AffectedCharacter == null) Console.WriteLine("Error in RemoveEffect(): effect wasn't linked properly to a character");
        else
        {
            if (EffectType == EffectTypes.BUFFCRITCHANCE)
                {
                    Weapon? weapon = ((Player)AffectedCharacter).CurrentWeapon;
                    if (weapon != null)
                    {
                        weapon.CritChance -= Power;
                        Console.WriteLine($"succesfully removed {this.EffectType} from {this.AffectedCharacter}");
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

        }
    }
}