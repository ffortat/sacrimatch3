using UnityEngine;

namespace Sacrimatch3
{
    [CreateAssetMenu(fileName = "NameGem", menuName = "ScriptableObjects/New Gem", order = 2)]
    public class SOGem : ScriptableObject
    {
        public string gemName;
        public Sprite sprite;
    }
}
