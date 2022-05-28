using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        [SerializeField]
        private float moveSpeed = 5f;

        private GameObject characterHolder = null;
        private List<Character> party = new List<Character>();
        private UnityEvent onSacrifice = new UnityEvent();

        private DoorController doorController = null;

        private void Awake()
        {
            characterHolder = new GameObject();
            characterHolder.transform.position = CameraController.TopLeft + new Vector3(2, -2);
            characterHolder.transform.parent = transform;

            for (int i = 0; i < partySize; i += 1)
            {
                Character character = Instantiate(characterPrefab, characterHolder.transform.position, Quaternion.identity, characterHolder.transform);
                character.Setup(characters[Random.Range(0, characters.Count)]);
                character.Position = new Vector3(i, 0);
                party.Add(character);
            }
        }

        private void Start()
        {
        }

        private void Update()
        {
            party.ForEach((Character character) =>
            {
                Vector3 moveDir = (character.TargetPosition - character.Position);
                character.transform.position += moveDir * moveSpeed * Time.deltaTime;
            });
        }

        public void Setup(DoorController doorController)
        {
            this.doorController = doorController;
        }

        public void AddOnSacrificeListener(UnityAction listener)
        {
            onSacrifice.AddListener(listener);
        }

        public void Sacrifice(Character character)
        {
            character.Sacrifice();
            onSacrifice?.Invoke();
        }
    }
}
