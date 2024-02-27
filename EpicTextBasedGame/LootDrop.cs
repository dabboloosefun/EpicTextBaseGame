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
}