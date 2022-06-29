using UnityEngine;

namespace Runtime
{
    public class ParticlesEffect : MonoBehaviour
    {
        [SerializeField] private new ParticleSystem particleSystem;

        private void OnEnable()
        {
            particleSystem.Play();
        }

        private void OnDisable()
        {
            particleSystem.Stop();
        }
    }
}