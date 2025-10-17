using UnityEngine;
using System;
using System.Drawing;
using DamageNumbersPro.Demo;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI text;
    public int Score;
    public static event Action<int> OnCoinCollected;
    //public DNP_ExampleMesh pro;
    private void Awake()
    {
        if (instance == null)
        instance = this;
        else
            Destroy(this.gameObject);


        text.text = this.Score.ToString();
    }
    private void OnEnable()
    {
        OnCoinCollected += AddScore;
    }
    private void OnDisable()
    {
        OnCoinCollected -= AddScore;
    }
    public static void CollectItem(int value)
    {
        OnCoinCollected?.Invoke(value);
    }
    public void AddScore(int Score)
    {
        this.Score += Score;
        text.text = this.Score.ToString();
    }
    public void spawn(float number,Vector3 position)
    {
        
    }
    public void ResetScore()
    {
        Score = 0;
        //OnScoreChanged?.Invoke(Score);
    }
}
