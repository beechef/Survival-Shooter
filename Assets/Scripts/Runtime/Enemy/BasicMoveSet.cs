using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Enemy
{
    [CreateAssetMenu(menuName = "MoveSet/Basic MoveSet", fileName = "New Basic MoveSet")]
    public class BasicMoveSet : MoveSet
    {
        public override void Run(EnemyComponents enemyComponents)
        {
            EnemyController enemyController = enemyComponents.enemyController;
            EnemyAnimation enemyAnimation = enemyComponents.enemyAnimation;
            EnemyCombat enemyCombat = enemyComponents.enemyCombat;
            EnemyStatsSystem enemyStatsSystem = enemyComponents.enemyStatsSystem;
            EnemyStats stats = enemyStatsSystem.Stats;
            NavMeshAgent agent = enemyController.agent;
            Transform attackPoint = enemyCombat.attackPoint;
            if (enemyCombat.isDeath) return;
            if (IsInAttackRange(attackPoint.position, stats.attackRange))
            {
                enemyCombat.Attack();
            }
            else
            {
                Trace(agent, enemyAnimation, AICore.GetPlayerPosition());
            }
        }

        private void Trace(NavMeshAgent agent, EnemyAnimation enemyAnimation, Vector3 playerPosition)
        {
            agent.SetDestination(playerPosition);
            enemyAnimation.Move(agent.remainingDistance > agent.stoppingDistance ? agent.speed : 0f);
        }

        private bool IsInAttackRange(Vector3 attackPoint, float attackRange) =>
            (attackPoint - AICore.GetPlayerPosition()).magnitude < attackRange;
    }
}