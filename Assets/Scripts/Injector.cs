using UnityEngine;

public class Injector : MonoBehaviour
{
    [SerializeField]
    private DrawScript drawScript;

    [SerializeField]
    private Player player;

    private Grid<int> grid;
    private GridVisualizer<int> gridVisualizer;

    private void Start()
    {
        player.Construct(drawScript);

        grid = new Grid<int>(10, 10, 2f, (Grid<int> g, int x, int y) => 1);
        gridVisualizer = new GridVisualizer<int>(grid);
        gridVisualizer.Show();
    }
}
