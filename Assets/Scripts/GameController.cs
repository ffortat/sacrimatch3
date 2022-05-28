using UnityEngine;
using UnityEngine.Events;

namespace Sacrimatch3
{
    public class GameController : MonoBehaviour
    {
        /* TODO
         * Pr�senter le groupe de personnages face � une porte
         * S�quence de sacrifice
         * Initialisation du match 3
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

        private void Awake()
        {
            characterController.AddOnSacrificeListener(OnSacrifice);
            characterController.Setup(doorController);
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
    }
}
