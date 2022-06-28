using UnityEngine;

namespace Runtime.Enemy
{
    [CreateAssetMenu(menuName = "Stats/Enemy Stats", fileName = "New Enemy Stats")]
    public class EnemyStats : ScriptableObject
    {
        public float speed;
        
    }
}