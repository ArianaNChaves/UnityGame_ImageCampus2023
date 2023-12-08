using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneController : MonoBehaviour
    {
        public Action<float> CurrentSceneLoadingProgress;

        private List<string> _scenesLoaded = new List<string>();
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
            SceneManager.LoadScene(sceneName);
            _currentScene = sceneName;
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
    }
}
