using UnityEngine;

namespace Runtime.Player
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform gun;
        [SerializeField] private new Transform camera;
        [SerializeField] private float sensitivity;
        [SerializeField] private PlayerComponents playerComponents;
        private PlayerInput.PlayerInput _playerInput;

        private float _xRotation = 0f;
        private float _yRotation = 0f;

        private void Start()
        {
            _xRotation = player.localRotation.x;
            _yRotation = player.localRotation.y;

            _playerInput = playerComponents.playerInput;
        }

        private void Update()
        {
            Vector2 mouseValue = _playerInput.GetMouseValue();
            _xRotation -= mouseValue.y * sensitivity * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, -90, 90);
            _yRotation += mouseValue.x * sensitivity * Time.deltaTime;
            player.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
            camera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            // gun.LookAt(camera.forward * 50);
            gun.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
    }
}