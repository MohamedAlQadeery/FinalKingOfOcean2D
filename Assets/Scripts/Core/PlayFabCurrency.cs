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


    [Serializable]
    public class PlayFabCurrencyUpdatedEvent : UnityEvent<int>
    {

    }
    public class PlayFabCurrency : MonoBehaviour
    {
        public static PlayFabCurrency instance;
        [SerializeField] PlayFabCurrencyEvent getUserCurrencySuccessEvent;
        [SerializeField] PlayFabCurrencyUpdatedEvent OnCoinsUpdatedSuccess;
        [SerializeField] PlayFabCurrencyUpdatedEvent OnGemsUpdatedSuccess;
        [SerializeField] PlayFabEvent errorEvent;
      
        private const string coinsKey = "CN";        
        private const string gemsKey = "GM";        


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

        
        public void AddUserCoinsCurrency(int amount)
        {
            var request = new AddUserVirtualCurrencyRequest
            {
                VirtualCurrency = coinsKey,
                Amount = amount
                
            };

            PlayFabClientAPI.AddUserVirtualCurrency(request,result=> {
                OnCoinsUpdatedSuccess?.Invoke(result.Balance);
            
            },OnError);
        }

        public void SubtractUserCoinsCurrency(int amount)
        {
            var request = new SubtractUserVirtualCurrencyRequest
            {
                VirtualCurrency = coinsKey,
                Amount = amount

            };

            PlayFabClientAPI.SubtractUserVirtualCurrency(request, result => {
                OnCoinsUpdatedSuccess?.Invoke(result.Balance);

            }, OnError);
        }


        public void AddUserGemsCurrency(int amount)
        {
            var request = new AddUserVirtualCurrencyRequest
            {
                VirtualCurrency = gemsKey,
                Amount = amount

            };

            PlayFabClientAPI.AddUserVirtualCurrency(request, result => {
                OnGemsUpdatedSuccess?.Invoke(result.Balance);

            }, OnError);
        }


        public void SubtractUserGemsCurrency(int amount)
        {
            var request = new SubtractUserVirtualCurrencyRequest
            {
                VirtualCurrency = gemsKey,
                Amount = amount

            };

            PlayFabClientAPI.SubtractUserVirtualCurrency(request, result => {
                OnGemsUpdatedSuccess?.Invoke(result.Balance);

            }, OnError);
        }


    }

}