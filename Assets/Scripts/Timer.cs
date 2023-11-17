using UnityEngine;
using UnityEngine.UI;

public class Timer: MonoBehaviour
{
    public Text timerText;
    private float timeRemaining = 30f;
    private bool timerIsRunning = false;

    void Start()
    {
        timeRemaining = 30f;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;

        string minutes = Mathf.Floor(timeRemaining / 60).ToString("00");
        string seconds = (timeRemaining % 60).ToString("00");

        timerText.text = $"{minutes}:{seconds}";

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerIsRunning = false;
            Debug.Log("Timer has run out!");
        }
    }
}
