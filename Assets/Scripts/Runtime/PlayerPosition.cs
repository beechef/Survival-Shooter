using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "PlayerPosition")]
    public class PlayerPosition : ScriptableObject
    {
        public static readonly List<PlayerPosition> PlayerPositions = new List<PlayerPosition>();

        public Vector3 Position { get; private set; }
        private Action<Vector3> _changeActions;


        public PlayerPosition()
        {
            PlayerPositions.Add(this);
        }

        ~PlayerPosition()
        {
            PlayerPositions.Remove(this);
        }

        public void OnChange(Action<Vector3> action)
        {
            _changeActions += action;
        }

        public void SetPosition(Vector3 position)
        {
            if (position == Position) return;
            Position = position;
            _changeActions?.Invoke(Position);
        }
    }
}