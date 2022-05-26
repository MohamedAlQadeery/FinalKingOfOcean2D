using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishGame.Ships;

public class FishingTimer : MonoBehaviour
{

    public Slider timeSlider;
    public TMP_Text timerText;
    private float fishingTime;
    public bool stopTimer = true;
    public GameObject fullShipButton;
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
}
