using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class Match3 : MonoBehaviour
    {
        [SerializeField]
        private Gem gemPrefab = null;
        [SerializeField]
        private List<SOGem> gems = new List<SOGem>();

        private int startX = 0;
        private int startY = 0;

        private GridGenerator generator = null;
        private Grid<GemController> grid = null;

        private void Awake()
        {
            generator = new GridGenerator();
            Setup(generator.Grid);
        }

        private void Update()
        {
            UpdateVisuals();

            if (Input.GetMouseButtonDown(0))
            {
                grid.GetGridPosition(GetMousePosition(), out startX, out startY);
            }

            if (Input.GetMouseButtonUp(0))
            {
                grid.GetGridPosition(GetMousePosition(), out int x, out int y);

                SwapGems(startX, startY, x, y);
            }
        }

        private void Setup(Grid<GemController> grid)
        {
            this.grid = grid;

            grid.ForEach((int x, int y, GemController gemController) =>
            {
                Gem gem = Instantiate(gemPrefab, grid.GetWorldPosition(x, y), Quaternion.identity);
                gem.Sprite = gems[Random.Range(0, gems.Count)].sprite;

                gemController.Gem = gem;
            });
        }

        private void SwapGems(int x1, int y1, int x2, int y2)
        {
            GemController gem1 = generator.Grid.GetValue(x1, y1);
            GemController gem2 = generator.Grid.GetValue(x2, y2);

            gem1.Swap(gem2);
        }

        private void UpdateVisuals()
        {
            grid.ForEach((int x, int y, GemController gemController) =>
            {
                gemController.Update();
            });
        }

        private Vector3 GetMousePosition()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            return mousePosition;
        }
    }
}
