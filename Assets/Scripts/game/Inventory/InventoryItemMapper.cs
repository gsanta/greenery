namespace game.Item
{
    public class InventoryItemMapper
    {
        public static ItemType GetType(string name)
        {
            switch(name)
            {
                case "grass1":
                    return ItemType.Grass1;
                case "grass2":
                    return ItemType.Grass2;
                default:
                    throw new System.Exception("Unkown inventory type: " + name);
            }
            
        }
    }
}
