using UnityEngine;

namespace Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerComponents playerComponents;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerPosition playerPosition;
        [SerializeField] private PauseMenu pauseMenu;

        public float gravity;

        private PlayerInput.PlayerInput _playerInput;
        private PlayerAnimation _playerAnimation;
        private PlayerStatsSystem _playerStatsSystem;
        private PlayerCombat _playerCombat;

        private PlayerStats _stats;

        private float _speed;
        private Vector3 _moveDir;
        private float _yVelocity;
        private bool _isUpdatePosition;

        public bool isDeath = false;

        private void Start()
        {
            _playerInput = playerComponents.playerInput;
            _playerAnimation = playerComponents.playerAnimation;
            _playerStatsSystem = playerComponents.playerStatsSystem;
            _playerCombat = playerComponents.playerCombat;

            _stats = _playerStatsSystem.Stats;
            playerPosition.SetPosition(transform.position);
        }

        private void Update()
        {
            if (pauseMenu.isPause) return;
            if (isDeath) return;
            Reset();
            Move();
            Jump();
            Fire();
            GravitySimulate();

            _moveDir.y = _yVelocity;

            characterController.Move(_moveDir * Time.deltaTime);

            _playerAnimation.Move(_speed);

            if (_isUpdatePosition)
            {
                playerPosition.SetPosition(transform.position);
            }
        }

        private void Reset()
        {
            _isUpdatePosition = false;
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
            _isUpdatePosition = true;
        }

        private bool IsGround()
        {
            bool isGround = characterController.isGrounded;
            if (!isGround)
            {
                _isUpdatePosition = true;
            }

            return isGround;
        }

        private void Jump()
        {
            if (!_playerInput.IsJump() || !characterController.isGrounded) return;
            _yVelocity = Mathf.Sqrt(-2 * _stats.jumpHeight * gravity);
            _isUpdatePosition = true;
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
            _isUpdatePosition = true;
        }
    }
}