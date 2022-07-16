using UnityEngine;

namespace AI.GridSystem
{
    class GridComponent : MonoBehaviour
    {

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private bool isShowDebug;

        private Grid<int> _grid;
        private GridVisualizer<int> _gridVisualizer;
        public Grid<int> GetGrid()
        {
            return _grid;
        }

        private void Awake()
        {
            _grid = new Grid<int>(width, height, (Grid<int> g, int x, int y) => 1, cellSize);
            _gridVisualizer = new GridVisualizer<int>(_grid);

            if (isShowDebug)
            {
                _gridVisualizer.Show();
            }
        }
    }
}
