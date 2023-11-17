using UnityEngine;

namespace Player
{
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraPosition;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = cameraPosition.position;
        }
    }
}
