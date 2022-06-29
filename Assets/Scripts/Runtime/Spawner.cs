using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets.Pooling;
using Random = UnityEngine.Random;

namespace Runtime
{
    public class Spawner : MonoBehaviour
    {
        private static Spawner _instance;
        public static Spawner Instance => _instance;
        [SerializeField] private AddressableGameObjectSpawner spawner;
        [SerializeField] private float minDelayTime;
        [SerializeField] private float maxDelayTime;
        public List<Vector3> positions;
        private readonly string[] _enemies = {"Zombear", "Zombunny", "Hellephant"};
        private float _lastSpawn;
        private float _delayTime;
        private bool _isDone = false;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }

        private async void Start()
        {
            await spawner.InitializeAsync();
            _isDone = true;
        }

        private void Update()
        {
            if (Time.time - _lastSpawn > _delayTime && _isDone)
            {
                _lastSpawn = Time.time;
                _delayTime = Random.Range(maxDelayTime, maxDelayTime);
                SpawnEnemy();
            }
        }

        private async void SpawnEnemy()
        {
            int randomEnemy = Random.Range(0, _enemies.Length);
            int randomPos = Random.Range(0, positions.Count);
            var enemy = await spawner.GetAsync(_enemies[randomEnemy]);
            enemy.transform.position = positions[randomPos];
        }

        public void Return(GameObject go)
        {
            spawner.Return(go);
        }
    }
}