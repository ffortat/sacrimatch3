using UnityEngine;

namespace Sacrimatch3
{
    public class GemController
    {
        private Grid<GemController> grid;
        private int x;
        private int y;
        private Gem gem = null;
        private PuzzlePiece puzzlePiece = null;

        public GemController(Grid<GemController> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void Update()
        {
            if (gem)
            {
                Vector3 targetPosition = grid.GetWorldPosition(x, y);
                Vector3 moveDir = (targetPosition - gem.transform.position);
                float moveSpeed = 10f;
                gem.transform.position += moveDir * moveSpeed * Time.deltaTime;
                gem.Show = IsGemVisible();
            }
        }

        public void Swap(GemController otherGem)
        {
            Gem tempGem = otherGem.gem;

            otherGem.gem = gem;
            gem = tempGem;
        }

        public void DropTopGem()
        {
            for (int y = this.y + 1; y < grid.Height; y += 1)
            {
                if (!grid.GetValue(x, y).IsEmpty)
                {
                    Swap(grid.GetValue(x, y));
                    break;
                }
            }
        }

        public void Destroy()
        {
            if (gem)
            {
                gem.Destroy();
                gem = null;
            }
        }

        private bool IsGemVisible()
        {
            if (gem)
            {
                grid.GetGridPosition(gem.transform.position, out int x, out int y);
                return y < grid.Height;
            }

            return false;
        }

        public Gem Gem { get => gem; set => gem = value; }
        public PuzzlePiece PuzzlePiece { get => puzzlePiece; set => puzzlePiece = value; }
        public int X { get => x; }
        public int Y { get => y; }
        public bool IsEmpty { get => gem == null; }
    }
}