using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.PlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        private Input _input;
        private InputAction _moveAction;
        private InputAction _sprintAction;
        private InputAction _jumpAction;
        private InputAction _fireAction;
        private InputAction _mouseLookAction;

        private void Start()
        {
            _input = new Input();
            _moveAction = _input.Default.Move;
            _sprintAction = _input.Default.Sprint;
            _jumpAction = _input.Default.Jump;
            _fireAction = _input.Default.Fire;
            _mouseLookAction = _input.Default.MouseLook;

            _moveAction.Enable();
            _sprintAction.Enable();
            _jumpAction.Enable();
            _fireAction.Enable();
            _mouseLookAction.Enable();
        }

        public bool IsMove() => _moveAction.IsPressed();

        public bool IsSprint() => _sprintAction.IsPressed();

        public bool IsJump() => _jumpAction.IsPressed();

        public bool IsFire() => _fireAction.IsPressed();

        public Vector2 GetMoveValue() => _moveAction.ReadValue<Vector2>();

        public Vector2 GetMouseValue() => _mouseLookAction.ReadValue<Vector2>();
    }
}