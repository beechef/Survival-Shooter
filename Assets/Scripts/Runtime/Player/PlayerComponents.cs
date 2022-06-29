using UnityEngine;

namespace Runtime.Player
{
    public class PlayerComponents : MonoBehaviour
    {
        public PlayerInput.PlayerInput playerInput;
        public PlayerAnimation playerAnimation;
        public PlayerController playerController;
        public PlayerStatsSystem playerStatsSystem;
        public PlayerCombat playerCombat;
        

        private void Start()
        {
            ComponentsDictionary.PlayerComponents.Add(gameObject, this);
        }

        private void OnDestroy()
        {
            ComponentsDictionary.PlayerComponents.Remove(gameObject);
        }
    }
}