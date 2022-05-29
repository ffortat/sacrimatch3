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

        private List<int> unlockedTiles = new List<int>();
        private UnityEvent onDoorUnlocked = new UnityEvent();
        
        public void Setup(SODoor doorData)
        {
            this.doorData = doorData;
            renderer.sprite = doorData.sprite;
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

        public List<Sprite> Tiles { get => doorData.tiles; }
    }
}
