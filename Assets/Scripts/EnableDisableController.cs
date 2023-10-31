using System;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableController : MonoBehaviour
{
    [Header("Objects to Enable")]
    [SerializeField] private List<GameObject> objectsToEnable;
    [Header("Objects to Disable")]
    [SerializeField] private List<GameObject> objectsToDisable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var obj in objectsToDisable)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            }
            foreach (var obj in objectsToEnable)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                }
            }  
        }
    }
}