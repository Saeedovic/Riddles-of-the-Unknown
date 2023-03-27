using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirstSystem : MonoBehaviour
{
    public float thirstLevel = 100f;
    public float maxThirstLevel = 100f;
    public float thirstThreshold = 30f;
    public float thirstDepletionRate = 1f;
    public float thirstPenaltyRate = 0.5f;
    public Slider thirstSlider;

    private void Start()
    {
        UpdateThirstLevelUI();
    }

    private void Update()
    {
        float penaltyMultiplier = 1f;
        if (thirstLevel <= thirstThreshold)
        {
            penaltyMultiplier = thirstPenaltyRate;
        }
        thirstLevel -= thirstDepletionRate * penaltyMultiplier * Time.deltaTime;
        thirstLevel = Mathf.Clamp(thirstLevel, 0f, maxThirstLevel);
        UpdateThirstLevelUI();
    }

    public void DrinkWater(float amount)
    {
        thirstLevel += amount;
        thirstLevel = Mathf.Clamp(thirstLevel, 0f, maxThirstLevel);
        UpdateThirstLevelUI();
    }

    private void UpdateThirstLevelUI()
    {
        thirstSlider.value = thirstLevel / maxThirstLevel;
    }
}