using System;
using UnityEngine;

namespace Runtime
{
    public class HealthBarRenderer : MonoBehaviour
    {
        [SerializeField] private Transform healthBar;
        private float _defaultScaleX;

        public void Render(float value, float maxValue)
        {
            float ratio = (value / maxValue) * _defaultScaleX;
            Vector3 scale = healthBar.transform.localScale;
            scale.x = ratio;
            healthBar.transform.localScale = scale;
        }

        private void OnEnable()
        {
            _defaultScaleX = healthBar.localScale.x;
            _defaultScaleX = _defaultScaleX == 0f ? 1f : _defaultScaleX;

            Render(1, 1);
        }
    }
}