using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float startTimeSeconds = 120f;
    public string SceneWin;
    public string SceneLosse;
    public int Goal = 1000;
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
            int scoretotal = GameManager.instance.Score + Combo.Instance.ReturnScoreActual();
            if (scoretotal < Goal)
            {
                Debug.Log("perdiste");
                SceneManager.LoadScene(SceneLosse);
            }
            else
            {
                Debug.Log("ganaste");
                SceneManager.LoadScene(SceneWin);
            }
        }


        int minutes = Mathf.FloorToInt(timeValue / 60f);
        int seconds = Mathf.FloorToInt(timeValue % 60f);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
