using System;
using UnityEngine;
using UnityEngine.Events;

namespace Sacrimatch3
{
    public class GameController : MonoBehaviour
    {
        /* TODO
         * Déclenchement de séquence de défaite
         * Déclenchement de séquence de victoire
         */
        public enum State
        {
            Busy,
            Popup,
            Play,
            Pause,
        }

        [SerializeField]
        private CameraController cameraController = null;

        [SerializeField]
        private CharacterController characterController = null;
        [SerializeField]
        private DoorController doorController = null;
        [SerializeField]
        private Match3 match3 = null;

        private bool isLost = false;
        private bool isWon = false;
        private float busyTimer = 0f;
        private State state = State.Pause;
        private Action busyCallback;
        private UnityEvent onPresentParty = new UnityEvent();
        private UnityEvent onWin = new UnityEvent();
        private UnityEvent onLose = new UnityEvent();

        private GameLoader gameLoader = null;

        private void Awake()
        {
            gameLoader = FindObjectOfType<GameLoader>();

            characterController.AddOnSacrificeListener(OnSacrifice);
            characterController.AddOnMovesDepletedListener(PresentParty);
            characterController.AddOnNoMovesLeftListener(LevelLost);

            match3.AddOnPuzzlePieceClearedListener(doorController.ClearDoorPiece);
            match3.AddOnGemsSwappedListener(characterController.UseMove);

            doorController.AddOnDoorOpenedListener(OpenDoor);
            doorController.AddOnAllDoorsOpenedListener(LevelComplete);
        }

        private void Start()
        {
            PresentParty();
        }

        private void Update()
        {
            switch(state)
            {
                case State.Busy:
                    busyTimer -= Time.deltaTime;
                    if (busyTimer <= 0f)
                    {
                        busyCallback();
                    }
                    break;
                case State.Popup:
                    break;
                case State.Play:
                    if (isLost || isWon)
                    {
                        gameLoader.EndGame();
                    }
                    break;
                case State.Pause:
                    break;
                default:
                    SetState(State.Pause);
                    break;
            }
        }

        public void AddOnPresentPartyListener(UnityAction listener)
        {
            onPresentParty.AddListener(listener);
        }

        public void AddOnWinListener(UnityAction listener)
        {
            onWin.AddListener(listener);
        }

        public void AddOnLoseListener(UnityAction listener)
        {
            onLose.AddListener(listener);
        }

        public void Play()
        {
            SetState(State.Play);
        }

        public void Popup()
        {
            SetState(State.Popup);
        }

        private void PresentParty()
        {
            match3.Pause();
            ZoomToParty();
            characterController.EnableSelection();
            onPresentParty?.Invoke();
        }

        private void OnSacrifice()
        {
            Unzoom();
            StartMatch3();
        }

        private void OpenDoor(Door door)
        {
            characterController.TargetPosition = new Vector3(door.transform.position.x, door.transform.position.y);
            match3.Reset();
            StartMatch3();
        }

        private void LevelComplete()
        {
            characterController.TargetPosition = CameraController.TopRight + new Vector3(10, -2);

            onWin?.Invoke();
            isWon = true;
        }

        private void LevelLost()
        {
            onLose?.Invoke();
            isLost = true;
        }

        private void ZoomToParty()
        {
            // TODO add parameters to zoom to correct area
            cameraController.Zoom();
        }

        private void Unzoom()
        {
            cameraController.Unzoom();
        }

        private void StartMatch3()
        {
            if (!match3.IsRunning)
            {
                match3.Activate(doorController.CurrentDoor.Tiles);
            }
            
            match3.Resume();
        }

        private void SetState(State state)
        {
            this.state = state;
        }

        private void Delay(float delay, Action callback)
        {
            SetState(State.Busy);
            busyTimer = delay;
            busyCallback = callback;
        }

        public CharacterController CharacterController { get => characterController; }
    }
}
