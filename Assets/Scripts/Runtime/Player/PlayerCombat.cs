using System.Collections;
using UnityEngine;

namespace Runtime.Player
{
    public class PlayerCombat : CombatSystem
    {
        [SerializeField] private PlayerComponents playerComponents;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip fireAudio;
        [SerializeField] private ParticleSystem fireEffect;
        [SerializeField] private Transform attackPoint;

        private PlayerStatsSystem _playerStatsSystem;
        private PlayerStats _stats;
        private float _lastFire;
        private Camera _camera;
        private RaycastHit _raycastHit;

        private WaitForSeconds _lineFadedTime;


        protected override void Start()
        {
            base.Start();
            audioSource.loop = false;
            _camera = Camera.main;
            _playerStatsSystem = playerComponents.playerStatsSystem;
            _stats = _playerStatsSystem.Stats;

            _lineFadedTime = new WaitForSeconds(0.2f);
        }


        public override void Fire()
        {
            if (Time.time - _lastFire <= _stats.fireSpeed) return;
            Vector3 point;
            base.Fire();
            audioSource.Stop();
            audioSource.clip = fireAudio;
            audioSource.Play();
            lineRenderer.SetPosition(0, attackPoint.position);
            
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _raycastHit, _stats.fireRange))
            {
                point = _raycastHit.point;
            }
            else
            {
                point = _camera.transform.forward * _stats.fireRange;
            }

            lineRenderer.SetPosition(1, point);
            StartCoroutine(FireEffect());
            _lastFire = Time.time;
        }

        private IEnumerator FireEffect()
        {
            lineRenderer.enabled = true;
            fireEffect.Play();
            yield return _lineFadedTime;
            lineRenderer.enabled = false;
        }
    }
}