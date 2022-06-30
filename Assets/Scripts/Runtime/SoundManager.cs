using UnityEngine;
using UnityEngine.Audio;

namespace Runtime
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        public static SoundManager Instance => _instance;
        
        [SerializeField] private AudioMixer audioMixer;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(_instance);
            }
            else
            {
                _instance = this;
            }
            SetMusic();
            SetEffect();
        }


        public void SetMusic()
        {
            audioMixer.SetFloat("Music", DataLoader.saveData.MusicValue);
        }

        public void SetEffect()
        {
            audioMixer.SetFloat("Effect", DataLoader.saveData.EffectValue);
        }
    }
}