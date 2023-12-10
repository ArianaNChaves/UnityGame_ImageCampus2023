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
                    PlayerPrefs.SetInt("a", index);
                    PlayerPrefs.Save();
                    SceneController.Instance.LoadScene("NextLevel");
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
