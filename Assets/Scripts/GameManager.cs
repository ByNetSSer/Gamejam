using DamageNumbersPro;
using DamageNumbersPro.Demo;
using System;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
     public DamageNumber prefab;
    public RectTransform spawntext;
   
    public int Score;
    public static event Action<int> OnCoinCollected;
    //public DNP_ExampleMesh pro;
    private void Awake()
    {
        if (instance == null)
        instance = this;
        else
            Destroy(this.gameObject);
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

        DamageNumber scor = prefab.SpawnGUI(spawntext, Vector2.zero,Score);
        this.Score += Score;
    }
    public void spawn(float number,Vector3 position)
    {
        //numerosbug
    }
    public void ResetScore()
    {
        Score = 0;
        //OnScoreChanged?.Invoke(Score);
    }
}
