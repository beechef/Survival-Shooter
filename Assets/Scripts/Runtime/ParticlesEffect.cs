using UnityEngine;

namespace Runtime
{
    public class ParticlesEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;

        private void OnEnable()
        {
            particle.Play();
        }

        private void OnDisable()
        {
            particle.Stop();
        }
    }
}