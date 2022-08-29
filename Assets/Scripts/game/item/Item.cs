using UnityEngine;

namespace game.item
{
    public class Item
    {
        
        public static bool IsGrass(GameObject gameObject)
        {
            return gameObject.name.StartsWith("Grass");
        }
        
    }
}