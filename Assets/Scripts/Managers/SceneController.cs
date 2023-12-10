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
            _scenesLoaded.Add("LevelSelection");
            _scenesLoaded.Add("GameOver");
            _scenesLoaded.Add("Level_1");
            _scenesLoaded.Add("Level_2");
            _scenesLoaded.Add("Level_3");
            _scenesLoaded.Add("Tutorial");
            _scenesLoaded.Add("NextLevel");
        }
    
        public void LoadScene(string sceneName)
        {
            if (!_scenesLoaded.Contains(sceneName))
            {
                _scenesLoaded.Add(sceneName);
            }
            _currentScene = sceneName;
            SetLastestLevel(sceneName);
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
        public void SetLastestLevel(string level)
        {
            switch (level)
            {
                case "Level_1":
                    _lastestLevel = "Level_1";
                    break;
                case "Level_2":
                    _lastestLevel = "Level_2";
                    break;
                case "Level_3":
                    _lastestLevel = "Level_3";
                    break;
            }
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
                    _lastestLevel = "Level_2";
                    break;
                case "Level_2":
                    LoadScene("Level_3");
                    _lastestLevel = "Level_3";
                    break;
                case "Level_3":
                    LoadScene("MainMenu");
                    _lastestLevel = "Level_3";
                    break;
            }
        }

        public string GetCurrentScene()
        {
            return _currentScene;
        }
        public string GetLastestLevel()
        {
            return _lastestLevel;
        }
    }
}
