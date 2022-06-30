using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runtime
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text txtHighestScore;

        public void Open()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            txtHighestScore.text = DataLoader.saveData.HighestScore.ToString();
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }

        public void Replay()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SampleScene");
        }
    }
}