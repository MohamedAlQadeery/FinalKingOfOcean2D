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
        public static Action<Dictionary<string, int>> OnFishListUpdated;

        public void GetAllFishes()
        {

            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                Keys = new List<string>() { fishKey }

            }, OnGetAllFishesSuccess, null);
        }

        private void OnGetAllFishesSuccess(GetUserDataResult fishes)
        {
            Debug.Log($"Inside OnGetAllFishesSuccess()");
            string fishJson = fishes.Data[fishKey].Value;
            Dictionary<string, int> currentFishStorage = JsonConvert.DeserializeObject<Dictionary<string, int>>(fishJson);
            OnFishListUpdated?.Invoke(currentFishStorage);


        }
    }

}