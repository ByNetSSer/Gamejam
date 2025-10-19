using UnityEngine;

public static class ScoreManager
{
    private const int MaxScores = 5;
    private const string KeyPrefix = "HighScore_";

    public static void SaveScore(int newScore)
    {
        // Cargar los puntajes actuales
        int[] scores = LoadScores();

        int[] updated = new int[scores.Length + 1];
        for (int i = 0; i < scores.Length; i++)
            updated[i] = scores[i];
        updated[updated.Length - 1] = newScore;

        System.Array.Sort(updated);
        System.Array.Reverse(updated);

        for (int i = 0; i < MaxScores; i++)
        {
            PlayerPrefs.SetInt(KeyPrefix + i, i < updated.Length ? updated[i] : 0);
        }

        PlayerPrefs.Save();
    }

    public static int[] LoadScores()
    {
        int[] scores = new int[MaxScores];
        for (int i = 0; i < MaxScores; i++)
        {
            scores[i] = PlayerPrefs.GetInt(KeyPrefix + i, 0);
        }
        return scores;
    }
}
