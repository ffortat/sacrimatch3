using UnityEngine;

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
