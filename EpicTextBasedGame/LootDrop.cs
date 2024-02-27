public class LootDrop
{
    public Weapon? LootWeapon;
    public Item? LootItem;
    public int DropChance;

    public LootDrop(int dropChance, Weapon? weapon = null, Item? item = null)
    {
        this.LootWeapon = weapon;
        this.LootItem = item;
        this.DropChance = dropChance;
    }

    public bool ShouldDrop()
    {
        Random rand = new Random();
        int roll = rand.Next(1, 101); // Generate a random number between 1 and 100
        return roll <= DropChance;
    }
}