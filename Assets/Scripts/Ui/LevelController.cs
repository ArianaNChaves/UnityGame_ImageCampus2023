using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Ui
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Button[] levelButtons;
        [SerializeField] private Image lockPrefab;
        [SerializeField] private Image donePrefab;

        private int _highestLevel;
        void Awake()
        {
            _highestLevel = PlayerPrefs.GetInt("Highest_level", 1);

            for (int i = 0; i < levelButtons.Length; i++)
            {
                int levelNum = i + 1;
                
                if (levelNum > _highestLevel)
                {
                    levelButtons[i].interactable = false;
                    levelButtons[i].GetComponent<Image>().sprite = lockPrefab.sprite;
                    levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
                else
                {
                    levelButtons[i].interactable = true;
                    levelButtons[i].GetComponent<Image>().sprite = donePrefab.sprite;
                    levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "" + levelNum;
                }
            }
        }

        public void LoadLevel(int levelNum)
        {
            SceneController.Instance.LoadScene("Level_" + levelNum);
            SceneController.Instance.LastestLevel("Level_" + levelNum);
        }

        public void BackToMenu()
        {
            SceneController.Instance.LoadScene("MainMenu");
        }
    }
}
