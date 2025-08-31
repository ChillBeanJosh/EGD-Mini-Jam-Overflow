using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Diagnostics;

public class finalTimeShowcase : MonoBehaviour
{

    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI fastestScoreText;

    private void Start()
    {
        //Current Score:
        float lastScore = PlayerPrefs.GetFloat("LastScore", 0);
        finalScoreText.text = "Your Score: " + lastScore.ToString();

        // Load the fastest time from PlayerPrefs
        float highestScore = PlayerPrefs.GetFloat("HighestScore", float.MaxValue);
        if (highestScore == float.MaxValue)
        {
            fastestScoreText.text = "No record yet"; // Handle the case where there's no record
        }
        else
        {
            fastestScoreText.text = "Best Score: " + highestScore.ToString();
        }
    }
}
