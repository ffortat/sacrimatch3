using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sacrimatch3
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private new SpriteRenderer renderer = null;

        private SODoor doorData = null;

        private int tilesX = 0;
        private int tilesY = 0;
        private List<int> unlockedTiles = new List<int>();
        private UnityEvent onDoorUnlocked = new UnityEvent();
        
        public void Setup(SODoor doorData)
        {
            this.doorData = doorData;
            renderer.sprite = doorData.sprite;
            
            if (doorData.tiles.Count > 0)
            {
                tilesX = Mathf.RoundToInt(doorData.sprite.rect.width / doorData.tiles[0].rect.width);
                tilesY = Mathf.RoundToInt(doorData.sprite.rect.height / doorData.tiles[0].rect.height);
            }
        }

        public void AddOnDoorUnlockedListener(UnityAction listener)
        {
            onDoorUnlocked.AddListener(listener);
        }

        public void UnlockTile(int index)
        {
            if (!unlockedTiles.Contains(index))
            {
                unlockedTiles.Add(index);

                if (unlockedTiles.Count == Tiles.Count)
                {
                    onDoorUnlocked?.Invoke();
                }
            }
            else
            {
                Debug.LogWarning("Tile " + index + " was already unlocked");
            }
        }

        public void GetTileCoordinates(int index, out int x, out int y)
        {
            x = index % tilesX;
            y = tilesY - 1 - Mathf.FloorToInt(index / (float)tilesX);
        }

        public List<Sprite> Tiles { get => doorData.tiles; }
        public int TilesWidth { get => tilesX; }
        public int TilesHeight { get => tilesY; }
    }
}
