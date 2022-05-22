using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using FishGame.Ships;
using UnityEngine.Events;
using Newtonsoft.Json;
using FishGame.Fishes;

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
        private const string ownedShipKey = "owned_ships";
        private const string fishKey = "fishes";
        private const string mainShipsKey = "main_ships";
        

       [SerializeField] PlayFabPlayerShipEvent getShipSuccessEvent;
        [SerializeField] PlayFabPlayerShipsListEvent getMainShipsListEventSuccess;
        [SerializeField] PlayFabPlayerShipsListEvent getAllShipsListEventSuccess;


        [SerializeField] PlayFabError errorEvent;
        [SerializeField] PlayFabEvent updateMainShipsSuccessEvent;
        [SerializeField] PlayFabEvent GetFishJsonSuccess;
        [SerializeField] PlayFabEvent updateFishStorageSuccess;


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
                Keys =new List<string> { ownedShipKey},
            };
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetAllPlayerShipsSuccess, OnError);

        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError($"{error.GenerateErrorReport()}");
        }

        private void OnGetAllPlayerShipsSuccess(GetUserDataResult result)
        {
          
            List<SerializableShipData> allUserShips = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[ownedShipKey].Value);
           
           getAllShipsListEventSuccess?.Invoke(allUserShips);
        }


        //Get specifc ship by name
        //public void GetShip(string name)
        //{
        //    name = "first ship";
        //    var request = new GetUserDataRequest
        //    {
        //        Keys = new List<string> { ownedShipKey },
        //    };
        //    PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
        //       result => {
        //           List<SerializableShipData> shipsList = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[ownedShipKey].Value);

        //           foreach(SerializableShipData ship in shipsList)
        //           {
        //               if(ship.shipName == name.ToLower())
        //               {
        //                   Debug.Log($"Ship is found : {ship.shipName}");
        //                   getShipSuccessEvent?.Invoke(ship);
        //                   return;
        //               }
        //           }
               
        //       } ,OnError
                
        //      );
        //}


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
                serializableMainShips.Add(ship.GetDataToJson());
            }

            return JsonConvert.SerializeObject(serializableMainShips);
        }

        public void GetFishJsonValue()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest { 
                Keys = new List<string>(){fishKey}
            
            },  result => {
                     Debug.Log("Inside GetFishJsonValue() result");
                 GetFishJsonSuccess?.Invoke(result.Data[fishKey].Value);
              
            }, OnError);

           
        }

       
        public void UpdateFishStorage(Dictionary<string,int> fishDic)
        {
            string updatedFishJson = JsonConvert.SerializeObject(fishDic);
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>(){
                    {fishKey,updatedFishJson },
                }
            };

            PlayFabClientAPI.UpdateUserData(request,result=> {

                updateFishStorageSuccess?.Invoke("Fish storage has been updated !");
            },OnError);
        }
       
    }

}