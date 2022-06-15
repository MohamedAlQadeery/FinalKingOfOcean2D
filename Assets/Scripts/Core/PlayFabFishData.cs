using FishGame.Fishes;
using FishGame.UI.GameUI.FishMarketUI;
using FishGame.Utilities;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.Core
{
    public class PlayFabFishData : MonoBehaviour
    {
        private const string fishKey = "fishes";
        public static Action<Dictionary<Fish, int>> OnFishListUpdated;
        public static Action OnFishSoldSuccessfully;

        private void Awake()
        {
            DisplayFishesUI.OnGetAllFishes += GetAllFishes;
            DisplayFishesUI.OnGetLatestFishesPrices += HandleOnGetLatestFishPrices;
            FishInfoUI.OnSellFish += HandleOnSellFish;
        }

        private void HandleOnSellFish(string fishName)
        {
            var request = new GetUserDataRequest
            {
                Keys = new List<string> { fishKey }
            };

            PlayFabClientAPI.GetUserData(request,result => {
                string fishJson = result.Data[fishKey].Value;
                Dictionary<string, int> fishes = JsonConvert.DeserializeObject<Dictionary<string, int>>(fishJson);

                foreach (var fish in fishes)
                {
                    if(fish.Key == fishName)
                    {
                        fishes[fish.Key] = 0;
                        break;
                    }
                }

                string updatedFishJson = JsonConvert.SerializeObject(fishes);
                var updateRequest = new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string> { { fishKey, updatedFishJson } },
                };

                PlayFabClientAPI.UpdateUserData(updateRequest, success => { 
                    Debug.Log("Sold successfully");
                    OnFishSoldSuccessfully?.Invoke();


                }, error => { Debug.Log("Error in selling"); });
            }, null);
        }

        private void OnDestroy()
        {
            DisplayFishesUI.OnGetAllFishes -= GetAllFishes;
            DisplayFishesUI.OnGetLatestFishesPrices -= HandleOnGetLatestFishPrices;
            FishInfoUI.OnSellFish += HandleOnSellFish;

        }
        public void GetAllFishes()
        {

            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                Keys = new List<string>() { fishKey }

            }, OnGetAllFishesSuccess, error=> {

                Debug.Log(error.ErrorMessage);
            });
        }

        private void OnGetAllFishesSuccess(GetUserDataResult fishes)
        {
            Debug.Log($"Inside OnGetAllFishesSuccess()");
            string fishJson = fishes.Data[fishKey].Value;

            Dictionary<Fish, int> FishStorage = ListUtil.Instance.ConvertToStringKeyToFishType(JsonConvert.DeserializeObject<Dictionary<string, int>>(fishJson));
            OnFishListUpdated?.Invoke(FishStorage);


        }

        private void HandleOnGetLatestFishPrices()
        {
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest
            {
                Keys = new List<string>() { fishKey }

            },OnGetLatestFishPriceSuccess, error => {

                Debug.Log(error.ErrorMessage);
            });
        }

        private void OnGetLatestFishPriceSuccess(GetTitleDataResult res)
        {
            string fishJsonData = res.Data[fishKey];
            List<Fish> updatedFishList = JsonConvert.DeserializeObject<List<Fish>>(fishJsonData);
            List<Fish> FishesFromResouces = ResourcesUtil.Instance.GetFishFromResourcesFolder();
            UpdatingFishesPrices(updatedFishList, FishesFromResouces);

        }

        private static void UpdatingFishesPrices(List<Fish> updatedFishList, List<Fish> FishesFromResouces)
        {
            foreach (Fish fishR in FishesFromResouces)
            {
                string fishName = fishR.FishName;
                foreach (Fish fishU in updatedFishList)
                {
                    if (fishU.FishName == fishName)
                    {
                        fishR.CurrentPrice = fishU.CurrentPrice;
                        Debug.Log($"{fishU.FishName} price has been updated to : {fishU.CurrentPrice}");
                        continue;
                    }
                }
            }
        }
    }

}