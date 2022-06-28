using System;
using UnityEngine;

namespace Runtime.Enemy
{
    public static class AICore
    {
        public static Vector3 GetPlayerPosition() => PlayerPosition.playerPositions[0].Position;

        public static void OnPlayerChangePosition(Action<Vector3> action)
        {
            PlayerPosition.playerPositions[0].OnChange(action);
        }

    }
}