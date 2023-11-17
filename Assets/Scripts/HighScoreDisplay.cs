using UnityEngine.UI;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text highScoreText;

    private void Start()
    {
        UpdateHighScoreText();
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }
}
