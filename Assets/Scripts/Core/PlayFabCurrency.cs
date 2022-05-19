using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.Events;

namespace FishGame.Core
{

    [Serializable]
    public class PlayFabCurrencyEvent : UnityEvent<int,int>
    {

    }
    public class PlayFabCurrency : MonoBehaviour
    {
        public static PlayFabCurrency instance;
        [SerializeField] PlayFabCurrencyEvent getUserCurrencySuccessEvent;
        [SerializeField] PlayFabEvent errorEvent;
      


        private void Awake()
        {
            instance = this;
        }
        public void GetUserCurrency()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnSccuessGetUserInventory, OnError);
        }

        private void OnError(PlayFabError error)
        {
            errorEvent?.Invoke($"Error : {error.GenerateErrorReport()}");
        }

        private void OnSccuessGetUserInventory(GetUserInventoryResult result)
        {
           
            getUserCurrencySuccessEvent?.Invoke(result.VirtualCurrency["CN"],result.VirtualCurrency["GM"]);
        }
    }

}