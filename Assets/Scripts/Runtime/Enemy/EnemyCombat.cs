using Runtime.Player;
using UnityEngine;

namespace Runtime.Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private EnemyComponents enemyComponents;
        [SerializeField] private  Collider enemyCollider;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip hitAudio;
        [SerializeField] private AudioClip deathAudio;
        public Transform attackPoint;

        private EnemyStats _stats;
        private EnemyStatsSystem _enemyStatsSystem;
        private EnemyAnimation _enemyAnimation;

        [SerializeField] private LayerMask playerMask;

        private float _lastAttack;
        public bool isDeath = false;

        private void Start()
        {
            _enemyStatsSystem = enemyComponents.enemyStatsSystem;
            _enemyAnimation = enemyComponents.enemyAnimation;
            _stats = _enemyStatsSystem.Stats;
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.Stop();
        }

        private void PlayAudio(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void Attack()
        {
            if (Time.time - _lastAttack > _stats.attackSpeed)
            {
                _lastAttack = Time.time;
                Collider[] colliders = Physics.OverlapSphere(attackPoint.position, _stats.attackRange, playerMask);
                if (colliders == null) return;
                foreach (var collider in colliders)
                {
                    if (ComponentsDictionary.PlayerComponents.TryGetValue(collider.gameObject,
                        out PlayerComponents playerComponents))
                    {
                        playerComponents.playerCombat.Hit(_stats.attack);
                    }
                }
            }
        }

        public void Hit(float damage)
        {
            if (isDeath) return;
            if (_enemyStatsSystem.TakenDamage(damage))
            {
                _enemyAnimation.Death();
                PlayAudio(deathAudio);
                isDeath = true;
                enemyCollider.enabled = false;
            }
            else
            {
                PlayAudio(hitAudio);
            }
        }
    }
}