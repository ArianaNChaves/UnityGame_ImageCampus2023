using Managers;
using UnityEngine;

namespace Utilities
{
    public class NotifyLoaderScene : MonoBehaviour
    {
        public void LoadScene(string scene)
        {
            SceneController.Instance.LoadScene(scene);
        }

        public void LoadLastestLevel()
        {
            SceneController.Instance.LoadLastestLevel();
        }

        public void LoadNextScene()
        {
            SceneController.Instance.LoadNextLevel();
        }
    }
}