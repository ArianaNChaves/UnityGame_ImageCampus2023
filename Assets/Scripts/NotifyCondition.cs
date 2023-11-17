using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyCondition : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool condition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (condition)
        {
            GameController.Instance.WinCondition();
        }
        else
        {
            GameController.Instance.LoseCondition();
        }
        
    }
}
