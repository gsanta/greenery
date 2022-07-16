using AI.GridSystem;
using UnityEngine;

public class World
{
    public Vector3 originPosition;
    public float gridRatio;
    private Grid<object> grid;
    private Vector3 worldSize;
    
    public World(Grid<object> grid, float gridRatio, Vector3 originPosition)
    {
        this.grid = grid;
        this.gridRatio = gridRatio;
        this.originPosition = originPosition;
        worldSize = new Vector3(grid.Width * gridRatio, grid.Height * gridRatio, 0);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        var position = new Vector3(x, y) * gridRatio + originPosition - worldSize / 2;
        position.z = originPosition.z;
        return position;
    }
    
    public void GetGridPosition(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition + worldSize / 2).x / gridRatio);
        y = Mathf.FloorToInt((worldPosition - originPosition + worldSize / 2).y / gridRatio);
    }
}