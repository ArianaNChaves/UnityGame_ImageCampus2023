using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class DeletThisScript : MonoBehaviour
    {
        [SerializeField] private Rigidbody playerController;
        [SerializeField] private Text showState;
        [SerializeField] private PlayerStateMachine playerStateMachine;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            switch (playerStateMachine.GetCurrentState())
            {
                case PlayerStateMachine.MovementState.Grounded:
                    showState.text = "Grounded";
                    break;
                case PlayerStateMachine.MovementState.Swinging:
                    showState.text = "Swinging";
                    break;
                case PlayerStateMachine.MovementState.Falling:
                    showState.text = "Falling";
                    break;
                default:
                    Debug.LogError("Null state");
                    break;
            }
        }
    }
}
