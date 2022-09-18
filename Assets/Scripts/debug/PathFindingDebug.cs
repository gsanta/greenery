using game.scene.grid;
using game.scene.grid.path;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.debug
{
    public class PathFindingDebug : MonoBehaviour
    {
        [SerializeField] private PathVisualizer pathVisualizerPrefab;

        [SerializeField] private Transform from;

        [SerializeField] private Transform to;

        private PathVisualizer _pathVisualizer;

        private PathFinding _pathFinding;

        public void RenderPath(GridGraph<PathNode> grid)
        {
            if (!_pathVisualizer)
            {
                _pathVisualizer = Instantiate(pathVisualizerPrefab, new Vector3(0, 0, 0), transform.rotation);
            }

            var pathNodeList = new List<PathNode>();
            var path = _pathFinding.FindPath(grid, from.position, to.position, out pathNodeList);

            _pathVisualizer.SetPath(path);
        }
    }
}
