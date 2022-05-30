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
        private Vector3 targetPosition = Vector3.zero;

        private GameObject characterHolder = null;
        private List<Character> party = new List<Character>();
        private UnityEvent onSacrifice = new UnityEvent();
        private UnityEvent onMovesUpdated = new UnityEvent();
        private UnityEvent onMovesDepleted = new UnityEvent();
        private UnityEvent onNoMovesLeft = new UnityEvent();
        private Character characterSelected = null;

        private void Awake()
        {
            characterHolder = new GameObject();
            characterHolder.transform.position = CameraController.TopLeft + new Vector3(-5 - partySize, -2);
            characterHolder.transform.parent = transform;

            targetPosition = CameraController.TopLeft + new Vector3(2, -2);

            for (int i = 0; i < partySize; i += 1)
            {
                Character character = Instantiate(characterPrefab, characterHolder.transform.position, Quaternion.identity, characterHolder.transform);
                character.Setup(characters[UnityEngine.Random.Range(0, characters.Count)]);
                character.Position = new Vector3(i, 0);
                party.Add(character);
            }

            Delay(0.5f, () => SetState(State.Select));
        }

        private void Update()
        {
            MoveParty();
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
                        Vector3 selectPoint = Input.mousePosition;
                        selectPoint.z = -Camera.main.transform.position.z;
                        Character foundCharacter = null;

                        party.ForEach((character) =>
                        {
                            if (characterSelected == null && character.ContainsPosition(Camera.main.ScreenToWorldPoint(selectPoint)))
                            {
                                foundCharacter = character;
                            }
                        });

                        if (foundCharacter != null)
                        {
                            SelectCharacter(foundCharacter);
                            SetState(State.Stay);
                        }
                    }
                    break;
                case State.Stay:
                    break;
                default:
                    state = State.Stay;
                    break;
            }
        }

        public void AddOnSacrificeListener(UnityAction listener)
        {
            onSacrifice.AddListener(listener);
        }

        public void AddOnMovesUpdatedListener(UnityAction listener)
        {
            onMovesUpdated.AddListener(listener);
        }

        public void AddOnMovesDepletedListener(UnityAction listener)
        {
            onMovesDepleted.AddListener(listener);
        }

        public void AddOnNoMovesLeftListener(UnityAction listener)
        {
            onNoMovesLeft.AddListener(listener);
        }

        public void EnableSelection()
        {
            SetState(State.Select);
        }

        public void Sacrifice(Character character)
        {
            party.Remove(character);
            character.Sacrifice();
            character.transform.parent = transform;
            onSacrifice?.Invoke();
        }

        public void UseMove()
        {
            if (characterSelected)
            {
                characterSelected.UseMove();
                onMovesUpdated?.Invoke();

                if (characterSelected.MovesLeft == 0)
                {
                    characterSelected = null;

                    if (party.Count > 0)
                    {
                        onMovesDepleted?.Invoke();
                    }
                    else
                    {
                        onNoMovesLeft?.Invoke();
                    }
                }
            }
            else
            {
                Debug.LogWarning("Move used without a character");
            }
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
            characterSelected = character;
            onMovesUpdated?.Invoke();
            Sacrifice(character);
        }

        private void MoveParty()
        {
            Vector3 moveDir = (targetPosition - characterHolder.transform.position);
            float moveSpeed = 10f;
            characterHolder.transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        private void MoveCharacters()
        {
            party.ForEach((Character character) =>
            {
                Vector3 moveDir = (character.TargetPosition - character.Position);
                character.transform.position += moveDir * moveSpeed * Time.deltaTime;
            });
        }

        public Vector3 TargetPosition {
            set {
                targetPosition = value - Vector3.right * (party.Count + 1);
            }
        }

        public int MovesLeft { 
            get {
                if (characterSelected)
                {
                    return characterSelected.MovesLeft;
                }

                return 0;
            }
        }
    }
}
