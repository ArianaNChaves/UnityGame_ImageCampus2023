using System;
using System.Collections;
using System.Collections.Generic;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneController : MonoBehaviour
    {
        private List<string> _scenesLoaded;
        private string _currentScene;
        private string _lastestLevel;
    
        private string _initialScene = "MainMenu";
        public static SceneController Instance { get; private set; }

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

        private void Start()
        {
            _scenesLoaded = new List<string>();       
            _currentScene = _initialScene;
            _scenesLoaded.Add(_currentScene);
        }
    
        public void LoadScene(string sceneName)
        {
            if (!_scenesLoaded.Contains(sceneName))
            {
                _scenesLoaded.Add(sceneName);
            }
         /*   switch (sceneName)
            {
                case "Level_1":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayMusic("Game 2");
                    _lastestLevel = "Level_1";
                    break;
                case "Level_2":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayMusic("Game 1");
                    _lastestLevel = "Level_2";
                    break;
                case "Level_3":
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.PlayMusic("Game 2");
                    _lastestLevel = "Level_3";
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
            }*/
            _currentScene = sceneName;
            AudioController.Instance.ChangeAudio(sceneName);
            SceneManager.LoadScene(sceneName);
            Debug.Log("Se cargo la escena: " + sceneName);
        }
        

        public void UnloadScene(string sceneName)
        {
            if (_scenesLoaded.Contains(sceneName))
            {
                SceneManager.UnloadSceneAsync(sceneName);
                _scenesLoaded.Remove(sceneName);
            }
            else
            {
                Debug.LogError("Escena no encontrada: " + sceneName);
            }
        }

        public void LoadCurrentScene()
        {
            LoadScene(_currentScene);
        }

        public void LastestLevel(string level)
        {
            _lastestLevel = level;
        }

        public void LoadLastestLevel()
        {
            LoadScene(_lastestLevel);
        }

        public void LoadNextLevel()
        {
            switch (_lastestLevel)
            {
                case "Level_1":
                    LoadScene("Level_2");
                    break;
                case "Level_2":
                    LoadScene("Level_3");
                    break;
                case "Level_3":
                    LoadScene("MainMenu");
                    break;
            }
        }

        public string GetCurrentScene()
        {
            return _currentScene;
        }
    }
}
