using UnityEngine;
using UnityEngine.Events;

namespace Sacrimatch3
{
    public class GameController : MonoBehaviour
    {
        /* TODO
         * Présenter le groupe de personnages face à une porte
         * Séquence de sacrifice
         * Initialisation du match 3
         * Mise en scène
         * *phase de jeu*
         * Déclenchement du passage de porte
         * Déclenchement de séquence de sacrifice
         * Déclenchement de séquence de défaite
         * Déclenchement de séquence de victoire
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
