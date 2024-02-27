using System.Runtime.ConstrainedExecution;
using System.Security;

public class Merchant{
    public string Name;
    public List<(Item, int)> Potions = new(); //int = cost
    public List<(Weapon, int)> Weapons = new(); //int = cost
    static int RandomPotionsMade = 0; //to give random potions new ID's
    
    public Merchant(string name){
        Name = name;
        if (name=="Potion merchant")
        {
            Potions.Add((new Item("Damaging Potion10", new Effect("DamageInstant10", 10, EffectTypes.DAMAGEINSTANT, 1), "Deals 10 instant damage"), 8));
            Potions.Add((new Item("Damaging Potion100", new Effect("DamageInstant100", 100, EffectTypes.DAMAGEINSTANT, 1), "Deals 100 instant damage"), 100));
            Potions.Add((new Item("Lingering Healing Potion20_3T", new Effect("HealOverTime20_3T", 20, EffectTypes.HEALOVERTIME, 3), "Heals 20 damage every turn for 3 turns"), 20));
            Potions.Add((new Item("HealingPotion20", new Effect("HealInstant20", 20, EffectTypes.HEALOVERTIME, 1), "Heals 20 instantly"), 20));
        }
        else if(name=="Weaponsmith")
        {
            Weapons.Add((new Weapon(1, "Dagger", 5, 0.4 ), 10));
            Weapons.Add((new Weapon(2, "Sword", 15, 0.1 ), 30));
            Weapons.Add((new Weapon(3, "Gun", 100, 0.0 ), 300));
        }
    }

    public Item MakeRandomPotion(int cost){
        
        int turnsLeft = 1;
        Random rand = new();
        int power = rand.Next(cost-60,cost+60);
        
        Array values = Enum.GetValues(typeof(EffectTypes));
        EffectTypes effectType = (EffectTypes)values.GetValue(rand.Next(values.Length));
        
        if(effectType==EffectTypes.BUFFCRITCHANCE) power /= 100;
        if(effectType != EffectTypes.HEALINSTANT && effectType != EffectTypes.DAMAGEINSTANT){
            turnsLeft = rand.Next(1, 5);
        }
        
        Item mysteryPotion = new Item($"Mystery Potion{RandomPotionsMade}", new Effect($"Mystery Effect{RandomPotionsMade}", power, (EffectTypes)effectType, turnsLeft), "does something!");
        return mysteryPotion;
    }

