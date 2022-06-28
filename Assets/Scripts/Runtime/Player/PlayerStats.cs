using UnityEngine;

namespace Runtime.Player
{
    [CreateAssetMenu(fileName = "New Player Stats", menuName = "Stats/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        public float moveSpeed;
        public float sprintSpeed;
        public float jumpHeight;
        public float fireSpeed;
        public float fireRange;
    }
}