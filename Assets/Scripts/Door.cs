using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private new SpriteRenderer renderer = null;

        private SODoor doorData = null;
        
        public void Setup(SODoor doorData)
        {
            this.doorData = doorData;
            renderer.sprite = doorData.sprite;
        }

        public List<Sprite> Tiles { get => doorData.tiles; }
    }
}
