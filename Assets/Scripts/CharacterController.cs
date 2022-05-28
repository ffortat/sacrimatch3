using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sacrimatch3
{
    public class CharacterController : MonoBehaviour
    {
        public enum State
        {
            Stay,
            Move,
            Select,
        }

        [SerializeField]
        private Character characterPrefab = null;
        [SerializeField]
        private int partySize = 5;
        [SerializeField]
        private List<SOCharacter> characters = new List<SOCharacter>();

        [SerializeField]
        private float moveSpeed = 5f;

        private float moveTimer = 0f;
        private State state = State.Stay;
        private Action moveCallback;

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
                character.Setup(characters[UnityEngine.Random.Range(0, characters.Count)]);
                character.Position = new Vector3(i, 0);
                party.Add(character);
            }

            Delay(0.5f, () => state = State.Select);
        }

        private void Update()
        {
            MoveCharacters();

            switch (state)
            {
                case State.Move:
                    moveTimer -= Time.deltaTime;
                    if (moveTimer <= 0f)
                    {
                        moveCallback();
                    }
                    break;
                case State.Select:
                    if (Input.GetMouseButton(0))
                    {
                        party.ForEach((character) =>
                        {
                            if (character.ContainsPosition(Input.mousePosition))
                            {
                                SelectCharacter(character);
                            }
                        });
                    }
                    break;
                case State.Stay:
                    break;
                default:
                    state = State.Stay;
                    break;
            }
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

        private void SetState(State state)
        {
            this.state = state;
        }

        private void Delay(float delay, Action callback)
        {
            SetState(State.Move);
            moveTimer = delay;
            moveCallback = callback;
        }

        private void SelectCharacter(Character character)
        {
            Sacrifice(character);
        }

        private void MoveCharacters()
        {
            party.ForEach((Character character) =>
            {
                Vector3 moveDir = (character.TargetPosition - character.Position);
                character.transform.position += moveDir * moveSpeed * Time.deltaTime;
            });
        }
    }
}
