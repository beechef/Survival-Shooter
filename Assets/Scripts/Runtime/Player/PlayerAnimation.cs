using UnityEngine;

namespace Runtime.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private int _idle;
        private int _speed;
        private int _death;

        private void Start()
        {
            _idle = Animator.StringToHash("Idle");
            _speed = Animator.StringToHash("Speed");
            _death = Animator.StringToHash("Death");
        }

        public void Idle()
        {
            animator.SetTrigger(_idle);
        }

        public void Move(float speed)
        {
            animator.SetFloat(_speed, speed);
        }

        public void Death()
        {
            animator.SetTrigger(_death);
        }
    }
}