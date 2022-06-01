using FishGame.Core;
using FishGame.Fishes;
using FishGame.Ships;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipFishing : MonoBehaviour
{
    [SerializeField] Ship currentShip;
    [SerializeField] float timeToFillCapacity;
    [SerializeField] bool startFishing = false;
    [SerializeField] Button completeButton;
    [SerializeField] GameObject shipIsFullButton;
    Timer timer;
    public void TimeToFishing()
    {
        timeToFillCapacity = currentShip.GetFishingDuration();
    }

    public void stopFishingButton()
    {
        Debug.Log("Stopfishing");
        Debug.Log(timer.skipAmount);
        Debug.Log(timer.secondsLeft);
        Destroy(timer);
        FishingTimeBar.countdown = false;
    }

    public void startFishingButton()
    {
        if (PlayerPrefs.GetString("Fishing").Equals("true"))
        {
            Awake();
        }
        else
        {
            timeToFillCapacity = currentShip.GetFishingDuration();
            Debug.Log("Startfishing");
            timer = gameObject.AddComponent<Timer>();
            timer.Initialize("Fisihng", DateTime.Now, TimeSpan.FromMinutes(timeToFillCapacity));
            timer.startTimer();
            timer.TimerFinishedEvent.AddListener(delegate
            {
                //shipIsFullButton.SetActive(true);
                Destroy(timer);
                Debug.Log("Time Finished Ship is full");
            });
            FishingTimeBar.ShowTimerStatic(gameObject);
        }
    }

    private void Awake()
    {
        TimeToFishing();
        if (PlayerPrefs.GetString("Fishing").Equals("true"))
        {
            
            Debug.Log("Fishing 1");

            string quitDate =PlayerPrefs.GetString("QuitTime");
            Debug.Log(quitDate);
            DateTime quitDate1 = DateTime.Parse(quitDate);
            float timeLift = float.Parse(PlayerPrefs.GetString("TimeToFill"));
            timeToFillCapacity = timeLift/60;
            timeLift = ((float)(DateTime.Now - quitDate1).TotalSeconds) - 43200;
            
            Debug.Log(timeLift);
            timeLift = (timeToFillCapacity) - timeLift/60;
            Debug.Log("Fishing 2");
            Debug.Log(timeLift);
            timer = gameObject.AddComponent<Timer>();
            timer.Initialize("Fisihng", DateTime.Now, TimeSpan.FromMinutes(timeLift));
            timer.startTimer();
            timer.TimerFinishedEvent.AddListener(delegate
            {
                PlayerPrefs.SetString("Fishing", "false");
                //shipIsFullButton.SetActive(true);
                Destroy(timer);
                Debug.Log("Time Finished Ship is full");
            });
            FishingTimeBar.ShowTimerStatic(gameObject);
            
        }
        else
        {

            Debug.Log("Fishing no no no 1");
        }
    }


    private void OnApplicationQuit()
    {
        if (PlayerPrefs.GetString("isFishing").Equals("true"))
        {
            DateTime quitDate = DateTime.Now;
            PlayerPrefs.SetString("QuitTime", quitDate.ToString());
            PlayerPrefs.SetString("TimeToFill", timer.secondsLeft.ToString());
        }
        else
        {
            PlayerPrefs.SetString("QuitTime", "");
            PlayerPrefs.SetString("TimeToFill", "");
        }
    }




    }
