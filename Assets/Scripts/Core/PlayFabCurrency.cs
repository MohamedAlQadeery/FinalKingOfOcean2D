using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.Events;
using FishGame.Systems;

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
        private static PlayFabCurrency _instance;
        public static PlayFabCurrency Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayFabCurrency();
                }
                return _instance;
            }

        }
        CurrencySystem currencySystem;
        public PlayFabCurrency()
        {
            _instance = this;
        }
        private void Awake()
        {
            currencySystem = CurrencySystem.Instance;
            
        }

       

        private void OnEnable()
        {

            currencySystem.OnCoinsAdded.AddListener(AddUserCoinsCurrency);
            currencySystem.OnCoinSubtracted.AddListener(SubtractUserCoinsCurrency);
            currencySystem.OnGemsAdded.AddListener(AddUserGemsCurrency);
            currencySystem.OnGemsSubtracted.AddListener(SubtractUserGemsCurrency);
        }

        private void OnDisable()
        {

            currencySystem.OnCoinsAdded.RemoveListener(AddUserCoinsCurrency);
            currencySystem.OnCoinSubtracted.RemoveListener(SubtractUserCoinsCurrency);
            currencySystem.OnGemsAdded.RemoveListener(AddUserGemsCurrency);
            currencySystem.OnGemsSubtracted.RemoveListener(SubtractUserGemsCurrency);
        }


        [HideInInspector]  public PlayFabCurrencyEvent OnGetUserCurrencySuccess;

       [HideInInspector]public PlayFabCurrencyUpdatedEvent OnCoinsAddedSuccess;
        [HideInInspector]public PlayFabCurrencyUpdatedEvent OnGemsAddedSuccess;

        [HideInInspector] public PlayFabCurrencyUpdatedEvent OnCoinsSubtractedSuccess;
        [HideInInspector] public PlayFabCurrencyUpdatedEvent OnGemsSubtractedSuccess;

        [SerializeField] PlayFabEvent errorEvent;
      
        private const string coinsKey = "CN";        
        private const string gemsKey = "GM";



     



        public void GetUserCurrency()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnSuccessGetUserCurrency, OnError);
        }

        private void OnError(PlayFabError error)
        {
            errorEvent?.Invoke($"Error : {error.GenerateErrorReport()}");
        }

        private void OnSuccessGetUserCurrency(GetUserInventoryResult result)
        {
            OnGetUserCurrencySuccess?.Invoke(result.VirtualCurrency["CN"],result.VirtualCurrency["GM"]);
        }

        
        public void AddUserCoinsCurrency(int amount)
        {
            var request = new AddUserVirtualCurrencyRequest
            {
                VirtualCurrency = coinsKey,
                Amount = amount
                
            };

            PlayFabClientAPI.AddUserVirtualCurrency(request,result=> {
                OnCoinsAddedSuccess?.Invoke(result.Balance);
            
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
                OnCoinsSubtractedSuccess?.Invoke(result.Balance);

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
                OnGemsAddedSuccess?.Invoke(result.Balance);

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
                OnGemsSubtractedSuccess?.Invoke(result.Balance);

            }, OnError);
        }


    }

}