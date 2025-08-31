using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class overflowBar : MonoBehaviour
{
    [Header("Meter Settings")]
    public float currentValue = 0f;    
    public float maxValue = 100f;      
    public float increaseRate = 2.5f;
    public float increaseInterval = 1f;
    public float increaseAmount = 0.01f;
    private float timeSinceLastInterval;
    [Space]

    [Header("References")]
    public inputReader inputGame;     
    public Slider meterSlider;
    public timerManager timer;
    [Space]

    [Header("Scoring Settings")]
    public int scoreMultiplier = 10;  

    private void Start()
    {
        if (meterSlider != null)
        {
            meterSlider.minValue = 0f;
            meterSlider.maxValue = maxValue;
            meterSlider.value = currentValue;
        }
    }

    private void Update()
    {
        if (currentValue >= maxValue)
        {
            inputGame.score = (inputGame.score / 2);
            timer.UpdateHighestScore();

            scenesManager.Instance.LoadScene(scenesManager.Scene.LoseScreen);
        }
        else if(currentValue < maxValue)
        {
            currentValue += increaseRate * Time.deltaTime;
            meterSlider.value = currentValue;
        }


        timeSinceLastInterval += Time.deltaTime;
        if (timeSinceLastInterval >= increaseInterval)
        {
            increaseRate += increaseAmount;
            timeSinceLastInterval = 0f;
        }
        
    }

    public void OnPatternCompleted()
    {
        int reduceAmount = inputGame.stringAmount * scoreMultiplier;

        currentValue -= reduceAmount;
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);

        Debug.Log("OVERFLOW METER REDUCED!!!");
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (meterSlider != null)
        {
            meterSlider.value = currentValue;
        }
    }
}
