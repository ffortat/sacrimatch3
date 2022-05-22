using UnityEngine;

public class Match3 : MonoBehaviour
{
    private Grid<int> grid = null;

    private void Start()
    {
        grid = new Grid<int>(10, 15, 1f, new Vector3(-5, -10), (Grid<int> grid, int x, int y) => 0);
        grid.ShowDebug = true;
    }
}
