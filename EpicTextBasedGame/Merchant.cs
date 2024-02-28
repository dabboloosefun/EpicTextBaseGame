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
            Potions.Add((new Item("Damaging Potion20", new Effect("DamageInstant20", 20, EffectTypes.DAMAGEINSTANT, 1), "Deals 20 instant damage"), 18));
            Potions.Add((new Item("Damaging Potion100", new Effect("DamageInstant100", 100, EffectTypes.DAMAGEINSTANT, 1), "Deals 100 instant damage"), 100));
            Potions.Add((new Item("Lingering Healing Potion20_3T", new Effect("HealOverTime20_3T", 20, EffectTypes.HEALOVERTIME, 3), "Heals 20 damage every turn for 3 turns"), 20));
            Potions.Add((new Item("HealingPotion20", new Effect("HealInstant20", 20, EffectTypes.HEALOVERTIME, 1), "Heals 20 instantly"), 20));
            Potions.Add((new Item("Lingering Damage Potion30_3T", new Effect("DamageOverTime20_3T", 30, EffectTypes.DAMAGEOVERTIME, 3), "Deals 30 damage every turn for 3 turns"), 100));
        }
        else if(name=="Weaponsmith")
        {
            Weapons.Add((new Weapon(3, "Dagger", 5, 0.4 ), 10));
            Weapons.Add((new Weapon(4, "Sword", 15, 0.1 ), 30));
            Weapons.Add((new Weapon(5, "Gun", 100, 0.0 ), 300));
            Weapons.Add((new Weapon(6, "Stick", 1, 1.0 ), 1));
            Weapons.Add((new Weapon(7, "Pan", 20, 0.5), 100));
            Weapons.Add((new Weapon(8, "Battle axe", 40, 0.2), 140));
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

    public static void Market(Player player)
    {
        Helper.WriteInCenter(@"You decide to visit the market, where you notice a potion merchant and a weaponsmith.
1. Visit potion merchant
2. Visit weaponsmith
3. Nevermind
What would you like to do? 1-3");
        int userInput;
        bool parseSuccesful;
        do
        {
            parseSuccesful = int.TryParse(Console.ReadLine(), out userInput);
            Helper.ClearLineDo();
        } while (!parseSuccesful || !(1 <= userInput && userInput <= 3));
        bool shopping = true;
        while (shopping)
        {
            if (userInput == 1)
            {
                Console.Clear();
                Merchant merchant = new("Potion merchant");
                merchant.Visit(player);
                player.CurrentLocation.Map(); // overrides any feedback about purchase now.
                break;
            }
            if (userInput == 2)
            {
                Console.Clear();
                Merchant merchant = new("Weaponsmith");
                merchant.Visit(player);
                player.CurrentLocation.Map();
                break;
            }
            if (userInput == 3)
            {
                Console.Clear();
                break;
            }
        }
    }

    public void Visit(Player player){
        switch (Name){
            case "Potion merchant":
                while(true){
                    Console.WriteLine(Helper.CenterStr("You decide to visit the potion merchant"));
                    Console.WriteLine(Helper.CenterStr("He smiles at you as you approach. 'Good day! Is there anything particular you need?"));
                    Console.WriteLine(Helper.CenterStr("I also sell mystery potions if you're willing to take a risk."));
                    Console.WriteLine(Helper.CenterStr("1. Look at regular wares"));
                    Console.WriteLine(Helper.CenterStr("2. Ask more about mystery potion"));
                    Console.WriteLine(Helper.CenterStr("3. Leave"));
                    int potionInput;
                    bool parseSuccesful;
                    //Console.WriteLine("/n");
                    do{
                        //Helper.ClearLastLine();
                        //Helper.ClearLineDo();
                        parseSuccesful = int.TryParse(Console.ReadLine(), out potionInput);
                        Helper.ClearLineDo();
                    } while(!parseSuccesful || !(1<=potionInput && potionInput<=3));
                    Console.Clear();
                    if(potionInput == 1){
                        Console.WriteLine(Helper.CenterStr($"You have {player.Coins} Coins"));
                        Console.WriteLine(Helper.CenterStr("The merchant has these items for sale:"));
                        for(int i = 0; i<=Potions.Count-1; i++)
                        {
                            Console.WriteLine(Helper.CenterStr($"{i+1}. {Potions[i].Item1.Info()} for {Potions[i].Item2} gold coins each"));
                        }
                        string userInput;
                        int purchaceChoice;
                        //Console.WriteLine("/n");
                        do{
                            //Helper.ClearLastLine();
                            Console.WriteLine(Helper.CenterStr("Enter the number of an item you wish to purchace, or 'back' to leave"));
                            userInput = Console.ReadLine().ToLower();
                            parseSuccesful = int.TryParse(userInput, out purchaceChoice);
                            Helper.ClearLineDo();
                        } while(!(userInput=="back" || (parseSuccesful && 1 <= purchaceChoice && purchaceChoice <= Potions.Count))); //there offset of +1 so not Count-1
                        Console.Clear();
                        if (userInput=="back"){
                            continue;
                        }
                        else if(player.Coins < Potions[purchaceChoice-1].Item2) 
                        {
                            Console.WriteLine(Helper.CenterStr("You don't have enough coins."));
                            continue;
                        }
                        else
                        {
                            player.Coins -= Potions[purchaceChoice-1].Item2;
                            player.AddItem(Potions[purchaceChoice-1].Item1);
                            Potions.Remove(Potions[purchaceChoice-1]);
                            Console.WriteLine(Helper.CenterStr("Thank you for your patronage"));
                            Console.WriteLine(Helper.CenterStr($"You now have {player.Coins} Coins"));
                            Thread.Sleep(1500);
                            return;
                        }
                    }
                    if(potionInput == 2){
                        Console.WriteLine(Helper.CenterStr("'I'm not sure if they work as they should, which is why they're so cheap!'"));
                        Console.WriteLine(Helper.CenterStr("'If you're willing to pay more I'm willing to make it more potent'"));
                        Console.WriteLine(Helper.CenterStr("1. buy mystery potion"));
                        Console.WriteLine(Helper.CenterStr("2. Never mind"));
                        int mysteryPotionInput;
                        do{
                        parseSuccesful = int.TryParse(Console.ReadLine(), out mysteryPotionInput);
                        Helper.ClearLineDo();
                        } while(!parseSuccesful || !(1<=mysteryPotionInput && mysteryPotionInput<=2));
                        Console.Clear();
                        if (mysteryPotionInput==1){
                            int amountToPay;
                            //Console.WriteLine("/n");
                            do{
                                //Helper.ClearLastLine();
                                parseSuccesful = int.TryParse(Console.ReadLine(), out amountToPay);
                                if (player.Coins == 0){
                                    Console.WriteLine(Helper.CenterStr("You have no coins!"));
                                    break;
                                }
                                else{
                                    Console.WriteLine(Helper.CenterStr($"You have {player.Coins} Coins"));
                                    Console.WriteLine(Helper.CenterStr("How much are you willing to pay?"));
                                }
                                Helper.ClearLineDo();
                                
                            } while(!parseSuccesful || !(1<=amountToPay && amountToPay<=player.Coins));
                            Console.Clear();
                            if (player.Coins > 0)
                            {
                                Item mysteryPotion = MakeRandomPotion(amountToPay);
                                RandomPotionsMade ++;
                                player.AddItem(mysteryPotion);
                                player.Coins -= amountToPay;
                                Console.WriteLine(Helper.CenterStr("Here you go!"));
                                Console.WriteLine(Helper.CenterStr($"You now have {player.Coins} Coins"));
                                Console.WriteLine(Helper.CenterStr($"{mysteryPotion.Name} has been added to Player's inventory"));
                                Thread.Sleep(1500);
                                return;
                            }
                            else continue;
                        }
                        if(mysteryPotionInput==2) continue;
                    }
                    
                    if(potionInput == 3){
                        Console.WriteLine(Helper.CenterStr("You leave the potion merchant"));
                        Thread.Sleep(1500);
                        return;
                    }
                }
            case "Weaponsmith":
            while(true){
                    Console.WriteLine(Helper.CenterStr("You decide to visit the weaponsith"));
                    Console.WriteLine(Helper.CenterStr("What do you need?"));
                    Console.WriteLine(Helper.CenterStr("1. Look at weapons"));
                    Console.WriteLine(Helper.CenterStr("2. Upgrade current weapon"));
                    Console.WriteLine(Helper.CenterStr("3. Leave"));
                    int weaponInput;
                    bool parseSuccesful;
                    do{
                        parseSuccesful = int.TryParse(Console.ReadLine(), out weaponInput);
                        Helper.ClearLineDo();
                    } while(!parseSuccesful || !(1<=weaponInput && weaponInput<=3));
                    Console.Clear();
                    if (weaponInput == 1){
                        Console.WriteLine(Helper.CenterStr($"You have {player.Coins} Coins"));
                        Console.WriteLine(Helper.CenterStr("The merchant has these items for sale:"));
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
                            Console.WriteLine(Helper.CenterStr("Enter the number of an item you wish to purchace, or 'back' to leave"));
                            userInput = Console.ReadLine().ToLower();
                            parseSuccesful = int.TryParse(userInput, out purchaceChoice);
                            Helper.ClearLineDo();
                        } while(!(userInput=="back" || (parseSuccesful && 1 <= purchaceChoice && purchaceChoice <= Weapons.Count))); //there offset of +1 so not Count-1
                        Console.Clear();
                        if (userInput=="back"){
                            continue;
                        }
                        else if(player.Coins < Weapons[purchaceChoice-1].Item2) 
                        {
                            Console.WriteLine(Helper.CenterStr("You don't have enough coins."));
                            continue;
                        }
                        else
                        {
                            player.Coins -= Weapons[purchaceChoice-1].Item2;
                            if (Weapons[purchaceChoice-1].Item1.Name == "Gun")
                            {
                                Console.WriteLine(Helper.CenterStr("\n\nSay hello to the 21st century.\n\n"));
                                Thread.Sleep(1500);
                            }
                            player.AddWeapon(Weapons[purchaceChoice-1].Item1);
                            Weapons.Remove(Weapons[purchaceChoice-1]);
                            
                            Console.WriteLine(Helper.CenterStr("Thank you for your patronage"));
                            Console.WriteLine(Helper.CenterStr($"You now have {player.Coins} Coins"));
                            Thread.Sleep(1500);
                            return;
                        }
                    }
                    if(weaponInput == 2){
                        Console.WriteLine(Helper.CenterStr("I'll upgrade your weapon for 10 coins"));
                        Console.WriteLine(Helper.CenterStr("1. Upgrade current weapon"));
                        Console.WriteLine(Helper.CenterStr("2. Nevermind"));
                        int WeaponUpgradeInput;
                        do{
                        parseSuccesful = int.TryParse(Console.ReadLine(), out WeaponUpgradeInput);
                        Helper.ClearLineDo();
                        } while(!parseSuccesful || !(1<=WeaponUpgradeInput && WeaponUpgradeInput<=2));
                        Console.Clear();
                        if (WeaponUpgradeInput==1){
                            
                            if(player.Coins <=0)
                            {
                                Console.WriteLine(Helper.CenterStr("You have no coins!"));
                                continue;
                            }
                            if(player.Coins > 0)
                            {
                                player.CurrentWeapon.RaiseMaxDamage(2);
                                Console.WriteLine(Helper.CenterStr("Here you go!"));
                                Console.WriteLine(Helper.CenterStr($"You now have {player.Coins} Coins"));
                                Thread.Sleep(1500);
                                return;
                            }
                        }
                        if(WeaponUpgradeInput==2) continue;
                    }
                    if(weaponInput == 3){
                        Console.WriteLine(Helper.CenterStr("You leave the weaponsmith"));
                        Thread.Sleep(1500);
                        return;
                    }
                }
        }
       
    }
}