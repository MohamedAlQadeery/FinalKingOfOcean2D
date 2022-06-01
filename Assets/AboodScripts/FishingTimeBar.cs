using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingTimeBar : MonoBehaviour
{
    private static FishingTimeBar instance;

    private Timer timer;

    [SerializeField] private Slider timeSlider;
    [SerializeField] private TMP_Text timerText;
    public static bool countdown = false;
    private void Awake()
    {
        instance = this;
    }

    private void ShowTimer(GameObject caller)
    {
        timer = caller.GetComponent<Timer>();

        if (timer == null)
        {
            return;
        }

        Debug.Log(timer.Name);

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

    public static void ShowTimerStatic(GameObject caller)
    {
        instance.ShowTimer(caller);
    }

}
