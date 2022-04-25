using System;
using UnityEngine;

public class Grid<TGridObject>
{
    public readonly int width;
    public readonly int height;
    private TGridObject[,] gridArray;

    public Grid(int width, int height, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }
    }

    public bool IsWithinGrid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    public TGridObject[] GetColumnObjects(int x)
    {
        TGridObject[] row = new TGridObject[width];

        for (int i = 0; i < width; i++)
        {
            row[i] = gridArray[x, i];
        }

        return row;
    }

    public TGridObject[] GetRowObjects(int y)
    {
        TGridObject[] columns = new TGridObject[height];

        for (int i = 0; i < height; i++)
        {
            columns[i] = gridArray[i, y];
        }

        return columns;
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
}
