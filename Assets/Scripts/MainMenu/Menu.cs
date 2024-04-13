using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class Menu : MonoBehaviour
    {
        public DontDestroy dontDestroy;
        public void Play()
        {
            Debug.Log("Play");
            SceneManager.LoadScene("Main");
            dontDestroy.MuteMusic();
        }

        public void Settings()
        {
            Debug.Log("Set");
            SceneManager.LoadScene("TestSettingsMenu");

        }

        public void Exit()
        {
            Debug.Log("Ex");
            Application.Quit();

        }
    }
}
