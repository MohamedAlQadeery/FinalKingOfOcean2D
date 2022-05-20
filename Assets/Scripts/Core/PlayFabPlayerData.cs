using FishGame.Fishes;
using FishGame.Ships;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FishGame.Core
{
  
    /*
     * Custom Event class for ships data
     */

    [Serializable]
    public class PlayFabPlayerFishesEvent : UnityEvent<List<Fish>> {}
   
    
    public class PlayFabPlayerData : MonoBehaviour
    {
        
        private const string shipsKey = "ships";
        private const string mainShipKey = "main_ships";
        private const string levelKey = "level";
        private const string fishKey = "fishes";
        private const string fishesFolderName = "Fishes";


        [SerializeField] PlayFabPlayerShipsListEvent getPlayerShipsSuccessEvent;
        [SerializeField] PlayFabEvent errorEvent;
        [SerializeField] PlayFabEvent successEvent;

        public static PlayFabPlayerData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayFabPlayerData();
                }
                return _instance;
            }
        }
        private static PlayFabPlayerData _instance;

        public PlayFabPlayerData()
        {
            _instance = this;
        }

        public void GetPlayerShipsData()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetPlayerShipsSuccess, OnError);
        }

        private void OnGetPlayerShipsSuccess(GetUserDataResult result)
        {
            if (result.Data != null)
            {
                List<SerializableShipData> playerShips = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[shipsKey].Value);
                getPlayerShipsSuccessEvent?.Invoke(playerShips);
            }
            else
            {
                errorEvent?.Invoke("There was an error in fetching player data ..");
            }

          

        }

        //set up for newly created players where we give them there first ships
        public void NewPlayerSetup(List<SerializableShipData> newShips)
        {
            
            string newShipsToJson = JsonConvert.SerializeObject(newShips);
            Dictionary<string, int> fishesDic = new Dictionary<string, int>();
            List<Fish> fishesList = Resources.LoadAll<Fish>(fishesFolderName).ToList();
            foreach(Fish fish in fishesList)
            {
                fishesDic.Add(fish.GetName(),0);
                Debug.LogError($"Key is {fish.GetName()} And Value = {fishesDic[fish.GetName()]}");
            }
            string newFishesToJson = JsonConvert.SerializeObject(fishesDic);
            Debug.Log($"Final Json = {newFishesToJson}");

            var shipRequest = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> {
                { shipsKey,newShipsToJson},
                {mainShipKey,newShipsToJson },
                {levelKey,"0" },
                {fishKey,newFishesToJson }



              }
            };

            PlayFabClientAPI.UpdateUserData(shipRequest, OnSetupNewPlayerShipsSuccess, OnError);

        }


        private void OnSetupNewPlayerShipsSuccess(UpdateUserDataResult result)
        {
            successEvent?.Invoke("Data updated successfully");
        }

        private void OnError(PlayFabError error)
        {
            errorEvent?.Invoke($"Error : {error.GenerateErrorReport()}");
        }

        public void AddFishToPlayerData(List<SerializableFishData> fishList)
        {
            string fishListToJson = JsonConvert.SerializeObject(fishList);
            
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    {fishKey,fishListToJson }
                },
            };

            PlayFabClientAPI.UpdateUserData(request,AddPlayerFishSuccess,OnError);
        }

        private void AddPlayerFishSuccess(UpdateUserDataResult result)
        {
            successEvent?.Invoke("Fishses has been added to player successfully");
        }
    }

}