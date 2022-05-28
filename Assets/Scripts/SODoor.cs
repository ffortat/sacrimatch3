using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    [CreateAssetMenu(fileName = "Door", menuName = "ScriptableObjects/New Door", order = 2)]
    public class SODoor : ScriptableObject
    {
        public Sprite sprite = null;
        public List<Sprite> tiles = null;
    }
}
