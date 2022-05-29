using UnityEngine;

namespace Sacrimatch3
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private new SpriteRenderer renderer = null;

        private int movesCapacity = 20;
        private int movesUsed = 0;

        private Vector3 targetPosition = Vector3.zero;

        private SOCharacter characterData = null;
        private new Collider2D collider = null;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        public void Setup(SOCharacter characterData)
        {
            this.characterData = characterData;
            movesCapacity = characterData.moves;
            renderer.sprite = characterData.sprite;

            targetPosition = transform.localPosition;
        }

        public bool ContainsPosition(Vector3 position)
        {
            return collider.bounds.Contains(position);
        }

        public void Sacrifice()
        {
            renderer.sprite = characterData.spriteDead;
        }

        public SOCharacter Data { get => characterData; }
        public Vector3 Position {
            get => transform.localPosition;
            set => targetPosition = value;
        }
        public Vector3 TargetPosition {
            get => targetPosition;
            set => targetPosition = value;
        }
    }
}
