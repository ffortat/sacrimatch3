using System;
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

        private float busyTimer = 0f;
        private State state = State.Pause;
        private Action busyCallback;

        private GridGenerator generator = null;
        private Grid<GemController> grid = null;

        private void Awake()
        {
            generator = new GridGenerator();
            Setup(generator.Grid);
            SetState(State.Input);
        }

        private void Update()
        {
            UpdateVisuals();

            switch (state)
            {
                case State.Busy:
                    busyTimer -= Time.deltaTime;
                    if (busyTimer <= 0f)
                    {
                        busyCallback();
                    }
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
                    if (FindMatchAndDestroy())
                    {
                        Delay(0.5f, () => { SetState(State.Matching); });
                    }
                    else
                    {
                        SetState(State.Input);
                    }
                    // TODO trouver les matchs
                    // TODO d�truire les matchs
                    // TODO faire tomber les gems dans les espaces libres
                    // TODO cr�er les nouvelles gems dans les espaces libres en haut
                    // TODO v�rifier l'�tat du jeu pour voir s'il y a des possibilit�s de d�placement (pour randomiser)
                    // TODO repasser en �tat Input
                    break;
                case State.Pause:
                    break;
                default:
                    SetState(State.Pause);
                    break;
            }
        }

        private void Setup(Grid<GemController> grid)
        {
            this.grid = grid;

            grid.ForEach((int x, int y, GemController gemController) =>
            {
                Gem gem = Instantiate(gemPrefab, grid.GetWorldPosition(x, y), Quaternion.identity);
                gem.Setup(gems[UnityEngine.Random.Range(0, gems.Count)]);

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

                SetState(State.Matching);

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

        private void SetState(State state)
        {
            this.state = state;
        }

        private void Delay(float delay, Action callback)
        {
            SetState(State.Busy);
            busyTimer = delay;
            busyCallback = callback;
        }

        private bool FindMatchAndDestroy()
        {
            bool[,] seen = new bool[grid.Width, grid.Height];
            List<List<GemController>> linksList = new List<List<GemController>>();

            grid.ForEach((int x, int y, GemController gemController) =>
            {
                if (!seen[x, y])
                {
                    List<List<GemController>> gemLinks = GetMatch3Links(gemController);

                    if (gemLinks.Count > 0)
                    {
                        linksList.AddRange(gemLinks);

                        foreach (List<GemController> link in gemLinks)
                        {
                            foreach (GemController gem in link)
                            {
                                seen[gem.X, gem.Y] = true;
                            }
                        }
                    }
                    else
                    {
                        seen[x, y] = true;
                    }
                }
            });

            foreach (List<GemController> link in linksList)
            {
                foreach (GemController gem in link)
                {
                    gem.Destroy();
                }
            }

            return false;
        }

        private List<List<GemController>> GetMatch3Links(GemController gem)
        {
            List<List<GemController>> linksList = new List<List<GemController>>();
            List<GemController> link;

            int leftLinks = 0;
            int rightLinks = 0;
            int upLinks = 0;
            int downLinks = 0;

            for (int x = gem.X - 1; x >= 0; x -= 1)
            {
                if (grid.GetValue(x, gem.Y).Gem.Type == gem.Gem.Type)
                {
                    leftLinks += 1;
                }
                else
                {
                    break;
                }
            }

            for (int x = gem.X + 1; x < grid.Width; x += 1)
            {
                if (grid.GetValue(x, gem.Y).Gem.Type == gem.Gem.Type)
                {
                    rightLinks += 1;
                }
                else
                {
                    break;
                }
            }

            if (leftLinks + 1 + rightLinks >= 3)
            {
                link = new List<GemController>();
                for (int x = gem.X - leftLinks; x <= gem.X + rightLinks; x += 1)
                {
                    link.Add(grid.GetValue(x, gem.Y));
                }

                linksList.Add(link);
            }

            for (int y = gem.Y - 1; y >= 0; y -= 1)
            {
                if (grid.GetValue(gem.X, y).Gem.Type == gem.Gem.Type)
                {
                    downLinks += 1;
                }
                else
                {
                    break;
                }
            }

            for (int y = gem.Y + 1; y < grid.Height; y += 1)
            {
                if (grid.GetValue(gem.X, y).Gem.Type == gem.Gem.Type)
                {
                    upLinks += 1;
                }
                else
                {
                    break;
                }
            }

            if (downLinks + 1 + upLinks >= 3)
            {
                link = new List<GemController>();
                for (int y = gem.Y - downLinks; y <= gem.Y + upLinks; y += 1)
                {
                    link.Add(grid.GetValue(gem.X, y));
                }

                linksList.Add(link);
            }

            return linksList;
        }

        private Vector3 GetMousePosition()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            return mousePosition;
        }
    }
}
