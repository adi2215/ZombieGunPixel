using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timerText;
    public float totalTime = 60f;
    private float currentTime;
    public Health player;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        currentTime = Mathf.Max(currentTime, 0f);

        UpdateTimerText();

        if (currentTime == 0f && player != null)
        {
            player.Die();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
