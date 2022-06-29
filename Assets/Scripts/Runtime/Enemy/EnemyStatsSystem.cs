using UnityEngine;

namespace Runtime.Enemy
{
    public class EnemyStatsSystem : MonoBehaviour
    {
        [SerializeField] private EnemyStats stats;
        public EnemyStats Stats => stats;

        private void Awake()
        {
            InitialStats();
        }

        public void InitialStats()
        {
            stats = Instantiate(stats);
            stats.health = stats.maxHealth;
        }
        
        public bool TakenDamage(float damage)
        {
            stats.health = Mathf.Clamp(stats.health - damage, 0f, stats.maxHealth);
            return stats.health == 0f;
        }
    }
}