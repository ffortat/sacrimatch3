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

        [SerializeField]
        private Canvas popupCanvas = null;
        [SerializeField]
        private TextMeshProUGUI introduction = null;
        [SerializeField]
        private TextMeshProUGUI nextSacrifice = null;
        [SerializeField]
        private TextMeshProUGUI win = null;
        [SerializeField]
        private TextMeshProUGUI lose = null;

        private GameController gameController = null;

        private void Awake()
        {
            gameController = FindObjectOfType<GameController>();
        }

        private void Start()
        {
            moveCounter.text = "";
            gameController.AddOnPresentPartyListener(PopupPresentParty);
            gameController.CharacterController.AddOnSacrificeListener(HideCharacterSelection);
            gameController.CharacterController.AddOnMovesUpdatedListener(UpdateMovesCounter);
            gameController.AddOnWinListener(ShowPopupWin);
            gameController.AddOnLoseListener(ShowPopupLose);
        }

        private void Update()
        {
            if (popupCanvas.gameObject.activeInHierarchy)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    HidePopupCanvas();
                    gameController.Play();
                }
            }
        }

        private void PopupPresentParty()
        {
            ShowCharacterSelection();
            popupCanvas.gameObject.SetActive(true);

            if (!introduction.gameObject.activeInHierarchy)
            {
                nextSacrifice.gameObject.SetActive(true);
            }
        }

        private void ShowCharacterSelection()
        {
            characterSelection.gameObject.SetActive(true);
        }

        private void HideCharacterSelection()
        {
            characterSelection.gameObject.SetActive(false);
        }

        private void ShowPopupWin()
        {
            popupCanvas.gameObject.SetActive(true);
            win.gameObject.SetActive(true);
        }

        private void ShowPopupLose()
        {
            popupCanvas.gameObject.SetActive(true);
            lose.gameObject.SetActive(true);
        }

        private void HidePopupCanvas()
        {
            popupCanvas.gameObject.SetActive(false);
            introduction.gameObject.SetActive(false);
            nextSacrifice.gameObject.SetActive(false);
            win.gameObject.SetActive(false);
            lose.gameObject.SetActive(false);
        }

        private void UpdateMovesCounter()
        {
            moveCounter.text = gameController.CharacterController.MovesLeft.ToString();
        }
    }
}
