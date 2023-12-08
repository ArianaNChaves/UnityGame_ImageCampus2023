using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private bool isWin;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && isWin)
            {
                    index++;
                    PlayerPrefs.SetInt("Highest_level", index);
                    PlayerPrefs.Save();
                    SceneController.Instance.LoadScene("MainMenu");
            }

            
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player") && !isWin)
            {
                Debug.Log("You lose!");
                SceneController.Instance.LoadScene("GameOver");
            }
        }
    }
}
