using FishGame.Ships;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingTimeBar : MonoBehaviour
{
    public Timer timer;
    public Ship ship;

    [SerializeField] public Slider timeSlider;
    [SerializeField] private TMP_Text timerText;
    public bool countdown = false;
    public void ShowTimer(Timer caller)
    {
        timer = caller;

        if (timer == null)
        {
            return;
        }    

        countdown = true;

    }

    private void FixedUpdate()
    {
        if (countdown) 
        {
            timeSlider.value = (float)(1.0 - timer.secondsLeft / (ship.GetFishingDuration()*60));
            timerText.text = timer.DisplayTime();
        }
        else
        {
            timerText.text = "Stoped";
        }

    }

}
