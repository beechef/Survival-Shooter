using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets.Pooling;

namespace Runtime
{
    public class EffectPool : MonoBehaviour
    {
        private static EffectPool _instance;
        public static EffectPool Instance => _instance;
        [SerializeField] private AddressableGameObjectSpawner spawner;
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

        private void Start()
        {
            spawner.InitializeAsync().Forget();
        }

        public async UniTask<GameObject> GetEffect(string effect)
        {
            return await spawner.GetAsync(effect);
        }

        public void Return(GameObject go)
        {
            spawner.Return(go);
        }
    }
}