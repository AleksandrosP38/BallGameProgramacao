using UnityEngine;
using UnityEngine.UI;

public class Timer: MonoBehaviour
{
    public Text timerText;
    private float timeRemaining = 30f;
    private bool timerIsRunning = false;

    void Start()
    {
        // Set the initial time and start the timer
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

        // Format the time as minutes and seconds
        string minutes = Mathf.Floor(timeRemaining / 60).ToString("00");
        string seconds = (timeRemaining % 60).ToString("00");

        // Update the UI text
        timerText.text = $"{minutes}:{seconds}";

        // Check if the timer has reached zero
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerIsRunning = false;
            Debug.Log("Timer has run out!");
            // Optionally, you can add code here to handle what happens when the timer reaches zero.
        }
    }
}
