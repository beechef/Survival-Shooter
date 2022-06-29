using UnityEngine;

namespace Runtime.Enemy
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        public void Death()
        {
            Spawner.Instance.Return(parent);
        }
    }
}