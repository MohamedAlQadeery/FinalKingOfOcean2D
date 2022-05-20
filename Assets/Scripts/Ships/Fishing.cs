using FishGame.Core;
using FishGame.Fishes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.Ships
{
    public class Fishing : MonoBehaviour
    {
        [SerializeField] Ship currentShip;
        [SerializeField] float timeToFillCapacity;
        [SerializeField] bool startFishing = false;


        private void Start()
        {
           timeToFillCapacity= currentShip.GetFishingDuration() * 60;
           

        }
        public IEnumerator FishingCorutine()
        {


            if (currentShip.CanFish())
            {

                do
                {
                   
                    yield return new WaitForSeconds(60);

                    List<SerializableFishData> caughtFish = FillShipCapacity(currentShip.GetNumberOfFishCaughtPerMin());
                    currentShip.GetCaughtFishList().AddRange(caughtFish);
                    currentShip.UpdateCurrentCapacity();

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
            StartCoroutine(FishingCorutine());
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
            timeToFillCapacity -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeToFillCapacity);
            Debug.Log($"{time.Minutes.ToString()} : {time.Seconds.ToString()}");
        }
    }

}