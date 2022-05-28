using UnityEngine;

class GridComponent : MonoBehaviour
{

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int offsetWidth = 0;
    [SerializeField] private int offsetHeight = 0;
    [SerializeField] private float cellSize;
    [SerializeField] private bool isShowDebug;

    private Grid<int> grid;
    private GridVisualizer<int> gridVisualizer;
    public Grid<int> GetGrid()
    {
        return grid;
    }

    private void Awake()
    {
        grid = new Grid<int>(width, height, cellSize, (Grid<int> g, int x, int y) => 1, offsetWidth, offsetHeight);
        gridVisualizer = new GridVisualizer<int>(grid);

        if (isShowDebug)
        {
            gridVisualizer.Show();
        }
    }
}
