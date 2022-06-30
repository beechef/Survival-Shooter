using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Runtime.Enemy;
using UnityEngine;

namespace Runtime.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private PlayerComponents playerComponents;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip fireAudio;
        [SerializeField] private AudioClip hitAudio;
        [SerializeField] private AudioClip deathAudio;
        [SerializeField] private ParticleSystem fireEffect;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask enemyMask;
        private PlayerStatsSystem _playerStatsSystem;
        private PlayerAnimation _playerAnimation;
        private PlayerController _playerController;
        private PlayerStats _stats;
        private float _lastFire;
        private Camera _camera;
        private RaycastHit _raycastHit;
        private Vector3 _point;


        private WaitForSeconds _lineFadedTime;

        private bool _isDeath = false;

        protected void Start()
        {
            audioSource.loop = false;
            _camera = Camera.main;
            _playerAnimation = playerComponents.playerAnimation;
            _playerStatsSystem = playerComponents.playerStatsSystem;
            _playerController = playerComponents.playerController;
            _stats = _playerStatsSystem.Stats;

            _lineFadedTime = new WaitForSeconds(0.1f);
        }

        private void PlayAudio(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void Fire()
        {
            if (Time.time - _lastFire <= _stats.fireSpeed) return;
            PlayAudio(fireAudio);
            lineRenderer.SetPosition(0, attackPoint.position);

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _raycastHit,
                _stats.fireRange, enemyMask))
            {
                _point = _raycastHit.point;
                if (ComponentsDictionary.EnemyComponents.TryGetValue(_raycastHit.collider.gameObject,
                    out EnemyComponents enemyComponents))
                {
                    enemyComponents.enemyCombat.Hit(_stats.attack);
                }
            }
            else
            {
                _point = _camera.transform.forward * _stats.fireRange;
            }

            lineRenderer.SetPosition(1, _point);
            FireEffect().Forget();
            _lastFire = Time.time;
        }

        public void Hit(float damage)
        {
            if (_isDeath) return;
            if (_playerStatsSystem.TakenDamage(damage))
            {
                _playerAnimation.Death();
                PlayAudio(deathAudio);
                _isDeath = true;
                _playerController.isDeath = true;
                StateManager.Instance.GameOver();
            }
            else
            {
                PlayAudio(hitAudio);
            }
        }

        private async UniTask FireEffect()
        {
            lineRenderer.enabled = true;
            fireEffect.Play();
            var effect = await EffectPool.Instance.GetEffect("HitParticles");
            effect.transform.position = _point;
            await UniTask.Delay(TimeSpan.FromSeconds(.01f));
            lineRenderer.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            EffectPool.Instance.Return(effect);
        }
    }
}