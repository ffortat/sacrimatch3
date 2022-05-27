using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private Character characterPrefab = null;
        [SerializeField]
        private int partySize = 5;
        [SerializeField]
        private List<SOCharacter> characters = new List<SOCharacter>();

        private List<Character> party = new List<Character>();

        private void Awake()
        {
            for (int i = 0; i < partySize; i += 1)
            {
                Character character = Instantiate(characterPrefab, new Vector3(-16, 8), Quaternion.identity, transform);
                character.Setup(characters[Random.Range(0, characters.Count)]);
                party.Add(character);
            }
        }

        private void Start()
        {
        }
    }
}
