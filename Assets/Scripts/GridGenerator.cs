using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class GridGenerator
    {
        private Grid<GemController> grid;

        public GridGenerator(List<SOGem> gems)
        {
            grid = new Grid<GemController>(10, 15, 1f, new Vector3(-5, -10), (Grid<GemController> grid, int x, int y) => new GemController(grid, x, y));
            grid.ShowDebug = true;

            grid.ForEach((int x, int y, GemController gemController) =>
            {
                gemController.Gem = gems[Random.Range(0, gems.Count)];
            });
        }

        public Grid<GemController> Grid { get => grid; }
    }
}
