using UnityEngine;

namespace Grid
{
    class GridComponent : MonoBehaviour
    {

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int offsetWidth = 0;
        [SerializeField] private int offsetHeight = 0;
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
            _grid = new Grid<int>(width, height, cellSize, (Grid<int> g, int x, int y) => 1, offsetWidth, offsetHeight);
            _gridVisualizer = new GridVisualizer<int>(_grid);

            if (isShowDebug)
            {
                _gridVisualizer.Show();
            }
        }
    }
}
