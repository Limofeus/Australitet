using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class Menu : MonoBehaviour
    {
        public void Play()
        {
            Debug.Log("Play");
            SceneManager.LoadScene("Main");
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
