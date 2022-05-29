using UnityEngine;
using UnityEngine.UI;

namespace Sacrimatch3.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private Button exitButton = null;

        private GameLoader gameLoader = null;

        private void Start()
        {
            gameLoader = FindObjectOfType<GameLoader>();

#if UNITY_WEBGL
            exitButton.gameObject.SetActive(false);
#endif
        }

        public void Play()
        {
            gameLoader.StartGame();
        }

        public void Credits()
        {
            // TODO display credits
        }

        public void Exit()
        {
            gameLoader.ExitGame();
        }
    }
}
