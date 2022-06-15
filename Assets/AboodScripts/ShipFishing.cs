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
    bool isFoucs = false;

    #region Server Region
    public static Action<SerializableShipData> OnPaused;
    public static Action<string> OnBack;
    #endregion

    private void Awake()
    {
        shipDataService = PlayFabShipData.Instance;
        PlayFabShipData.OnUpdatedShipData += HandleUpdateShipData;
      
    }
    private void Start()
    {
        //OnBack?.Invoke(currentShip.GetShipName());

        FillStore(currentShip.GetDataToJson().currentCapacity);
        fishingTimeBar = gameObject.GetComponentInChildren<FishingTimeBar>();
        timeToFillCapacity = currentShip.GetFishingDuration();
        InitFishing();

    }
    public void startFishingButton()
    {
        randNum = UnityEngine.Random.Range(1, 10);
        currentShip.isFishing = true;
        StartCoroutine("putfish");
        TimerMethod(currentShip.GetFishingDuration());
    }

    public void stopFishingButton()
    {
        fishingTimeBar.timeSlider.value = 100;
        fishingTimeBar.countdown = false;
        StoreCaughtFishButton();
        Destroy(timer);
        currentShip.isFishing = false;
    }

    private void InitFishing()
    {
        randNum = currentShip.GetDataToJson().FishType;
        if (currentShip.GetDataToJson().Fishing == "true")
        {
            currentShip.isFishing = true;
            FishOnQuitOrPause();
            StartCoroutine("putfish");
        }
    }

    private void FishOnQuitOrPause()
    {
        DateTime quitDate = DateTime.Parse(currentShip.GetDataToJson().QuitTime);
        float timeLift = float.Parse(currentShip.GetDataToJson().TimeToFill);
        timeToFillCapacity = timeLift / 60;
        timeLift = ((float)(WorldTimeAPI.Instance.GetCurrentDateTime() - quitDate).TotalSeconds)-43200 ;//+ 43200
        float lastFishing = timeLift;
        timeLift = (timeToFillCapacity) - timeLift / 60;

        float fishQuitTime = lastFishing / (currentShip.GetFishingDuration() * 60 / currentShip.GetNumberOfFishCaughtPerMin());
        if (currentShip.GetCapacity() + fishQuitTime >= currentShip.GetMaxCapacity())
        {
            currentShip.GetCaughtFishList().Clear();
            currentShip.isFishing = false;
            FillStore(currentShip.GetMaxCapacity());           
        }
        else if (currentShip.GetCapacity() < currentShip.GetMaxCapacity())
        {
            FillStore(fishQuitTime);
        }
        TimerMethod(timeLift);
    }


    public void TimerMethod(float time)
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Initialize("Fisihng", WorldTimeAPI.Instance.GetCurrentDateTime(), TimeSpan.FromMinutes(time));
        timer.startTimer();
        timer.TimerFinishedEvent.AddListener(delegate
        {
            currentShip.isFishing = false;
            completeButton.gameObject.SetActive(true);
            Destroy(timer);
        });
        fishingTimeBar.ShowTimer(timer);
    }

    public void FillStore(float fish)
    {
        List<SerializableFishData> caughtFish = FillShipCapacity((int)fish);
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

    private void OnDestroy()
    {
        PlayFabShipData.OnUpdatedShipData -= HandleUpdateShipData;
    }
    private void HandleUpdateShipData(SerializableShipData obj)
    {
        Debug.Log("HandleUpdateShipData IN SHIP FISHING Cs");
        currentShip.GetDataToJson().currentCapacity = obj.currentCapacity;
        currentShip.GetDataToJson().FishType = obj.FishType;
        currentShip.GetDataToJson().QuitTime = obj.QuitTime;
        currentShip.GetDataToJson().TimeToFill = obj.TimeToFill;
        currentShip.GetDataToJson().Xpos = obj.Xpos;
        currentShip.GetDataToJson().Ypos = obj.Ypos;
        currentShip.GetDataToJson().Fishing = obj.Fishing;
        currentShip.GetDataToJson().Stop = obj.Stop;

    }

    
    private IEnumerator putfish()
    {
        if (currentShip.isFishing == true)
        {
            do
            {
                if (currentShip.isFishing == false) yield return new WaitForSeconds(0);
                yield return new WaitForSeconds((currentShip.GetFishingDuration() * 60) / currentShip.GetMaxCapacity());
                List<SerializableFishData> caughtFish = FillShipCapacity(1);
                currentShip.GetCaughtFishList().AddRange(caughtFish);
                currentShip.UpdateCurrentCapacity();
            } while (currentShip.isFishing);
            Debug.Log("The Ship is full now");
        }
    }
    //Save Date Time On PlayerPrefs --
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            OnBack?.Invoke(currentShip.GetShipName());
            if (currentShip.isFishing)
            {
                Destroy(timer);
                Debug.Log("Ship name fishsing 1 : " + currentShip.name);
                FishOnQuitOrPause();
            }
        }
        if (pause)
        {          
            if (currentShip.isFishing)
            {
                SetCurrentShipFishingOnPaused();           
            }
            else
            {
                SetCurrentShipNotFishingOnPaused();
            }
        }
    }

    private void SetCurrentShipNotFishingOnPaused()
    {
        Debug.Log("SetCurrentShipNotFishingOnPaused in ShipFishing.cs " + currentShip.name);
        currentShip.GetDataToJson().QuitTime = "";
        currentShip.GetDataToJson().TimeToFill = "";
        currentShip.GetDataToJson().FishType = randNum;
        currentShip.GetDataToJson().currentCapacity = 0;
        currentShip.GetDataToJson().Fishing = "false";
        currentShip.GetDataToJson().Stop = "true";
        currentShip.GetDataToJson().Xpos = 0;
        currentShip.GetDataToJson().Ypos = 0;
        OnPaused?.Invoke(currentShip.GetDataToJson());
        //PlayerPrefs.SetInt(currentShip.GetShipName() + "FishType", randNum);
        //PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", "");
        //PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", "");
        //PlayerPrefs.SetInt(currentShip.GetShipName() + "currentCapacity", 0);
    }

    private void SetCurrentShipFishingOnPaused()
    {
        Debug.Log("SetCurrentShipFishingOnPaused in ShipFishing.cs " + currentShip.name);
        DateTime quitDate = WorldTimeAPI.Instance.GetCurrentDateTime();
       
        currentShip.GetDataToJson().QuitTime = quitDate.ToString();
        currentShip.GetDataToJson().TimeToFill = timer.secondsLeft.ToString();
        currentShip.GetDataToJson().FishType = randNum;
        currentShip.GetDataToJson().currentCapacity = currentShip.GetCapacity();
        currentShip.GetDataToJson().Fishing = "true";
        currentShip.GetDataToJson().Stop = "false";
        currentShip.GetDataToJson().Xpos = transform.position.x;
        currentShip.GetDataToJson().Ypos = transform.position.y;

        OnPaused?.Invoke(currentShip.GetDataToJson());

        //PlayerPrefs.SetString(currentShip.GetShipName() + "QuitTime", quitDate.ToString());
        //PlayerPrefs.SetString(currentShip.GetShipName() + "TimeToFill", timer.secondsLeft.ToString());
        //PlayerPrefs.SetInt(currentShip.GetShipName() + "FishType", randNum);
        //PlayerPrefs.SetInt(currentShip.GetShipName() + "currentCapacity", currentShip.GetCapacity());
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
            if (currentFishStorage.ContainsKey(fish.FishName))
            {
                currentFishStorage[fish.FishName]++;
            }
            else
            {
                currentFishStorage.Add(fish.FishName, 1);
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
