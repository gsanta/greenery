using UnityEngine;

namespace AI.GridSystem
{
    class GridComponent : MonoBehaviour
    {

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private bool isShowDebug;

        private Grid<PathNode> _grid;
        private GridVisualizer<PathNode> _gridVisualizer;

        private void Awake()
        {
            _grid = new Grid<PathNode>(width, height, (g, x, y) => new PathNode(x, y), cellSize);
            _gridVisualizer = new GridVisualizer<PathNode>(_grid);

            if (isShowDebug)
            {
                _gridVisualizer.Show();
            }
        }
    }
}
