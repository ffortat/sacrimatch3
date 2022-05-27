using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sacrimatch3
{
    public class GameLoader : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectsOfType<GameLoader>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene("GameLevel");
        }

        public void EndGame()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
