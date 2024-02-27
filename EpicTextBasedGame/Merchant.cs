using System.Runtime.ConstrainedExecution;
using System.Security;

public class Merchant{
    public string Name;
    public List<(Item, int)> RegularWares = new();
    static int RandomPotionsMade = 0; //to give random potions new ID's
    
    public Merchant(string name){
        Name = name;
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
                    if(potionInput == 1){
                        Console.WriteLine($"You have {player.Coins} Coins");
                        Console.WriteLine("The merchant has these items for sale:");
                        for(int i = RegularWares.Count; i>0; i--)
                        {
                            Console.WriteLine($"{i+1}. {RegularWares[i].Item1.Info()} for {RegularWares[i].Item2} gold coins each");
                        }
                        string userInput;
                        int purchaceChoice;
                        Console.WriteLine("/n");
                        do{
                            Helper.ClearLastLine();
                            Console.WriteLine("Enter the number of an item you wish to purchace, or 'back' to leave");
                            userInput = Console.ReadLine().ToLower();
                        parseSuccesful = int.TryParse(userInput, out purchaceChoice);
                        } while(!(userInput=="back" || (parseSuccesful && 1 <= purchaceChoice && purchaceChoice <= RegularWares.Count))); //there offset of +1 so not Count-1
                        if(userInput=="back"){
                            continue;
                        }
                        else if(player.Coins < RegularWares[purchaceChoice-1].Item2) 
                        {
                            Console.WriteLine("You don't have enough coins.");
                            continue;
                        }
                        else
                        {
                            player.Coins -= RegularWares[purchaceChoice-1].Item2;
                            player.AddItem(RegularWares[purchaceChoice-1].Item1);
                            RegularWares.Remove(RegularWares[purchaceChoice-1]);
                            Console.WriteLine("Thank you for your patronage");
                            Console.WriteLine($"You now have {player.Coins} Coins");
                            return;
                        }
                    }
                    if(potionInput == 3){
                        Console.WriteLine("You leave the potion merchant");
                        return;
                    }
                }
            case "Weaponsmith":
            break;
        }
       
    }
}