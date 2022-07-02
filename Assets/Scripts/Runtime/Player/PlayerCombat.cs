using System;
using Cysharp.Threading.Tasks;
using Runtime.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private const int DirLeft = 0;
        private const int DirRight = 1;
        private const int DirDown = 2;
        private const int DirUp = 3;
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

        public float shakeValue;


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
            shakeValue = 0f;
        }

        private void PlayAudio(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        private void Update()
        {
            if (shakeValue > 0f)
            {
                shakeValue = Mathf.Clamp(shakeValue - (_stats.shakeStep * Time.deltaTime * 4), 0f, _stats.maxShake);
            }
        }

        public void Fire()
        {
            if (Time.time - _lastFire <= _stats.fireSpeed) return;
            PlayAudio(fireAudio);
            lineRenderer.SetPosition(0, attackPoint.position);
            Vector3 dir = CalcShake();

            if (Physics.Raycast(_camera.transform.position, dir, out _raycastHit,
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

        private Vector3 CalcShake()
        {
            shakeValue = Mathf.Clamp(shakeValue + _stats.shakeStep, 0f, _stats.maxShake);
            Vector3 dir = _camera.transform.forward;
            if (Random.Range(0, 100) > _stats.precision)
            {
                int dirShake = Random.Range(0, 4);
                switch (dirShake)
                {
                    case DirLeft:
                    {
                        dir -= _camera.transform.right * (shakeValue * 0.1f);
                        break;
                    }
                    case DirRight:
                    {
                        dir += _camera.transform.right * (shakeValue * 0.1f);
                        break;
                    }
                    case DirDown:
                    {
                        dir -= _camera.transform.up * (shakeValue * 0.1f);
                        break;
                    }
                    case DirUp:
                    {
                        dir += _camera.transform.up * (shakeValue * 0.1f);
                        break;
                    }
                }
            }

            return dir;
        }
    }
}