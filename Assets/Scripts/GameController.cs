using UnityEngine;
using UnityEngine.Events;

namespace Sacrimatch3
{
    public class GameController : MonoBehaviour
    {
        /* TODO
         * Pr�senter le groupe de personnages face � une porte
         * Mise en sc�ne
         * *phase de jeu*
         * D�clenchement du passage de porte
         * D�clenchement de s�quence de sacrifice
         * D�clenchement de s�quence de d�faite
         * D�clenchement de s�quence de victoire
         */
        [SerializeField]
        private CameraController cameraController = null;

        [SerializeField]
        private CharacterController characterController = null;
        [SerializeField]
        private DoorController doorController = null;
        [SerializeField]
        private Match3 match3 = null;

        private UnityEvent onPresentParty = new UnityEvent();

        private GameLoader gameLoader = null;

        private void Awake()
        {
            gameLoader = FindObjectOfType<GameLoader>();

            characterController.AddOnSacrificeListener(OnSacrifice);
            characterController.Setup(doorController);

            match3.AddOnPuzzlePieceClearedListener(doorController.ClearDoorPiece);
            match3.AddOnGemsSwappedListener(characterController.UseMove);

            doorController.AddOnDoorOpenedListener(OpenDoor);
            doorController.AddOnAllDoorsOpenedListener(LevelComplete);
        }

        private void Start()
        {
            PresentParty();
        }

        public void AddOnPresentPartyListener(UnityAction listener)
        {
            onPresentParty.AddListener(listener);
        }

        private void PresentParty()
        {
            ZoomToParty();
            onPresentParty?.Invoke();
        }

        private void OnSacrifice()
        {
            Unzoom();
            StartMatch3();
        }

        private void OpenDoor(Door door)
        {
            characterController.TargetPosition = door.transform.position;
            match3.Reset();
            StartMatch3();
        }

        private void LevelComplete()
        {
            characterController.TargetPosition = CameraController.TopRight + new Vector3(10, -2);

            gameLoader.EndGame();
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
            match3.Activate(doorController.CurrentDoor.Tiles);
        }
    }
}
