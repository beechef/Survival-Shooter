using System.Collections.Generic;
using UnityEditor;
using Runtime;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            Spawner spawner = target as Spawner;
            List<Vector3> positions = spawner.positions ?? new List<Vector3>();

            for (int i = 0; i < positions.Count; i++)
            {
                Vector3 pos = positions[i];
                pos = Handles.FreeMoveHandle(pos, Quaternion.identity, .5f, Vector3.one, Handles.SphereHandleCap);
                pos = Handles.PositionHandle(pos, Quaternion.identity);
                positions[i] = pos;
            }
        }
    }
}