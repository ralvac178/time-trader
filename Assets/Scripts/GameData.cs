using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameData : MonoBehaviour
{
    public static GameData singleton;

    public int score = 0;
    public TextMeshProUGUI textScore;

    // Awake is called before the first frame update
    void Awake()
    {
        GameObject[] gd = GameObject.FindGameObjectsWithTag("gameData");

        if (gd.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        singleton = this;
    }

    public void UpdateTextScore(int points)
    {
        score += points;
        if (textScore != null)
        {    
            textScore.text = $"Score {score}";
        }
    }
}
