using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingTimeBar : MonoBehaviour
{

    [SerializeField] private Slider timeSlider;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float fishingTime;
    [SerializeField] private bool stopTimer = true;
    [SerializeField] private GameObject fullShipButton;

    private void Start()
    {
        float timeToFillShip = GetComponent<ShipFishing>().TimeToFishing();

        Timer timer = gameObject.AddComponent<Timer>();
        timer.Initialize("Fisihng", DateTime.Now, TimeSpan.FromMinutes(2));
        timer.startTimer();
        timer.TimerFinishedEvent.AddListener(delegate
        {
            Destroy(gameObject);
            Destroy(timer);
        });
    }
}
