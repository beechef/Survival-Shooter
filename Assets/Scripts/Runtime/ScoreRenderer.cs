using UnityEngine;
using UnityEngine.UI;

namespace Runtime
{
    public class ScoreRenderer : MonoBehaviour
    {
        [SerializeField] private Text score;

        public void Render(float value)
        {
            score.text = value.ToString();
        }
    }
}