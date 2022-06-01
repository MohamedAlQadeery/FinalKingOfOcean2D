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

    public void stopFishingButton()
    {
        Debug.Log("Stopfishing");
        Debug.Log(timer.skipAmount);
        Debug.Log(timer.secondsLeft);
        Destroy(timer);
        FishingTimeBar.countdown = false;
        currentShip.ClearCapacity();
    }

    public void FillStore()
    {
        List<SerializableFishData> caughtFish = FillShipCapacity(currentShip.GetNumberOfFishCaughtPerMin());
        currentShip.GetCaughtFishList().AddRange(caughtFish);
        currentShip.UpdateCurrentCapacity();
        PlayerPrefs.SetString("ShipIsFull","true");
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
        //currentShip.isFishing;

        if (PlayerPrefs.GetString("Fishing").Equals("true"))
        {
            Awake();
        }
        else
        {
            StartCoroutine("putfish");
            timeToFillCapacity = currentShip.GetFishingDuration();
            Debug.Log("Startfishing");
            timer = gameObject.AddComponent<Timer>();
            timer.Initialize("Fisihng", DateTime.Now, TimeSpan.FromMinutes(1));
            timer.startTimer();
            timer.TimerFinishedEvent.AddListener(delegate
            {
                //FillStore();             
                PlayerPrefs.SetString("ShipIsFull", "false");
                //shipIsFullButton.SetActive(true);
                Destroy(timer);
                Debug.Log("Time Finished Ship is full");
            });
            FishingTimeBar.ShowTimerStatic(gameObject);
        }
    }

    private void Awake()
    {
        timeToFillCapacity = currentShip.GetFishingDuration();
        if (PlayerPrefs.GetString("Fishing").Equals("true"))
        {
            StartCoroutine("putfish");
            Debug.Log("Fishing 1");
            string quitDate =PlayerPrefs.GetString("QuitTime");
            Debug.Log(quitDate);
            DateTime quitDate1 = DateTime.Parse(quitDate);
            float timeLift = float.Parse(PlayerPrefs.GetString("TimeToFill"));
            timeToFillCapacity = timeLift/60;
            timeLift = ((float)(DateTime.Now - quitDate1).TotalSeconds) - 43200;
            //add fish when game quit

            float fishQuitTime = timeLift / (currentShip.GetFishingDuration() * 60 / currentShip.GetNumberOfFishCaughtPerMin());
            List<SerializableFishData> caughtFish = FillShipCapacity(fishQuitTime);
            currentShip.GetCaughtFishList().AddRange(caughtFish);
            currentShip.UpdateCurrentCapacity();

            Debug.Log("Fish Quit Time = "+ fishQuitTime);
            Debug.Log(timeLift);
            timeLift = (timeToFillCapacity) - timeLift/60;
            Debug.Log("Fishing 2");
            Debug.Log(timeLift);
            timer = gameObject.AddComponent<Timer>();
            timer.Initialize("Fisihng", DateTime.Now, TimeSpan.FromMinutes(timeLift));
            timer.startTimer();
            timer.TimerFinishedEvent.AddListener(delegate
            {
                //FillStore();
                PlayerPrefs.SetString("ShipIsFull", "false");
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

    //Save Date Time On PlayerPrefs --
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

    private IEnumerator putfish()
    {
        if (currentShip.CanFish())
        {
            do 
            {
                yield return new WaitForSeconds(currentShip.GetFishingDuration()*60/currentShip.GetNumberOfFishCaughtPerMin());
                List<SerializableFishData> caughtFish = FillShipCapacity(currentShip.GetFishingDuration());
                currentShip.GetCaughtFishList().AddRange(caughtFish);
                currentShip.UpdateCurrentCapacity();
            } while (!currentShip.IsCapacityFull());
            Debug.Log("The Ship is full now");

        }else
        {
            Debug.Log("Cant fish the ship is busy");
        }

    }

}
