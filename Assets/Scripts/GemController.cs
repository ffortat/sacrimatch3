namespace Sacrimatch3
{
    public class GemController
    {
        private Grid<GemController> grid;
        private int x;
        private int y;

        public GemController(Grid<GemController> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public SOGem Gem { get; set; }
    }
}