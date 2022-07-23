using AI.pathFinding;
using UnityEngine;

namespace AI.GridSystem
{
    public class GridComponent : MonoBehaviour
    {

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private bool isShowDebug;

        private Grid<PathNode> _grid;
        private GridVisualizer<PathNode> _gridVisualizer;
        private PathFinding _pathFinding;

        private void Awake()
        {
            _grid = new Grid<PathNode>(width, height, (g, x, y) => new PathNode(x, y), cellSize);
            _gridVisualizer = new GridVisualizer<PathNode>(_grid);
            _pathFinding = new PathFinding(_grid);

            if (isShowDebug)
            {
                _gridVisualizer.Show();
            }
        }

        public Grid<PathNode> GetGrid()
        {
            return _grid;
        }

        public PathFinding GetPathFinding()
        {
            return _pathFinding;
        }
    }
}
