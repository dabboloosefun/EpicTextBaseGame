using System.Security.Cryptography;

public abstract class Character{
    public int CurrentHealth;
    public int MaxHealth;
    public int Experience;
    public int Level;
    public string Name;
    public List<Effect> ActiveEffects;

    //abstract classes are only to be inherited from and not to be initialized. 
    //It's possible to have Monster/Player delegate initialization of common fields to this constructor but
    //then you'd have to look at both constructors to see what's being initialized so let's not
    // public Character(){
    //     ActiveEffects = new List<Effect>();
    // }
    public void AddEffect(Effect effect){
        //Console.WriteLine($"Character.AddEffect: Adding effect {effect.EffectType} to {Name}");
        effect.AffectedCharacter = this;
        ActiveEffects.Add(effect);
        //Console.WriteLine($"Character.AddEffect: effect {effect.EffectType} is linked to {effect.AffectedCharacter}");
    }
    public void UpdateEffects() 
    {
        if(ActiveEffects.Count > 0){
            //Console.WriteLine($"updating {Name}'s effects:");
            for (int i = ActiveEffects.Count-1; i >= 0; i--) //reverse order to handle changing length
            {
                //Console.WriteLine($"Effect {i}: {ActiveEffects[i].EffectType} is applied: {ActiveEffects[i].EffectIsApplied}, turns left: {ActiveEffects[i].TurnsLeft}");
                if(ActiveEffects[i].EffectIsApplied)
                {
                    //Console.WriteLine($"At UpdateEffects(): effect {ActiveEffects[i].EffectType} is linked to {ActiveEffects[i].AffectedCharacter}");
                    ActiveEffects[i].UpdateEffect();
                }
                // else if (!ActiveEffects[i].EffectIsApplied && ActiveEffects[i].TurnsLeft <= 0){
                //     //Console.WriteLine($"Attempting removing effect {ActiveEffects[i].EffectType} from {ActiveEffects[i].AffectedCharacter}");
                //     ActiveEffects[i].RemoveEffect();
                // } //now handled internally by the effect
                else if (!ActiveEffects[i].EffectIsApplied && ActiveEffects[i].TurnsLeft > 0) ActiveEffects[i].ApplyEffect();
                
            }
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0; // HP wont go into negative when printing
            if (Name == "Player") Helper.Deathscreen(); 
        }
        else
        {
            Console.WriteLine($"{Name} took {damage} damage. Current health: {CurrentHealth}");
        }
    }

    public void RegenarateHealth(int health)
    {
        CurrentHealth += health;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        Console.WriteLine($"{Name} Restored {health} health. Current health: {CurrentHealth}");
    }

    
    // Te gebruiken voor als Monster MaxHealth wordt gebuffed in gevecht-scenario
    public void RaiseMaxHealth(int raisedMaxHealth)
    {
        this.MaxHealth += raisedMaxHealth;
        Console.WriteLine($"{this.Name}'s maximum health has been raised to {this.MaxHealth}!");
    }


    // Te gebruiken voor als Monster MaxHealth wordt gedebuffed in gevecht-scenario.
    public void LowerMaxHealth(int loweredMaxHealth)
    {
        this.MaxHealth -= loweredMaxHealth;
        if (this.MaxHealth < 1)
        {
            this.MaxHealth = 1;
            Console.WriteLine($"{this.Name} is already at 1 max health!");
        }
        if (this.CurrentHealth > this.MaxHealth) this.CurrentHealth = this.MaxHealth;
        Console.WriteLine($"{this.Name}'s maximum health has been lowered to {this.MaxHealth}!");
    }
}