using System;
using UnityEngine;

public class Grid<TGridObject>
{
    private bool debug;

    private int width;
    private int height;
    private float cellsize;
    private Vector3 originPosition;

    private TGridObject[,] gridArray;

    public Grid(int width, int height, float cellsize, Func<Grid<TGridObject>, int, int, TGridObject> objectCreator) : this(width, height, cellsize, Vector3.zero, objectCreator) { }

    public Grid(int width, int height, float cellsize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> objectCreator)
    {
        debug = false;
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x += 1)
        {
            for (int y = 0; y < gridArray.GetLength(1); y += 1)
            {
                gridArray[x, y] = objectCreator(this, x, y);
            }
        }
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        GetGridPosition(worldPosition, out int x, out int y);
        SetValue(x, y, value);
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default;
        }
    }

    public TGridObject GetValue(Vector3 worldPosition)
    {
        GetGridPosition(worldPosition, out int x, out int y);
        return GetValue(x, y);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellsize + originPosition;
    }

    private void GetGridPosition(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellsize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellsize);
    }

    private void DrawDebug()
    {
        if (debug)
        {
            for (int x = 0; x < gridArray.GetLength(0); x += 1)
            {
                for (int y = 0; y < gridArray.GetLength(1); y += 1)
                {
                    // Debug display
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
    }

    public int Height { get => height; }
    public int Width { get => width; }
    public float Cellsize { get => cellsize; }
    public bool ShowDebug {
        get => debug;
        set {
            debug = value;
            DrawDebug();
        }
    }
}