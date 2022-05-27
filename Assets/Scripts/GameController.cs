using UnityEngine;

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
        private CharacterController characterController = null;
        [SerializeField]
        private DoorController doorController = null;
        [SerializeField]
        private Match3 match3 = null;

        private void Awake()
        {
            
        }

        private void Start()
        {

        }
    }
}
