using UnityEngine;

namespace Runtime.Player
{
    [CreateAssetMenu(fileName = "New Player Stats", menuName = "Stats/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        public float moveSpeed;
        public float sprintSpeed;
        public float jumpHeight;
        public float attack;
        public float fireSpeed;
        public float fireRange;
        public float health;
        public float maxHealth;
        [Range(0, 100)] public float precision;
        [Range(0, 10)] public float maxShake;
        [Range(0, 10)] public float shakeStep;
    }
}