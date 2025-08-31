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
    [Space]

    [Header("References")]
    public inputReader inputGame;     
    public Slider meterSlider;
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

        StartCoroutine(IncreaseMeter());
    }

    private void Update()
    {
        if (currentValue == maxValue)
        {
            scenesManager.Instance.LoadScene(scenesManager.Scene.LoseScreen);
        }
    }

    private IEnumerator IncreaseMeter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            currentValue += increaseRate;
            currentValue = Mathf.Clamp(currentValue, 0f, maxValue);

            UpdateUI();

            if (currentValue >= maxValue)
            {
                Debug.Log("GAME OVER!!!!");
            }
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
