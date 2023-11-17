using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int points = 0;
    [SerializeField] private Text pointsText;
    private string playerPrefsKey = "PlayerScore";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetScore();
    }

    private void Update()
    {
        CheckGameOverScene();
    }

    public void IncreasePoints()
    {
        points += 1;

        int highScore = GetHighScore();
        if (points > highScore)
        {
            SetHighScore(points);
        }

        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = points.ToString();
        }
    }

    private void CheckGameOverScene()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            Destroy(gameObject);
        }
    }

    private int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SetHighScore(int newHighScore)
    {
        PlayerPrefs.SetInt("HighScore", newHighScore);
        PlayerPrefs.Save();
    }

    public void ResetScore()
    {
        points = 0;
        SaveScore();
        UpdatePointsText();
    }

    private void LoadScore()
    {
        points = PlayerPrefs.GetInt(playerPrefsKey, 0);
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt(playerPrefsKey, points);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveScore();
    }
}
