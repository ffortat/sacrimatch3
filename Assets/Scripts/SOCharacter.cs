using UnityEngine;

namespace Sacrimatch3
{
    [CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/New Character", order = 1)]
    public class SOCharacter : ScriptableObject
    {
        public int moves = 20;
        public Sprite sprite = null;
    }
}
