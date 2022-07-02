using System.Collections.Generic;
using Runtime.Enemy;
using Runtime.Player;
using UnityEngine;

namespace Runtime
{
    public static class ComponentsDictionary
    {
        public static readonly Dictionary<GameObject, PlayerComponents> PlayerComponents =
            new Dictionary<GameObject, PlayerComponents>();

        public static readonly Dictionary<GameObject, EnemyComponents> EnemyComponents =
            new Dictionary<GameObject, EnemyComponents>();
    }
}