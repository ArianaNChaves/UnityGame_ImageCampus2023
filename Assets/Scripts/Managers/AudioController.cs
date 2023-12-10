using System;
using Managers;
using UnityEngine;

namespace Ui
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        

        public void ChangeAudio(string scene)
        {
            switch (scene)
            {
                case "Level_1":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayMusic("Game 2");
                    break;
                case "Level_2":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayMusic("Game 1");
                    break;
                case "Level_3":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayMusic("Game 2");
                    break;
                case "GameOver":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayEffect("Lose");
                    break;
                case "NextLevel":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayEffect("Win");
                    break;
                case "MainMenu":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayMusic("Menu");
                    break;
                case "Tutorial":
                    Debug.Log("Tutorial no hacer nada");
                    break;
                case "LevelSelection":
                    Debug.Log("LevelSelection no hacer nada");
                    break;
                default:
                    Debug.Log("Escena no valida - AudioController");
                    break;
            }
        }

    }
}
