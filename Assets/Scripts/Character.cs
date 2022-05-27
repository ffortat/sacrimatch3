using UnityEngine;

namespace Sacrimatch3
{
    public class Character : MonoBehaviour
    {
        /* TODO
         * Sacrifice
         */

        [SerializeField]
        private new SpriteRenderer renderer = null;

        private int movesCapacity = 20;
        private int movesUsed = 0;

        private SOCharacter characterData = null;

        public void Setup(SOCharacter characterData)
        {
            this.characterData = characterData;
            movesCapacity = characterData.moves;
            renderer.sprite = characterData.sprite;
        }

        public SOCharacter Data { get => characterData; }
    }
}
