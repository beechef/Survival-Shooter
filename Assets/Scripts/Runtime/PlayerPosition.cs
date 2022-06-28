using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "PlayerPosition")]
    public class PlayerPosition : ScriptableObject
    {
        public static readonly List<PlayerPosition> playerPositions = new List<PlayerPosition>();

        public Vector3 Position { get; private set; }
        private Action<Vector3> _changeActions;


        public PlayerPosition()
        {
            playerPositions.Add(this);
        }

        ~PlayerPosition()
        {
            playerPositions.Remove(this);
        }

        public void OnChange(Action<Vector3> action)
        {
            _changeActions += action;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
            _changeActions?.Invoke(Position);
        }
    }
}