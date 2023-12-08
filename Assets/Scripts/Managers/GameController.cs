using UnityEngine;

namespace Managers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
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
        
    }
}
