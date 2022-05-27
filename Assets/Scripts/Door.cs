using UnityEngine;

namespace Sacrimatch3
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private new SpriteRenderer renderer = null;

        private SODoor doorData = null;
        
        private void Setup(SODoor doorData)
        {
            this.doorData = doorData;
            renderer.sprite = doorData.sprite;
        }
    }
}
