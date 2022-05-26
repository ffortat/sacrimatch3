using UnityEngine;

namespace Sacrimatch3
{
    public class GemController
    {
        private Grid<GemController> grid;
        private int x;
        private int y;
        private Gem gem;

        public GemController(Grid<GemController> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void Swap(GemController otherGem)
        {
            Gem tempGem = otherGem.gem;

            otherGem.gem = gem;
            gem = tempGem;
        }

        public void Update()
        {
            if (gem)
            {
                Vector3 targetPosition = grid.GetWorldPosition(x, y);
                Vector3 moveDir = (targetPosition - gem.transform.position);
                float moveSpeed = 10f;
                gem.transform.position += moveDir * moveSpeed * Time.deltaTime;
            }
        }

        public void Destroy()
        {
            Object.Destroy(gem);
            gem = null;
        }

        public Gem Gem { get => gem; set => gem = value; }
        public int X { get => x; }
        public int Y { get => y; }
    }
}