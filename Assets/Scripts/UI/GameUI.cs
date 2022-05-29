using TMPro;
using UnityEngine;

namespace Sacrimatch3.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI moveCounter = null;

        private GameController gameController = null;

        private void Awake()
        {
            gameController = FindObjectOfType<GameController>();
        }

        private void Start()
        {
            moveCounter.text = "";
            gameController.CharacterController.AddOnMovesUpdatedListener(UpdateMovesCounter);
        }

        private void UpdateMovesCounter()
        {
            moveCounter.text = gameController.CharacterController.MovesLeft.ToString();
        }
    }
}
