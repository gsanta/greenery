using UnityEngine;

namespace game.scene.level
{
    public class LevelLoader : MonoBehaviour
    {
        private static LevelLoader _instance;
        
        public void Construct()
        {
            _instance = this;
        }

        public static LevelLoader GetInstance()
        {
            return _instance;
        }

        public void Load(string name)
        {
            
        }
    }
}