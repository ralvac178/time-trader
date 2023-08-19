using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI highestScoreText;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("lastScore"))
        {
            lastScoreText.text = $"Last Score: {PlayerPrefs.GetInt("lastScore")}";
        }
        else
        {
            lastScoreText.text = $"Last Score: 0";
        }

        if (PlayerPrefs.HasKey("highScore"))
        {
            highestScoreText.text = $"Highest Score: {PlayerPrefs.GetInt("highScore")}";
        }
        else
        {
            highestScoreText.text = $"Highest Score: 0";
        }
    }
}
