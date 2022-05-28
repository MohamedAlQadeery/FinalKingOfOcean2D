using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishGame.Ships;
using System;

public class FishingTimer : MonoBehaviour
{

    public Slider timeSlider;
    public TMP_Text timerText;
    private float fishingTime;
    public bool stopTimer = true;
    public GameObject fullShipButton;
/*
    DateTime quitDateTime;
    int timeLeft;
*/
    // Start is called before the first frame update

    public void FullShip()
    {

        GetComponent<Fishing>().StoreCaughtFishButton();
        //here code for take fish to store ot collect fish from ship
        fullShipButton.SetActive(false);
    }

    public void FullShipActive()
    {
        fullShipButton.SetActive(true);
    }

    void Start()
    {

/*
        string dateQuitString = PlayerPrefs.GetString("quitDateTimeFish", "");
        string fishingTimeString = PlayerPrefs.GetString("FishingTime", "");
        if (!dateQuitString.Equals("")) {            
            fishingTime = int.Parse(fishingTimeString);
            quitDateTime = DateTime.Parse(dateQuitString);
            timeLeft = ((int)(DateTime.Now - quitDateTime).TotalSeconds) - 43200;
            fishingTime = fishingTime + timeLeft;
        }
*/

        stopTimer = true;
        fishingTime = GetComponent<Fishing>().GetTimeToFillCapacity();
        timeSlider.maxValue = fishingTime;
        timeSlider.value = fishingTime;   
        TimeMethod();
    }
    public void StartFishiingTimer()
    {
        stopTimer = false;      
    }

    public void StopFishiingTimer()
    {
        stopTimer = true ;
        fishingTime = GetComponent<Fishing>().GetTimeToFillCapacity();
        timeSlider.maxValue = fishingTime;
        timeSlider.value = fishingTime;
        TimeMethod();
    }

    private void TimeMethod()
    {
        int minutes = Mathf.FloorToInt(fishingTime / 60);
        int seconds = Mathf.FloorToInt(fishingTime - minutes * 60f);

        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = textTime;
    }

    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("Games is Pause :- on " + isPaused);
    }
    // Update is called once per frame
    void Update()
    {
        if (stopTimer == false) { 
        fishingTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(fishingTime / 60);
        int seconds = Mathf.FloorToInt(fishingTime - minutes * 60f);

        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (fishingTime <= 0)
        {
            stopTimer = true;
                FullShipActive();
        }

            if (stopTimer == false)
            {
                timerText.text = textTime;
                timeSlider.value = fishingTime;
            }
        }
    }
/*
    private void OnApplicationQuit()
    {
        DateTime quitDate = DateTime.Now;
        PlayerPrefs.SetString("quitDateTimeFish", quitDate.ToString());
        PlayerPrefs.SetString("FishingTime", fishingTime.ToString());
    }
*/
}
