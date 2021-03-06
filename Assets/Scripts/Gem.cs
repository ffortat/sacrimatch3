using UnityEngine;

namespace Sacrimatch3
{
    public class Gem : MonoBehaviour
    {
        [SerializeField]
        private new SpriteRenderer renderer = null;

        private SOGem gemData = null;

        public void Setup(SOGem gemData)
        {
            this.gemData = gemData;
            renderer.sprite = gemData.sprite;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public Sprite Sprite {
            get => renderer.sprite;
            set => renderer.sprite = value;
        }
        public SOGem Type { get => gemData; }
        public bool Show {
            get => renderer.enabled;
            set {
                if (renderer.enabled != value)
                {
                    renderer.enabled = value;
                }
            }
        }
    }
}
