using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [HideInInspector]
    public int currentScore, highScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        scoreText.text = currentScore.ToString("000000");

        if (currentScore > highScore)
        { 
            highScore = currentScore;
        }
    }


}
