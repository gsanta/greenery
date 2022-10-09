
using UnityEngine;

namespace game.scene.level
{
    public class LevelLoadingInfo
    {
        public LevelName levelName;
        public Vector2 position;
        public bool IsLoaded;

        public LevelLoadingInfo(LevelName levelName, Vector2 position)
        {
            this.levelName = levelName;
            this.position = position;
        }
    }
}
