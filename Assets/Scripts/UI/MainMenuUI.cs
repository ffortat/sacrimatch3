using UnityEngine;

namespace Sacrimatch3.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private GameLoader gameLoader = null;

        private void Start()
        {
            gameLoader = FindObjectOfType<GameLoader>();
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
