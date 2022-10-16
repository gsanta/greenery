using game.scene.level;
using UnityEngine;

namespace game.scene.grid
{
    public class LevelTileRenderer : TileRenderer
    {
        private Level _level;

        public void SetLevel(Level level)
        {
            _level = level;
        }

        protected override GridGraph GetGridGraph()
        {
            return _level.Grid;    
        }

        protected override Vector2 GetUV(PathNode node)
        {
            if (node.IsWalkable)
            {
                return new Vector2(0, 0);
            }
            else
            {
                return new Vector2(0.1f, 0.5f);
            }

        }

        protected override bool IsRenderQuad(int x, int y)
        {
            return true;            
        }

        protected override Vector2 GetOffsetPosition()
        {
            return _level.RootGameObject.transform.position;
        }
    }
}
