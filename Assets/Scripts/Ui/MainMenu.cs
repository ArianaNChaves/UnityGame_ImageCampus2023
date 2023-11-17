using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Play(string sceneName)
        {
            SceneController.Instance.LoadScene(sceneName);
        }

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
