using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class Character : MonoBehaviour
    {
        /* TODO
         * Nombre de moves
         * Représentation graphique
         * Sacrifice
         */

        [SerializeField]
        private new SpriteRenderer renderer = null;

        private int movesCapacity = 20;
        private Sprite characterSprite = null;

    }
}
