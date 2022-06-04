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
    FishingTimeBar fishingTimeBar;

    public void stopFishingButton()
    {
        Debug.Log("Stopfishing");
        Destroy(timer);
        currentShip.ClearCapacity();
        currentShip.isFishing = false;
    }

    public void FillStore()
    {
        List<SerializableFishData> caughtFish = FillShipCapacity(currentShip.GetNumberOfFishCaughtPerMin());
        currentShip.GetCaughtFishList().AddRange(caughtFish);
        currentShip.UpdateCurrentCapacity();
    }
    private List<SerializableFishData> FillShipCapacity(float numberOfFishes)
    {
        List<SerializableFishData> tmpList = new List<SerializableFishData>();
        for (int i = 0; i < numberOfFishes; i++)
        {
            int randNum = UnityEngine.Random.Range(1, 10);
            if (randNum < 5)
            {
                tmpList.Add(currentShip.GetCanFishTypesList()[0].GetDataToJson());
            }
            else if (randNum > 5 && randNum <= 8)
            {
                tmpList.Add(currentShip.GetCanFishTypesList()[1].GetDataToJson());

            }
            else
            {
                tmpList.Add(currentShip.GetCanFishTypesList()[2].GetDataToJson());
            }
        }
        return tmpList;
    }

    public void startFishingButton()
    {
        currentShip.isFishing = true;
        if (PlayerPrefs.GetString(currentShip.GetShipName() + "Fishing").Equals("true"))
        {
            Debug.Log("Startfishing seconde or more time");
            Awake();
        }
        else
        {
            Debug.Log("Startfishing first time");
            StartCoroutine("putfish");
            timeToFillCapacity = currentShip.GetFishingDuration();
            timer = gameObject.AddComponent<Timer>();
            timer.Initialize("Fisihng", DateTime.Now, TimeSpan.FromMinutes(currentShip.GetFishingDuration()));
            timer.startTimer();
            timer.TimerFinishedEvent.AddListener(delegate
            {
                PlayerPrefs.SetString(currentShip.GetShipName() + "ShipIsFull", "false");
                //shipIsFullButton.SetActive(true);
                Destroy(timer);
                Debug.Log("Time Finished Ship is full");
                currentShip.isFishing = false;
            });
            fishingTimeBar.ShowTimer(timer);
        }
    }
    private void Awake()
    {
        fishingTimeBar = gameObject.GetComponentInChildren<FishingTimeBar>();
        timeToFillCapacity = currentShip.GetFishingDuration();
        if (PlayerPrefs.GetString(currentShip.GetShipName() + "Fishing").Equals("true"))
        {
            if (currentShip.isFishing == false) return;
            Debug.Log("is fishing" + currentShip.isFishing);
            string quitDate = PlayerPrefs.GetString(currentShip.GetShipName() + "QuitTime");
            DateTime quitDate1 = DateTime.Parse(quitDate);
            float timeLift = float.Parse(PlayerPrefs.GetString(currentShip.GetShipName() + "TimeToFill"));
            timeToFillCapacity = timeLift / 60;
            timeLift = ((float)(DateTime.Now - quitDate1).TotalSeconds);//+ 43200
            //add fish when game quit
            float lastFishing = timeLift;
            timeLift = (timeToFillCapacity) - timeLift / 60;
            StartCoroutine("putfish");
            float fishQuitTime = lastFishing / (currentShip.GetFishingDuration() * 60 / currentShip.GetNumberOfFishCaughtPerMin());
            if (currentShip.GetCapacity() + fishQuitTime >= currentShip.GetMaxCapacity())
            {
                Debug.Log(" is full : " + currentShip.IsCapacityFull());
                List<SerializableFishData> caughtFish = FillShipCapacity(currentShip.GetMaxCapacity() - 1);
                currentShip.GetCaughtFishList().Clear();
                currentShip.GetCaughtFishList().AddRange(caughtFish);
                currentShip.UpdateCurrentCapacity();
                PlayerPrefs.SetString(currentShip.GetShipName() + "ShipIsFull", "true");
                currentShip.isFishing = false;
            }
            else if (currentShip.GetCapacity() < currentShip.GetMaxCapacity())
            {
                List<SerializableFishData> caughtFish = FillShipCapacity(fishQuitTime);
                currentShip.GetCaughtFishList().AddRange(caughtFish);
                currentShip.UpdateCurrentCapacity();
            }
            timer = gameObject.AddComponent<Timer>();
            timer.Initialize("Fisihng", DateTime.Now, TimeSpan.FromMinutes(timeLift));
            timer.startTimer();
            timer.TimerFinishedEvent.AddListener(delegate
            {
                currentShip.isFishing = false;
                PlayerPrefs.SetString(currentShip.GetShipName() + "ShipIsFull", "true");
                PlayerPrefs.SetString(currentShip.GetShipName() + "Fishing", "false");
                //shipIsFullButton.SetActive(true);
                Destroy(timer);
                currentShip.isFishing = false;
            });
            fishingTimeBar.ShowTimer(timer);
        }
        else
        {
            Debug.Log("Fishing no no no 1");
        }
    }

    private IEnumerator putfish()
    {
        if (currentShip.isFishing == true)
        {
            do
            {
                yield return new WaitForSeconds(currentShip.GetFishingDuration() * 60 / currentShip.GetNumberOfFishCaughtPerMin());
                List<SerializableFishData> caughtFish = FillShipCapacity(currentShip.GetFishingDuration());
                currentShip.GetCaughtFishList().AddRange(caughtFish);
                currentShip.UpdateCurrentCapacity();
            } while (currentShip.isFishing);
            Debug.Log("The Ship is full now");
        }
    }

    //Save Date Time On PlayerPrefs --
    private void OnApplicationQuit()
    {
        if (currentShip.isFishing)
        {
            DateTime quitDate = DateTime.Now;
            PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", quitDate.ToString());
            PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", timer.secondsLeft.ToString());
        }
        else
        {
            PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", "");
            PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", "");
        }
    }
}
