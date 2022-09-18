using game.scene.grid;
using game.scene.grid.path;
using UnityEngine;

namespace Assets.Scripts.debug
{
    public class PathFindingDebug : MonoBehaviour
    {
        [SerializeField] private PathVisualizer pathVisualizerPrefab;

        [SerializeField] private Transform from;
        [SerializeField] private Transform to;

        private PathVisualizer _pathVisualizer;

        private PathFinding pathFinding;

        public void RenderPath()
        {
            if (!_pathVisualizer)
            {
                _pathVisualizer = Instantiate(pathVisualizerPrefab, new Vector3(0, 0, 0), transform.rotation);
            }
        }
    }
}
