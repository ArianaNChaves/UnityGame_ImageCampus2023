using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Ui
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenu;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
        }

        public void Continue()
        {
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        public void Quit()
        {
            Time.timeScale = 1f;
            SceneController.Instance.LoadScene("MainMenu");
            Debug.Log("Go to main menu");
        }
    }
}
