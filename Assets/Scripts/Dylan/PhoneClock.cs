using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneClock : MonoBehaviour
{
    private TextMeshProUGUI textClock;
    void Awake()
    {
        textClock = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        DateTime time = DateTime.Now;
        string hour = LeadingZero(time.Hour);
        string minute = LeadingZero(time.Minute);
        textClock.text = hour + ":" + minute;
    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
