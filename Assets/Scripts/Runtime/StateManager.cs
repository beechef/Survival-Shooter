using UnityEngine;
using UnityEngine.Audio;

namespace Runtime
{
    public class StateManager : MonoBehaviour
    {
        private static StateManager _instance;
        public static StateManager Instance => _instance;

        [SerializeField] private GameOverMenu gameOverMenu;
        [SerializeField] private AudioMixerSnapshot normalAudioMixerSnapshot;
        [SerializeField] private AudioMixerSnapshot gameOverAudioMixerSnapshot;

        private void Awake()
        {
            normalAudioMixerSnapshot.TransitionTo(0.01f);
            if (_instance != null)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }

        public void GameOver()
        {
            float currentScore = EnemyManager.Instance.killedEnemyCount;
            if (currentScore > DataLoader.saveData.HighestScore)
            {
                DataLoader.saveData.HighestScore = currentScore;
                DataLoader.SaveData();
            }
            gameOverAudioMixerSnapshot.TransitionTo(0.01f);
            gameOverMenu.Open();
        }
    }
}