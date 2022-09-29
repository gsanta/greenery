using game.scene.grid;
using game.scene.grid.path;
using game.scene.level;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.debug
{
    public class PathFindingDebug : MonoBehaviour
    {
        [SerializeField] private Transform from;

        [SerializeField] private Transform to;

        [SerializeField] private PathVisualizer pathVisualizer;

        private PathFinding _pathFinding = new PathFinding();

        public void RenderPath(Level level)
        {

            var levelOffset = level.EnvironmentData.Center;
            var fromPos = from.transform.position - levelOffset;
            var toPos = to.transform.position - levelOffset;

            var pathNodeList = new List<PathNode>();
            var path = _pathFinding.FindPath(level.Grid, fromPos, toPos, out pathNodeList);

            if (path != null)
            {
                var offsetedPath = path.Select((pos) => pos + (Vector2)levelOffset).ToList();

                pathVisualizer.SetLevel(level);
                pathVisualizer.SetPath(offsetedPath);
            }
        }
    }
}
