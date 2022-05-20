using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using FishGame.Ships;
using UnityEngine.Events;
using Newtonsoft.Json;

namespace FishGame.Core
{

    /*
   * Custom Event class for ships data
   */

    [Serializable]
    public class PlayFabPlayerShipsListEvent : UnityEvent<List<SerializableShipData>> { }
    [Serializable]

    public class PlayFabPlayerShipEvent : UnityEvent<SerializableShipData> { }


    public class PlayFabShipData : MonoBehaviour
    {
        private static PlayFabShipData _instance;
        private const string shipKey = "ships";
        private const string mainShipsKey = "main_ships";
        

       [SerializeField] PlayFabPlayerShipEvent getShipSuccessEvent;
        [SerializeField] PlayFabPlayerShipsListEvent getMainShipsListEventSuccess;
        [SerializeField] PlayFabPlayerShipsListEvent getAllShipsListEventSuccess;


        [SerializeField] PlayFabError errorEvent;
        [SerializeField] PlayFabEvent updateMainShipsSuccessEvent;

        public static PlayFabShipData Instance
        {
            get {
               if(_instance == null)
                {
                    _instance = new PlayFabShipData();
                }
                return _instance;
            }       
        }

        public PlayFabShipData()
        {
            _instance = this;
        }
        public void GetAllPlayerShips()
        {

            var request = new GetUserDataRequest
            {
                Keys =new List<string> { shipKey},
            };
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetAllPlayerShipsSuccess, OnError);

        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError($"{error.GenerateErrorReport()}");
        }

        private void OnGetAllPlayerShipsSuccess(GetUserDataResult result)
        {
          
            List<SerializableShipData> allUserShips = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[shipKey].Value);
           
           getAllShipsListEventSuccess?.Invoke(allUserShips);
        }


        //Get specifc ship by name
        public void GetShip(string name)
        {
            name = "first ship";
            var request = new GetUserDataRequest
            {
                Keys = new List<string> { shipKey },
            };
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
               result => {
                   List<SerializableShipData> shipsList = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[shipKey].Value);

                   foreach(SerializableShipData ship in shipsList)
                   {
                       if(ship.shipName == name.ToLower())
                       {
                           Debug.Log($"Ship is found : {ship.shipName}");
                           getShipSuccessEvent?.Invoke(ship);
                           return;
                       }
                   }
               
               } ,OnError
                
              );
        }


        public void GetMainShips()
        {
            var request = new GetUserDataRequest
            {
                Keys = new List<string> { mainShipsKey },
            };

            PlayFabClientAPI.GetUserData(request,OnGetMainShipsListSuccess,OnError );

        }

        private void OnGetMainShipsListSuccess(GetUserDataResult result)
        {
          
            List<SerializableShipData> mainShipsList = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[mainShipsKey].Value);
            getMainShipsListEventSuccess?.Invoke(mainShipsList);

        }

        /**
         * User this function to update player main ships
         */

        public void UpdateMainShips(List<Ship> mainShipsList)
        {
           string shipsToJson= GetJsonStringFromMainShipsList(mainShipsList);
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> { {mainShipsKey, shipsToJson } },
            };
            PlayFabClientAPI.UpdateUserData(request,OnUpdateMainShipsSucess,OnError);
        }

        private void OnUpdateMainShipsSucess(UpdateUserDataResult result)
        {
            updateMainShipsSuccessEvent?.Invoke("Main Ships has been updated ");
        }

        public string GetJsonStringFromMainShipsList(List<Ship> mainShipsList)
        {
            List<SerializableShipData> serializableMainShips = new List<SerializableShipData>();

            foreach (Ship ship in mainShipsList)
            {
                ship.SetIsMainShip(true); 
                serializableMainShips.Add(ship.GetDataToJson());
            }

            return JsonConvert.SerializeObject(serializableMainShips);
        }


       
    }

}