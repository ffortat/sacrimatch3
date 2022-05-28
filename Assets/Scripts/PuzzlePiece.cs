using UnityEngine;

namespace Sacrimatch3
{
    public class PuzzlePiece : MonoBehaviour
    {
        [SerializeField]
        private new SpriteRenderer renderer = null;

        private int index = -1;

        public void Setup(int index, Sprite sprite)
        {
            this.index = index;
            renderer.sprite = sprite;
        }

        Sprite Sprite {
            get => renderer.sprite;
            set => renderer.sprite = value;
        }
    }
}
