using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyComponents enemyComponents;
        [SerializeField] private NavMeshAgent agent;

        private EnemyStats _stats;
        private EnemyStatsSystem _enemyStatsSystem;
        private EnemyAnimation _enemyAnimation;
        private void Start()
        {
            _enemyStatsSystem = enemyComponents.enemyStatsSystem;
            _enemyAnimation = enemyComponents.enemyAnimation;

            _stats = _enemyStatsSystem.Stats;

            agent.speed = _stats.speed;
            
            agent.SetDestination(AICore.GetPlayerPosition());
            AICore.OnPlayerChangePosition(TracePlayer);
        }
        
        private void Update()
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                _enemyAnimation.Move(agent.speed);
            }
            else
            {
                _enemyAnimation.Move(0);
            }
        }

        private void TracePlayer(Vector3 pos)
        {
            agent.SetDestination(pos);
        }
    }
}