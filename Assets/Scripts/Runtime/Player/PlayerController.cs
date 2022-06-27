using UnityEngine;

namespace Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerComponents playerComponents;
        [SerializeField] private CharacterController characterController;

        public float gravity;

        private PlayerInput.PlayerInput _playerInput;
        private PlayerAnimation _playerAnimation;
        private PlayerStatsSystem _playerStatsSystem;
        private PlayerCombat _playerCombat;

        private PlayerStats _stats;
        
        private float _speed;
        private Vector3 _moveDir;
        private float _yVelocity;

        private void Start()
        {
            _playerInput = playerComponents.playerInput;
            _playerAnimation = playerComponents.playerAnimation;
            _playerStatsSystem = playerComponents.playerStatsSystem;
            _playerCombat = playerComponents.playerCombat;

            _stats = _playerStatsSystem.Stats;
        }

        private void Update()
        {
            Reset();
            Move();
            Jump();
            Fire();
            GravitySimulate();

            _moveDir.y = _yVelocity;

            characterController.Move(_moveDir * Time.deltaTime);

            _playerAnimation.Move(_speed);
        }

        private void Reset()
        {
            _speed = 0f;
            _moveDir = Vector3.zero;
        }

        private void Move()
        {
            if (!_playerInput.IsMove()) return;
            _speed = _stats.moveSpeed;
            if (_playerInput.IsSprint())
            {
                _speed = _stats.sprintSpeed;
            }

            Vector2 moveValue = _playerInput.GetMoveValue() * _speed;
            _moveDir = transform.forward * moveValue.y + transform.right * moveValue.x;
        }

        private void Jump()
        {
            if (!_playerInput.IsJump() || !characterController.isGrounded) return;
            _yVelocity = Mathf.Sqrt(-2 * _stats.jumpHeight * gravity);
        }

        private void Fire()
        {
            if (!_playerInput.IsFire()) return;
            _playerCombat.Fire();
        }

        private void GravitySimulate()
        {
            if (characterController.isGrounded) return;
            _yVelocity += (gravity * Time.deltaTime);
        }
    }
}