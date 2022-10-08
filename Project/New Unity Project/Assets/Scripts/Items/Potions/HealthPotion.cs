public class HealthPotion : InventoryItem
{
    public PotionInfo potionInfo => itemInfo as PotionInfo;
    public void Use(Player handler)
    {
        handler.GetHealth(potionInfo.healthAmount);
    }
}
