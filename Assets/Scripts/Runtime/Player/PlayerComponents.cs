using UnityEngine;

namespace Runtime.Player
{
    public class PlayerComponents : MonoBehaviour
    {
        public PlayerInput.PlayerInput playerInput;
        public PlayerAnimation playerAnimation;
        public PlayerStatsSystem playerStatsSystem;
        public PlayerCombat playerCombat;

    }
}