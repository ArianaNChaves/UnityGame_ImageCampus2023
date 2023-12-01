using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyCondition : MonoBehaviour
{
    [Header("true for win")]
    [SerializeField] private bool condition;
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
