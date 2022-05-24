using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class Match3 : MonoBehaviour
    {
        public enum State
        {
            Busy,
            Input,
            Matching,
            Pause,
        }

        [SerializeField]
        private Gem gemPrefab = null;
        [SerializeField]
        private List<SOGem> gems = new List<SOGem>();

        private bool dragging = false;
        private int startX = 0;
        private int startY = 0;

        private State state = State.Busy;

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

            switch (state)
            {
                case State.Busy:
                    break;
                case State.Input:
                    if (Input.GetMouseButtonDown(0))
                    {
                        grid.GetGridPosition(GetMousePosition(), out startX, out startY);
                        dragging = true;
                    }

                    if (dragging)
                    {
                        grid.GetGridPosition(GetMousePosition(), out int x, out int y);

                        if (SwapGems(startX, startY, x, y) || Input.GetMouseButtonUp(0))
                        {
                            dragging = false;
                        }
                    }
                    break;
                case State.Matching:
                    // TODO trouver les matchs
                    // TODO détruire les matchs
                    // TODO faire tomber les gems dans les espaces libres
                    // TODO créer les nouvelles gems dans les espaces libres en haut
                    // TODO vérifier l'état du jeu pour voir s'il y a des possibilités de déplacement (pour randomiser)
                    // TODO repasser en état Input
                    break;
                case State.Pause:
                    break;
                default:
                    state = State.Pause;
                    break;
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

        private bool SwapGems(int x1, int y1, int x2, int y2)
        {
            int diffX = Mathf.Abs(x1 - x2);
            int diffY = Mathf.Abs(y1 - y2);
            
            if ((diffX == 1 && diffY == 0) || (diffX == 0 && diffY == 1))
            {
                GemController gem1 = generator.Grid.GetValue(x1, y1);
                GemController gem2 = generator.Grid.GetValue(x2, y2);

                gem1.Swap(gem2);

                return true;
            }

            return false;
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
