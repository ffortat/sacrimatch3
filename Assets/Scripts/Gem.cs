using UnityEngine;

namespace Sacrimatch3
{
    public class Gem : MonoBehaviour
    {
        [SerializeField]
        private new SpriteRenderer renderer = null;

        private void Awake()
        {
            // TODO visual initialization
        }

        public Sprite Sprite {
            get => renderer.sprite;
            set => renderer.sprite = value;
        }
    }
}
