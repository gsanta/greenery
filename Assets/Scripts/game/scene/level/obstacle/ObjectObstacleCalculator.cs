
using game.scene.grid;
using UnityEngine;

namespace game.scene.level.obstacle
{
    public class ObjectObstacleCalculator : IObstacleCalculator
    {
        private EnvironmentData _environmentData;

        public ObjectObstacleCalculator(EnvironmentData environmentData)
        {
            _environmentData = environmentData;
        }

        public void Calculate(GridGraph grid)
        {
            var container = _environmentData.ObjectsContainer;
            foreach (Transform child in container)
            {
                if (!child.gameObject.activeInHierarchy)
                {
                    break;
                }
                
                var bounds = child.GetComponent<Collider2D>().bounds;

                var bottomLeftWorld = new Vector2(bounds.min.x, bounds.min.y);
                var topRightWorld = new Vector2(bounds.max.x, bounds.max.y);
                var bottomLeftGrid = grid.GetGridPosition(bottomLeftWorld);
                var topRightGrid = grid.GetGridPosition(topRightWorld);

                if (!bottomLeftGrid.HasValue || !topRightGrid.HasValue)
                {
                    break;
                }

                for (var i = bottomLeftGrid.Value.x; i <= topRightGrid.Value.x; i++)
                {
                    for (var j = bottomLeftGrid.Value.y; j <= topRightGrid.Value.y; j++)
                    {
                        grid.GetNode(i, j).IsWalkable = false;
                    }
                }
            }
        }
    }
}
