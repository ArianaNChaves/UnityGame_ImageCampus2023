using System;
using UnityEngine;

namespace Player
{
    
    public class PlayerStateMachine : MonoBehaviour
    {
        private AirPlayerController _airPlayerController;
        private PlayerController _playerController;
        public enum MovementState
        {
            None,
            Swinging,
            Grounded,
        }
    
        private MovementState _currentState;

        private void Awake()
        {
            _airPlayerController = GetComponent<AirPlayerController>();
            _playerController = GetComponent<PlayerController>();
        }

        public void SetCurrentState(MovementState state)
        {
            if (state == _currentState)
            {
                Debug.Log("SetCurrentState - Ya esta en ese estado");
                return;
            }

            switch (state)
            {
                case MovementState.Grounded:
                    _playerController.ActivateController();
                    _airPlayerController.DeactivateController();
                    _currentState = state;
                    Debug.Log("SetCurrentState - Grounded");
                    break;
                case MovementState.Swinging:
                    _playerController.DeactivateController();
                    _airPlayerController.ActivateController();
                    _currentState = state;
                    Debug.Log("SetCurrentState - Swinging");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public MovementState GetCurrentState()
        {
            return _currentState;
        }
    }
}
