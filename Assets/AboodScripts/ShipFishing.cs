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
    Timer timer;
    FishingTimeBar fishingTimeBar;
    int randNum;
    PlayFabShipData shipDataService;
    static string clickedShipName = string.Empty;


    public void stopFishingButton()
    {
        fishingTimeBar.timeSlider.value = 100;
        fishingTimeBar.countdown = false;
        StoreCaughtFishButton();
        Debug.Log("Stopfishing");
        Destroy(timer);
        currentShip.isFishing = false;
    }

    public void TimerMethod(float time)
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Initialize("Fisihng", WorldTimeAPI.Instance.GetCurrentDateTime(), TimeSpan.FromMinutes(time));
        timer.startTimer();
        timer.TimerFinishedEvent.AddListener(delegate
        {
            currentShip.isFishing = false;
            PlayerPrefs.SetString(currentShip.GetShipName() + "ShipIsFull", "true");
            PlayerPrefs.SetString(currentShip.GetShipName() + "Fishing", "false");
            completeButton.gameObject.SetActive(true);
            Destroy(timer);
            currentShip.isFishing = false;
        });
        fishingTimeBar.ShowTimer(timer);
    }

    public void FillStore(float fish)
    {
        List<SerializableFishData> caughtFish = FillShipCapacity(fish);
        currentShip.GetCaughtFishList().AddRange(caughtFish);
        currentShip.UpdateCurrentCapacity();
    }

    private List<SerializableFishData> FillShipCapacity(float numberOfFishes)
    {
        List<SerializableFishData> tmpList = new List<SerializableFishData>();
        for (int i = 0; i < numberOfFishes; i++)
        {          
            if (currentShip.GetCanFishTypesList().Count == 1)
            {
                tmpList.Add(currentShip.GetCanFishTypesList()[0].GetDataToJson());
            }
            else if (currentShip.GetCanFishTypesList().Count == 2)
            {
                if (randNum < 5)
                {
                    tmpList.Add(currentShip.GetCanFishTypesList()[0].GetDataToJson());
                }
                else
                {
                    tmpList.Add(currentShip.GetCanFishTypesList()[1].GetDataToJson());
                }
            }
            else if (currentShip.GetCanFishTypesList().Count == 3)
            {
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
            //for more than 3 fish 
            /*else if (currentShip.GetCanFishTypesList().Count == 4)
            {
                if (randNum < 4)
                {
                    tmpList.Add(currentShip.GetCanFishTypesList()[0].GetDataToJson());
                }
                else if (randNum > 4 && randNum <= 7)
                {
                    tmpList.Add(currentShip.GetCanFishTypesList()[1].GetDataToJson());
                }
                else if (randNum > 7 && randNum <= 9)
                {
                    tmpList.Add(currentShip.GetCanFishTypesList()[2].GetDataToJson());
                }
                else
                {
                    tmpList.Add(currentShip.GetCanFishTypesList()[3].GetDataToJson());
                }
            }*/
        }
        return tmpList;
    }

    public void startFishingButton()
    {
        randNum = UnityEngine.Random.Range(1, 10);
        currentShip.isFishing = true;
        if (PlayerPrefs.GetString(currentShip.GetShipName() + "Fishing").Equals("true"))
        {            
            Awake();
        }
        else
        {
            Debug.Log("In start first time or back and start");
            StartCoroutine("putfish");
            timeToFillCapacity = currentShip.GetFishingDuration();
            TimerMethod(currentShip.GetFishingDuration());
        }
    }
    private void Awake()
    {
        shipDataService = PlayFabShipData.Instance;
        randNum = PlayerPrefs.GetInt(currentShip.GetShipName() + "FishType");
        fishingTimeBar = gameObject.GetComponentInChildren<FishingTimeBar>();
        timeToFillCapacity = currentShip.GetFishingDuration();
       /* if (PlayerPrefs.GetString(currentShip.GetShipName() + "Fishing").Equals("true"))
        {
            Debug.Log("In exit then back ");
            if (currentShip.isFishing == false) return;
            Debug.Log("In exit then back 22");
            DateTime quitDate = DateTime.Parse(PlayerPrefs.GetString(currentShip.GetShipName() + "QuitTime"));
            float timeLift = float.Parse(PlayerPrefs.GetString(currentShip.GetShipName() + "TimeToFill"));
            timeToFillCapacity = timeLift / 60;
            timeLift = ((float)(WorldTimeAPI.Instance.GetCurrentDateTime() - quitDate).TotalSeconds) - 43200;//+ 43200
            float lastFishing = timeLift;
            timeLift = (timeToFillCapacity) - timeLift / 60;
            StartCoroutine("putfish");
            float fishQuitTime = lastFishing / (currentShip.GetFishingDuration() * 60 / currentShip.GetNumberOfFishCaughtPerMin());
            Debug.Log("@@@" + currentShip.GetCapacity() + " " + fishQuitTime);
            if (currentShip.GetCapacity() + fishQuitTime >= currentShip.GetMaxCapacity())
            {
                currentShip.GetCaughtFishList().Clear();
                FillStore(currentShip.GetMaxCapacity() - 1);
                PlayerPrefs.SetString(currentShip.GetShipName() + "ShipIsFull", "true");
                currentShip.isFishing = false;
            }
            else if (currentShip.GetCapacity() < currentShip.GetMaxCapacity())
            {
                FillStore(fishQuitTime);
            }
            TimerMethod(timeLift);
        }
        else
        {
            Debug.Log(currentShip.name + " is idel (noot fishsing)");
        }*/
    }

    private IEnumerator putfish()
    {
        if (currentShip.isFishing == true)
        {
            do
            {
                yield return new WaitForSeconds((currentShip.GetFishingDuration() * 60) / currentShip.GetMaxCapacity());
                List<SerializableFishData> caughtFish = FillShipCapacity(1);
                currentShip.GetCaughtFishList().AddRange(caughtFish);
                currentShip.UpdateCurrentCapacity();
            } while (currentShip.isFishing);
            Debug.Log("The Ship is full now");
        }
    }

    //Save Date Time On PlayerPrefs --
    private void OnApplicationFocus(bool focus)
    {
        Debug.Log("ON FOUCS ");
        if (!focus) return;
        if (PlayerPrefs.GetString(currentShip.GetShipName() + "Fishing").Equals("true"))
        {
            Debug.Log("In exit then back ");
            if (currentShip.isFishing == false) return;
            Debug.Log("In exit then back 22");
            DateTime quitDate = DateTime.Parse(PlayerPrefs.GetString(currentShip.GetShipName() + "QuitTime"));
            float timeLift = float.Parse(PlayerPrefs.GetString(currentShip.GetShipName() + "TimeToFill"));
            timeToFillCapacity = timeLift / 60;
            timeLift = ((float)(WorldTimeAPI.Instance.GetCurrentDateTime() - quitDate).TotalSeconds) - 43200;//+ 43200
            float lastFishing = timeLift;
            timeLift = (timeToFillCapacity) - timeLift / 60;
            StartCoroutine("putfish");
            float fishQuitTime = lastFishing / (currentShip.GetFishingDuration() * 60 / currentShip.GetNumberOfFishCaughtPerMin());
            Debug.Log("@@@" + currentShip.GetCapacity() + " " + fishQuitTime);
            if (currentShip.GetCapacity() + fishQuitTime >= currentShip.GetMaxCapacity())
            {
                currentShip.GetCaughtFishList().Clear();
                FillStore(currentShip.GetMaxCapacity() - 1);
                PlayerPrefs.SetString(currentShip.GetShipName() + "ShipIsFull", "true");
                currentShip.isFishing = false;
            }
            else if (currentShip.GetCapacity() < currentShip.GetMaxCapacity())
            {
                FillStore(fishQuitTime);
            }
            TimerMethod(timeLift);
        }
        else
        {
            Debug.Log(currentShip.name + " is idel (noot fishsing)");
        }
        /*if (currentShip.isFishing)
        {
            Debug.Log("pausssssssssssssssssssssssssssssss");
            DateTime quitDate = WorldTimeAPI.Instance.GetCurrentDateTime();
            PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", quitDate.ToString());
            PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", timer.secondsLeft.ToString());
            PlayerPrefs.SetInt(currentShip.GetShipName() + "FishType", randNum);
        }
        else
        {
            Debug.Log("pausssssssssssssssssssssssssssssss");
            PlayerPrefs.SetInt(currentShip.GetShipName() + "FishType", randNum);
            PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", "");
            PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", "");
        }*/
    }
    private void OnApplicationPause(bool pause)
    {
        Debug.Log("ON POUSE ");
        if (!pause) return;
        if (currentShip.isFishing)
        {
            DateTime quitDate = WorldTimeAPI.Instance.GetCurrentDateTime();
            PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", quitDate.ToString());
            PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", timer.secondsLeft.ToString());
            PlayerPrefs.SetInt(currentShip.GetShipName() + "FishType", randNum);
        }
        else
        {
            PlayerPrefs.SetInt(currentShip.GetShipName() + "FishType", randNum);
            PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", "");
            PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", "");
        }
    }
    private void OnApplicationQuit()
    {
        Debug.Log("ON nApplicationQuit ");
        if (currentShip.isFishing)
        {
            Debug.Log("Quit and safe "+currentShip.name);
            DateTime quitDate = WorldTimeAPI.Instance.GetCurrentDateTime();
            PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", quitDate.ToString());
            PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", timer.secondsLeft.ToString());
            PlayerPrefs.SetInt(currentShip.GetShipName() + "FishType", randNum);
        }
        else
        {
            Debug.Log("Quit and Not safe " + currentShip.name);
            PlayerPrefs.SetInt(currentShip.GetShipName() + "FishType" , randNum);
            PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", "");
            PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", "");
        }
    }


    //shittttttttttttttttttttttttttttttttttttt
    private void OnEnable()
    {
        shipDataService.GetFishJsonSuccess.AddListener(UpdateFishStorage);
        shipDataService.updateFishStorageSuccess.AddListener(OnUpdateFishStorageSuccess);
        completeButton.onClick.AddListener(StoreCaughtFishButton);

    }

    private void OnDisable()
    {
        shipDataService.GetFishJsonSuccess.RemoveListener(UpdateFishStorage);
        shipDataService.updateFishStorageSuccess.RemoveListener(OnUpdateFishStorageSuccess);
        completeButton.onClick.RemoveListener(StoreCaughtFishButton);
    }
    /**
         * Gets the current fish storage from the serever
         * then update it with updated data then send it back to the server
         */

    public void StoreCaughtFishButton()
    {
        clickedShipName = currentShip.GetShipName();
        completeButton.gameObject.SetActive(false);
        shipDataService.GetFishJsonValue(); // invoke event that updateFishStorage() listens to it
    }

    public void UpdateFishStorage(string fishJson)
    {
        if (currentShip.GetShipName() != clickedShipName) return;
        Debug.Log("Inside UpdateFishStorage()");


        Dictionary<string, int> currentFishStorage = JsonConvert.DeserializeObject<Dictionary<string, int>>(fishJson);
        Debug.Log(fishJson);
        foreach (var fish in currentShip.GetCaughtFishList())
        {
            if (currentFishStorage.ContainsKey(fish.fishName))
            {
                currentFishStorage[fish.fishName]++;
            }
            else
            {
                currentFishStorage.Add(fish.fishName, 1);
            }
        }


        shipDataService.UpdateFishStorage(currentFishStorage);
    }

    public void OnUpdateFishStorageSuccess(string message)
    {
        if (currentShip.GetShipName() != clickedShipName) return;

        currentShip.ClearCapacity();
        Debug.Log(message);
    }
}
