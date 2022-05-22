using UnityEngine;

namespace Sacrimatch3
{
    public class Match3Visual : MonoBehaviour
    {
        [SerializeField]
        private GemVisual gemPrefab = null;

        private Grid<GemController> grid = null;

        private void Awake()
        {
            // TODO initialization
        }

        private void Update()
        {
            // TODO process visual updates if needed
        }

        public void Setup(Grid<GemController> grid)
        {
            this.grid = grid;

            grid.ForEach((int x, int y, GemController gemController) =>
            {
                GemVisual gemVisual = Instantiate(gemPrefab, grid.GetWorldPosition(x, y), Quaternion.identity);
                gemVisual.Sprite = gemController.Gem.sprite;
            });
        }
    }
}
