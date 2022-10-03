namespace game.Inventory
{
    public class InventoryItemMapper
    {
        public static InventoryItemType GetType(string name)
        {
            switch(name)
            {
                case "grass1":
                    return InventoryItemType.Grass1;
                default:
                    throw new System.Exception("Unkown inventory type: " + name);
            }
            
        }
    }
}
