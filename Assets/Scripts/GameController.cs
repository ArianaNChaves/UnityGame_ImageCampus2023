using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinCondition()
    {
        Debug.Log("Win!");
        SceneController.Instance.LoadScene("MainMenu");
    }

    public void LoseCondition()
    {
        Debug.Log("You lose!");
        SceneController.Instance.LoadScene(SceneController.Instance.CurrentScene());
    }
}
