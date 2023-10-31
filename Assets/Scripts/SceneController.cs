using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Action<float> CurrentSceneLoadingProgress;

    private List<string> _scenesLoaded = new List<string>();
    private string _currentScene;
    
    private string _initialScene = "InitialScene";

    public string CurrentScene => _currentScene;
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

    public void LoadSceneAsync(string sceneName)
    {
        if (!_scenesLoaded.Contains(sceneName))
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }
        else
        {
            Debug.LogWarning("Escena " + sceneName + " ya esta cargada");
        }
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation loadingNewScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loadingNewScene.isDone)
        {
            CurrentSceneLoadingProgress?.Invoke(loadingNewScene.progress);
            yield return null;
        }

        _scenesLoaded.Add(sceneName);
        _currentScene = sceneName;
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
}
