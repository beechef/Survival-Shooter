using Runtime.Player;
using UnityEngine;

namespace Runtime.Enemy
{
    public class EnemyStatsSystem : MonoBehaviour
    {
        [SerializeField] private EnemyStats stats;
        public EnemyStats Stats => stats;
        
        
    }
}