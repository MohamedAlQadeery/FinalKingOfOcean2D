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

        private void Awake()
        {
            DisplayFishesUI.OnGetAllFishes += GetAllFishes;
        }

        private void OnDestroy()
        {
            DisplayFishesUI.OnGetAllFishes -= GetAllFishes;
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
    }

}