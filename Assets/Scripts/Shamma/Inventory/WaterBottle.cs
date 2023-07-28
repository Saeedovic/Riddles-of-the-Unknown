using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBottle
{
    int servingsLeft = 0;
    [SerializeField] int maxServings = 20;

    Slider servingIndicator;
    public bool isExpendable = false;

    public WaterBottle(Button bottleButton, Slider servingIndicator)
    {
        bottleButton.onClick.AddListener(UseBottle);

        this.servingIndicator = servingIndicator;
        this.servingIndicator.maxValue = maxServings;
    }

    public void RefillBottle(int servingsToAdd)
    {
        servingsLeft += servingsToAdd;

        if (servingsLeft > maxServings)
        {
            servingsLeft = maxServings;
        }

        UpdateIndicator();
    }

    public void UseBottle()
    {
        if (servingsLeft != 0)
        {
            servingsLeft--;
            ThirstSystem.Instance.RefillThirst();
            Debug.Log("water drank, servings left: " + servingsLeft);
        }
        else
        {
            Debug.Log("you don't have any water left.");
        }

        UpdateIndicator();
    }

    public void UpdateIndicator()
    {
        servingIndicator.value = servingsLeft;
    }
}
