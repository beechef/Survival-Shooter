using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Runtime
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectSlider;

        private Input _playerInput;
        private InputAction _pause;

        private float _timeScale;
        public bool isPause;

        private void Start()
        {
            _playerInput = new Input();
            _pause = _playerInput.Default.Pause;
            _pause.Enable();
            _timeScale = Time.timeScale;
            Close();
            musicSlider.value = DataLoader.saveData.MusicValue;
            effectSlider.value = DataLoader.saveData.EffectValue;
        }

        private void Update()
        {
            if (_pause.WasPerformedThisFrame())
            {
                if (!isPause)
                {
                    Open();
                }
                else
                {
                    Close();
                }
            }
        }

        public void Open()
        {
            isPause = true;
            _timeScale = Time.timeScale;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Close()
        {
            isPause = false;
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            Time.timeScale = _timeScale;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ChangeMusic(float value)
        {
            DataLoader.saveData.MusicValue = value;
            SoundManager.Instance.SetMusic();
            DataLoader.SaveData();
        }

        public void ChangeEffect(float value)
        {
            DataLoader.saveData.EffectValue = value;
            SoundManager.Instance.SetEffect();
            DataLoader.SaveData();
        }
    }
}