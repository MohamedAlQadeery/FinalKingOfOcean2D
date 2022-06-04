using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingTimeBar : MonoBehaviour
{
    //private static FishingTimeBar instance;

    public Timer timer;

    [SerializeField] private Slider timeSlider;
    [SerializeField] private TMP_Text timerText;
    public bool countdown = false;
    /*private void Awake()
    {
        instance = this;
    }
    */
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
            timeSlider.value = (float)(1.0 - timer.secondsLeft / timer.timeToFinish.TotalSeconds);
            timerText.text = timer.DisplayTime();
        }
        else
        {
            timerText.text = "Cancel";
        }

    } 

    //public void ShowTimerStatic(GameObject caller)
    //{
    //    ShowTimer(caller);
    //}

}
