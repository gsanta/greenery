using Base.Input;
using game.character.characters.player;
using game.scene.level;
using UnityEngine;

namespace game.scene.grid
{
    public class ScopedTileRenderer : TileRenderer
    {

        private int _radius = 5;

        PlayerStore _playerStore;

        LevelStore _levelStore;

        private PathNode _activeNode;

        private Vector2Int _radiusOriginPos;

        private bool _isDirty = false;

        public void Construct(PlayerStore playerStore, LevelStore levelStore, int radius = 0)
        {
            _playerStore = playerStore;
            _radius = radius;
            _levelStore = levelStore;
        }

        private void Update()
        {
            var grid = _levelStore.ActiveLevel.Grid;
            var player = _playerStore.GetCurrentPlayer();

            if (grid == null || player == null || !IsVisualize)
            {
                return;
            }

            var newPos = grid.GetGridPosition(player.transform.position);

            if (newPos.Value != _radiusOriginPos)
            {
                _isDirty = true;
                _radiusOriginPos = newPos.Value;
            }

            if (_isDirty)
            {
                _isDirty = false;
                Remove();
                Render();
            }
        }

        public void SetActiveNode(PathNode node)
        {
            _activeNode = node;
            _isDirty = true;
        }

        protected override GridGraph GetGridGraph()
        {
            return _levelStore.ActiveLevel.Grid;
        }

        protected override Vector2 GetUV(PathNode node)
        {
            if (node == _activeNode)
            {
                return new Vector2(0.5f, 0.0f);
            }
            else if (node.IsWalkable)
            {
                return new Vector2(0, 0);
            } else
            {
                return new Vector2(0.1f, 0.5f);
            }
        }

        protected override bool IsRenderQuad(int x, int y)
        {
            return IsWithinRadius(x, y);
        }

        private bool IsWithinRadius(int x, int y)
        {
            var player = _playerStore.GetCurrentPlayer();

            if (_radius == -1 || player == null)
            {
                return true;
            }

            if (x >= _radiusOriginPos.x - _radius && x <= _radiusOriginPos.x + _radius)
            {
                if (y >= _radiusOriginPos.y - _radius && y <= _radiusOriginPos.y + _radius)
                {
                    return true;
                }
            }

            return false;
        }

        protected override Vector2 GetOffsetPosition()
        {
            return _levelStore.ActiveLevel.RootGameObject.transform.position;
        }
    }
}
