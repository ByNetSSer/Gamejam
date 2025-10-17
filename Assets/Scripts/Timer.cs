using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float startTimeSeconds = 60f;


    float timeValue;
    bool running;


    void Start()
    {
        timeValue = startTimeSeconds;
        running = true;
    }


    void Update()
    {
        if (!running) return;


        timeValue -= Time.deltaTime;
        if (timeValue <= 0f)
        {
            timeValue = 0f;
            running = false;
            Debug.Log("me detube");
        }


        int minutes = Mathf.FloorToInt(timeValue / 60f);
        int seconds = Mathf.FloorToInt(timeValue % 60f);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
