using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RealTimeClock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private TextMeshProUGUI dateText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        DateTime today = DateTime.Now;
        clockText.text = today.ToString("HH:mm");
        dateText.text = string.Format("{0:00}/{1:00}/{2:00}", today.Day, today.Month, 2004);
    }
    
}
