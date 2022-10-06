using UnityEngine;

namespace game.Item
{
    public class ItemInfo
    {
        
        public static bool IsGrass(GameObject gameObject)
        {
            return gameObject.name.StartsWith("Grass");
        }
        
    }
}