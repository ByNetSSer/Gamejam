using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        int[] scores = ScoreManager.LoadScores();

        for (int i = 0; i < scoreTexts.Length && i < scores.Length; i++)
        {
            scoreTexts[i].text = (i + 1) + ". " + scores[i].ToString();
        }
    }
}
