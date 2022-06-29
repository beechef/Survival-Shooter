using UnityEngine;

namespace Runtime.Enemy
{
    public class EnemyComponents : MonoBehaviour
    {
        public EnemyStatsSystem enemyStatsSystem;
        public EnemyAnimation enemyAnimation;
        public EnemyController enemyController;
        public EnemyCombat enemyCombat;
        
        private void Start()
        {
            ComponentsDictionary.EnemyComponents.Add(gameObject, this);
        }

        private void OnDestroy()
        {
            ComponentsDictionary.EnemyComponents.Remove(gameObject);
        }
    }
}