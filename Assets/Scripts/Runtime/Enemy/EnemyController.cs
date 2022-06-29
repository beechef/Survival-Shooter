using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyComponents enemyComponents;
        [SerializeField] private MoveSet moveSet;
        [SerializeField] private new Collider collider;
        public NavMeshAgent agent;
        private EnemyStats _stats;
        private EnemyStatsSystem _enemyStatsSystem;

        private void Start()
        {
            Initial();
        }

        private void Update()
        {
            moveSet.Run(enemyComponents);
        }

        private void OnEnable()
        {
            Initial();
        }

        private void Initial()
        {
            _enemyStatsSystem = enemyComponents.enemyStatsSystem;
            enemyComponents.enemyCombat.isDeath = false;
            collider.enabled = true;

            _enemyStatsSystem.InitialStats();
            _stats = _enemyStatsSystem.Stats;

            agent.speed = _stats.speed;
        }
        
    }
}