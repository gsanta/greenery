
using game.scene.grid;
using game.scene.tile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.level
{
    public class Environment
    {
        private Tilemap _tilemapGround;

        private Tilemap _tilemapObjects; 

        private Transform _blocksContainer;

        private Vector2Int _offset;

        private Level _level;

        public Environment(Level level, Transform blocksContainer, Tilemap tilemapGround, Tilemap tilemapObjects)
        {
            _blocksContainer = blocksContainer;
            _tilemapGround = tilemapGround;
            _tilemapObjects = tilemapObjects;
            _level = level;
        }

        public void Init()
        {
            var minBounds = _tilemapGround.localBounds.min;
            _offset = new Vector2Int((int)minBounds.x, (int)minBounds.y);
        }

        public void SetUnWalkable(GridGraph<PathNode> graph)
        {
            SetUnWalkableTiles(graph);
            SetUnWalkableObstacles(graph);
        }

        private void SetUnWalkableTiles(GridGraph<PathNode> graph)
        {
            var width = graph.Width;
            var height = graph.Height;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    graph.GetNode(i, j).IsWalkable = IsWalkable(new Vector2Int(i, j));

                }
            }
        }

        private bool IsWalkable(Vector2Int pos)
        {
            var tile = _tilemapObjects.GetTile(new Vector3Int(pos.x + _offset.x, pos.y + _offset.y, 0));

            return tile == null;
        }

        private void SetUnWalkableObstacles(GridGraph<PathNode> graph)
        {
            foreach (Transform child in _blocksContainer)
            {
                var bounds = child.GetComponent<Collider2D>().bounds;

                var bottomLeftWorld = new Vector2(bounds.min.x, bounds.min.y);
                var topRightWorld = new Vector2(bounds.max.x, bounds.max.y);
                var bottomLeftGrid = graph.GetGridPosition(bottomLeftWorld);
                var topRightGrid = graph.GetGridPosition(topRightWorld);

                if (!bottomLeftGrid.HasValue || !topRightGrid.HasValue)
                {
                    break;
                }

                for (var i = bottomLeftGrid.Value.x; i <= topRightGrid.Value.x; i++)
                {
                    for (var j = bottomLeftGrid.Value.y; j <= topRightGrid.Value.y; j++)
                    {
                        graph.GetNode(i, j).IsWalkable = false;
                    }
                }
            }
        }
    }
}
