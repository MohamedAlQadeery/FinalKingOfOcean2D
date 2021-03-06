using FishGame.Core;
using FishGame.Fishes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FishGame.Ships
{
    public class Fishing : MonoBehaviour
    {
        [SerializeField] Ship currentShip;
        [SerializeField] float timeToFillCapacity;
        [SerializeField] bool startFishing = false;
        private IEnumerator fishingCourtineRef;

        PlayFabShipData shipDataService;
        [SerializeField] Button completeButton;
        static string clickedShipName = string.Empty;

        private void Awake()
        {
            shipDataService = PlayFabShipData.Instance;
           
        }

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
        private void DisplayClickedShipName()
        {
            Debug.LogError($"Ship name is {currentShip.GetShipName()}");
        }

        private void Start()
        {
           timeToFillCapacity= currentShip.GetFishingDuration() * 60;


        }

        public float GetTimeToFillCapacity()
        {
            return currentShip.GetFishingDuration() * 60;
        }


        private IEnumerator FishingCorutine()
        {


            if (currentShip.CanFish())
            {

                do
                {
                    Debug.Log("Fishing.....................................");
                    yield return new WaitForSeconds(60);

                    List<SerializableFishData> caughtFish = FillShipCapacity(currentShip.GetNumberOfFishCaughtPerMin());
                    currentShip.GetCaughtFishList().AddRange(caughtFish);
                    currentShip.UpdateCurrentCapacity();
                    // here we send api request to the server to update the current capacity
                    Debug.Log($"{currentShip.GetNumberOfFishCaughtPerMin()} fishes has been caught by the ship in 1 min");

                } while (!currentShip.IsCapacityFull());

                Debug.Log("The Ship is full now");
                
                //PlayFabPlayerData.Instance.AddFishToPlayerData(currentShip.GetCaughtFishList());
                //currentShip.ClearCapacity();
                //currentShip.GetCaughtFishList().Clear();
            }
            else
            {
                Debug.Log("Cant fish the ship is busy");
            }

        }

        private List<SerializableFishData> FillShipCapacity(int numberOfFishes)
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

        public void OnFishingSuccess(string message)
        {
            Debug.Log(message);
        }


        public void StartFishing()
        {
            startFishing = true;
            fishingCourtineRef = FishingCorutine();
            StartCoroutine(fishingCourtineRef);
            Debug.Log(fishingCourtineRef.GetHashCode());

        }
        private void Update()
        {
            if (startFishing)
            {
              
                DisplayFishingTimer();
            }


        }

       private void DisplayFishingTimer()
        {
          if (!startFishing) return;
            timeToFillCapacity -= Time.deltaTime;
            //TimeSpan time = TimeSpan.FromSeconds(timeToFillCapacity);
            //Debug.Log($"{time.Minutes.ToString()} : {time.Seconds.ToString()}");
        }


        public void CancelFishing()
        {
            StopCoroutine(fishingCourtineRef);
            Debug.Log(fishingCourtineRef.GetHashCode());
            currentShip.ClearCapacity();
            startFishing = false;
            Debug.Log("Fishing is canclled");
        }

        /**
         * Gets the current fish storage from the serever
         * then update it with updated data then send it back to the server
         */
        
        public void StoreCaughtFishButton()
        {
            Debug.Log($"{currentShip.GetShipName()}");
            clickedShipName = currentShip.GetShipName();
            completeButton.gameObject.SetActive(false);
            shipDataService.GetFishJsonValue(); // invoke event that updateFishStorage() listens to it
        }

        public void UpdateFishStorage(string fishJson)
        {
            if (currentShip.GetShipName() != clickedShipName) return;
            Debug.Log("Inside UpdateFishStorage()");


            Dictionary<string,int> currentFishStorage= JsonConvert.DeserializeObject<Dictionary<string, int>>(fishJson);
            Debug.Log(fishJson);
            foreach(var fish in currentShip.GetCaughtFishList())
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

}