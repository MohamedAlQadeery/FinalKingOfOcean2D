using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public string Name { get; private set; }

    public bool isRunning { get; private set; }
    private DateTime startTime;
    public TimeSpan timeToFinish { get; private set; }
    private DateTime finishTime;
    public UnityEvent TimerFinishedEvent;

    public double secondsLeft { get; private set; }


    // to speed ot for tools uses on game as speed fishisng tool
    public int skipAmount
    {
        get
        {
            return (int)(secondsLeft / 60) * 2;
        }   
    }

    public void Initialize(string processName , DateTime start , TimeSpan time)
    {
        Name = processName;

        startTime = start;
        timeToFinish = time;
        finishTime = start.Add(time);

        TimerFinishedEvent = new UnityEvent();
    }

    public void startTimer()
    {
        secondsLeft = timeToFinish.TotalSeconds;
        isRunning = true;
    }

    private void Update()
    {
        if (isRunning)
        {
            if(secondsLeft > 0)
            {
                secondsLeft -= Time.deltaTime;
            }
            else
            {
                TimerFinishedEvent.Invoke();
                secondsLeft = 0;
                isRunning = false;
            }
        }
    }

    public string DisplayTime()
    {
        string text = "";
        TimeSpan timeLeft = TimeSpan.FromSeconds(secondsLeft);

        if (timeLeft.Days != 0)
        {
            text += timeLeft.Days + "d ";
            text += timeLeft.Hours + "h";
        }else if (timeLeft.Hours != 0)
        {
            text += timeLeft.Hours + "H ";
            text += timeLeft.Minutes + "min";
        }
        else if (timeLeft.Minutes != 0)
        {
            text += timeLeft.Minutes +":";
            text += timeLeft.Seconds;
        }
        else if (secondsLeft > 0)
        {
            text += "00:"+Mathf.FloorToInt((float) secondsLeft);
        }
        else
        {
            text = "Stoped";
        }

        return text;
    }

    public void SkipTimer()
    {
        secondsLeft = 0;
        finishTime = DateTime.Now;
    }



}