    public void Visit(Player player){
        switch (Name){
            case "Potion merchant":
                while(true){
                    Console.WriteLine("You decide to visit the potion merchant");
                    Console.WriteLine("He smiles at you as you approach. 'Good day! Is there anything particular you need?");
                    Console.WriteLine("I also sell mystery potions if you're willing to take a risk.");
                    Console.WriteLine("1. Look at regular wares");
                    Console.WriteLine("2. Ask more about mystery potion");
                    Console.WriteLine("3. Leave");
                    int potionInput;
                    bool parseSuccesful;
                    //Console.WriteLine("/n");
                    do{
                        //Helper.ClearLastLine();
                        parseSuccesful = int.TryParse(Console.ReadLine(), out potionInput);
                    } while(!parseSuccesful || !(1<=potionInput && potionInput<=3));
                    if(potionInput == 1){
                        Console.WriteLine($"You have {player.Coins} Coins");
                        Console.WriteLine("The merchant has these items for sale:");
                        for(int i = 0; i<=Potions.Count-1; i++)
                        {
                            Console.WriteLine($"{i+1}. {Potions[i].Item1.Info()} for {Potions[i].Item2} gold coins each");
                        }
                        string userInput;
                        int purchaceChoice;
                        //Console.WriteLine("/n");
                        do{
                            //Helper.ClearLastLine();
                            Console.WriteLine("Enter the number of an item you wish to purchace, or 'back' to leave");
                            userInput = Console.ReadLine().ToLower();
                        parseSuccesful = int.TryParse(userInput, out purchaceChoice);
                        } while(!(userInput=="back" || (parseSuccesful && 1 <= purchaceChoice && purchaceChoice <= Potions.Count))); //there offset of +1 so not Count-1
                        if(userInput=="back"){
                            continue;
                        }
                        else if(player.Coins < Potions[purchaceChoice-1].Item2) 
                        {
                            Console.WriteLine("You don't have enough coins.");
                            continue;
                        }
                        else
                        {
                            player.Coins -= Potions[purchaceChoice-1].Item2;
                            player.AddItem(Potions[purchaceChoice-1].Item1);
                            Potions.Remove(Potions[purchaceChoice-1]);
                            Console.WriteLine("Thank you for your patronage");
                            Console.WriteLine($"You now have {player.Coins} Coins");
                            return;
                        }
                    }
                    if(potionInput == 2){
                        Console.WriteLine("'I'm not sure if they work as they should, which is why they're so cheap!'");
                        Console.WriteLine("'If you're willing to pay more I'm willing to make it more potent'");
                        Console.WriteLine("1. buy mystery potion");
                        Console.WriteLine("2. Never mind");
                        int mysteryPotionInput;
                        do{
                        parseSuccesful = int.TryParse(Console.ReadLine(), out mysteryPotionInput);
                        } while(!parseSuccesful || !(1<=mysteryPotionInput && mysteryPotionInput<=2));
                        if(mysteryPotionInput==1){
                            int amountToPay;
                            //Console.WriteLine("/n");
                            do{
                                //Helper.ClearLastLine();
                                parseSuccesful = int.TryParse(Console.ReadLine(), out amountToPay);
                                if (player.Coins == 0){
                                    Console.WriteLine("You have no coins!");
                                    break;
                                }
                                else{
                                    Console.WriteLine($"You have {player.Coins} Coins");
                                    Console.WriteLine("How much are you willing to pay?");
                                }
                                
                                
                            } while(!parseSuccesful || !(1<=amountToPay && amountToPay<=player.Coins));
                            if(player.Coins > 0)
                            {
                                Item mysteryPotion = MakeRandomPotion(amountToPay);
                                RandomPotionsMade ++;
                                player.AddItem(mysteryPotion);
                                player.Coins -= amountToPay;
                                Console.WriteLine("Here you go!");
                                Console.WriteLine($"You now have {player.Coins} Coins");
                                Console.WriteLine($"{mysteryPotion.Name} has been added to Player's inventory");
                                return;
                            }
                            else continue;
                        }
                        if(mysteryPotionInput==2) continue;
                    }
                    
                    if(potionInput == 3){
                        Console.WriteLine("You leave the potion merchant");
                        return;
                    }
                }
            case "Weaponsmith":
            while(true){
                    Console.WriteLine("You decide to visit the weaponsith");
                    Console.WriteLine("What do you need?");
                    Console.WriteLine("1. Look at weapons");
                    Console.WriteLine("2. Upgrade current weapon");
                    Console.WriteLine("3. Leave");
                    int weaponInput;
                    bool parseSuccesful;
                    do{
                        parseSuccesful = int.TryParse(Console.ReadLine(), out weaponInput);
                    } while(!parseSuccesful || !(1<=weaponInput && weaponInput<=3));
                    if(weaponInput == 1){
                        Console.WriteLine($"You have {player.Coins} Coins");
                        Console.WriteLine("The merchant has these items for sale:");
                        for(int i = 0; i<=Weapons.Count-1; i++)
                        {
                            Weapon currentWeapon = Weapons[i].Item1;
                            Console.WriteLine(Helper.CenterStr($"{i+1}. {currentWeapon.Name}: Max damage: {currentWeapon.MaxDamage}, Crit chance: {currentWeapon.CritChance} for {Weapons[i].Item2}"));
                        }
        
                        string userInput;
                        int purchaceChoice;
                        //Console.WriteLine("/n");
                        do{
                            //Helper.ClearLastLine();
                            Console.WriteLine("Enter the number of an item you wish to purchace, or 'back' to leave");
                            userInput = Console.ReadLine().ToLower();
                        parseSuccesful = int.TryParse(userInput, out purchaceChoice);
                        } while(!(userInput=="back" || (parseSuccesful && 1 <= purchaceChoice && purchaceChoice <= Weapons.Count))); //there offset of +1 so not Count-1
                        if(userInput=="back"){
                            continue;
                        }
                        else if(player.Coins < Weapons[purchaceChoice-1].Item2) 
                        {
                            Console.WriteLine("You don't have enough coins.");
                            continue;
                        }
                        else
                        {
                            player.Coins -= Weapons[purchaceChoice-1].Item2;
                            player.AddWeapon(Weapons[purchaceChoice-1].Item1);
                            Weapons.Remove(Weapons[purchaceChoice-1]);
                            Console.WriteLine("Thank you for your patronage");
                            Console.WriteLine($"You now have {player.Coins} Coins");
                            return;
                        }
                    }
                    if(weaponInput == 2){
                        Console.WriteLine("I'll upgrade your weapon for 10 coins");
                        Console.WriteLine("1. Upgrade current weapon");
                        Console.WriteLine("2. Nevermind");
                        int WeaponUpgradeInput;
                        do{
                        parseSuccesful = int.TryParse(Console.ReadLine(), out WeaponUpgradeInput);
                        } while(!parseSuccesful || !(1<=WeaponUpgradeInput && WeaponUpgradeInput<=2));
                        if(WeaponUpgradeInput==1){
                            
                            if(player.Coins <=0)
                            {
                                Console.WriteLine("You have no coins!");
                                continue;
                            }
                            if(player.Coins > 0)
                            {
                                player.CurrentWeapon.RaiseMaxDamage(2);
                                Console.WriteLine("Here you go!");
                                Console.WriteLine($"You now have {player.Coins} Coins");
                                return;
                            }
                        }
                        if(WeaponUpgradeInput==2) continue;
                    }
                    if(weaponInput == 3){
                        Console.WriteLine("You leave the weaponsmith");
                        return;
                    }
                }
        }
       
    }
}