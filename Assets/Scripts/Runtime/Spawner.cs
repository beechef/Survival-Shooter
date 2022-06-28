using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Runtime
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<AssetReference> enemies;
        [SerializeField] private float minDelayTime;
        [SerializeField] private float maxDelayTime;
        public List<Vector3> positions;

        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            int randomEnemy = Random.Range(0, enemies.Count);
            enemies[randomEnemy].InstantiateAsync().Completed += (go) =>
            {
                int randomPos = Random.Range(0, positions.Count);
                go.Result.transform.position = positions[randomPos];
            };
            yield return new WaitForSeconds(Random.Range(maxDelayTime, maxDelayTime));
            StartCoroutine(SpawnEnemy());
        }
    }
}