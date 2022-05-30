using TMPro;
using UnityEngine;

namespace Sacrimatch3.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI moveCounter = null;
        [SerializeField]
        private Canvas characterSelection = null;

        private GameController gameController = null;

        private void Awake()
        {
            gameController = FindObjectOfType<GameController>();
        }

        private void Start()
        {
            moveCounter.text = "";
            gameController.AddOnPresentPartyListener(ShowCharacterSelection);
            gameController.CharacterController.AddOnSacrificeListener(HideCharacterSelection);
            gameController.CharacterController.AddOnMovesUpdatedListener(UpdateMovesCounter);
        }

        private void ShowCharacterSelection()
        {
            characterSelection.gameObject.SetActive(true);
        }

        private void HideCharacterSelection()
        {
            characterSelection.gameObject.SetActive(false);
        }

        private void UpdateMovesCounter()
        {
            moveCounter.text = gameController.CharacterController.MovesLeft.ToString();
        }
    }
}
