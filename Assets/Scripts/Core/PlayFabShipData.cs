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
using System.Linq;

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
        private const string all_ships_key = "ships";
        private const string fishKey = "fishes";
        private const string owned_ships_key = "owned_ships";
        

       [SerializeField] PlayFabPlayerShipEvent getShipSuccessEvent;
        public PlayFabPlayerShipsListEvent getOwnedShipsListEventSuccess;
         public PlayFabPlayerShipsListEvent getAllShipsEventSuccess;


        [SerializeField] PlayFabError errorEvent;
        public PlayFabEvent updateOwnedShipsSuccessEvent;

        public PlayFabEvent GetFishJsonSuccess;
        public PlayFabEvent updateFishStorageSuccess;

        public static Action<SerializableShipData> OnUpdatedShipData;
      
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


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            ShipFishing.OnPaused += HandleShipFishingOnPaused;
            ShipFishing.OnBack += HandleShipFishingOnBack;
           // ShipUIManager.OnPaused += HandleShipUIManagerOnPaused;
           // ShipUIManager.OnBack += HandleShipFishingOnBack;

        }



        private void OnDestroy()
        {
            ShipFishing.OnPaused -= HandleShipFishingOnPaused;
            ShipFishing.OnBack -= HandleShipFishingOnBack;
           // ShipUIManager.OnPaused -= HandleShipUIManagerOnPaused;
           // ShipUIManager.OnBack -= HandleShipFishingOnBack;

        }

        private void HandleShipFishingOnBack(string name)
        {
            var request = new GetUserDataRequest
            {
                Keys = new List<string> { owned_ships_key }
            };

            PlayFabClientAPI.GetUserData(request,result => {
                List<SerializableShipData> serializableShipDatas = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[owned_ships_key].Value);

                foreach (var ship in serializableShipDatas)
                {
                    if(ship.shipName == name)
                    {
                        OnUpdatedShipData?.Invoke(ship);
                        break;
                    }
                }

            },OnError);
        }
        public void GetAllShips()
        {
           
            var request = new GetUserDataRequest
            {
                Keys =new List<string> (){ all_ships_key},
            };
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetAllShipsSuccess, OnError);

        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError($"{error.GenerateErrorReport()}");
        }

        private void OnGetAllShipsSuccess(GetUserDataResult result)
        {
          
            List<SerializableShipData> allShips = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[all_ships_key].Value);
           
           getAllShipsEventSuccess?.Invoke(allShips);
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


        public void GetOwnedShips()
        {
            var request = new GetUserDataRequest
            {
                Keys = new List<string>(){ owned_ships_key },
            };

            PlayFabClientAPI.GetUserData(request,OnGetOwnedShipsListSuccess,OnError );

        }

        private void OnGetOwnedShipsListSuccess(GetUserDataResult result)
        {
          
            List<SerializableShipData> ownedShipsList = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[owned_ships_key].Value);
            getOwnedShipsListEventSuccess?.Invoke(ownedShipsList);

        }

        /**
         * User this function to update player main ships
         */

        public void UpdateOwnedShips(List<Ship> ownedShipsList)
        {
           string shipsToJson= GetJsonStringFromOwnedShipsList(ownedShipsList);
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> { {owned_ships_key, shipsToJson } },
            };
            PlayFabClientAPI.UpdateUserData(request,OnUpdateOwnedShipsSucess,OnError);
        }

        private void OnUpdateOwnedShipsSucess(UpdateUserDataResult result)
        {
            updateOwnedShipsSuccessEvent?.Invoke("Owned Ships has been updated ");
        }

        public string GetJsonStringFromOwnedShipsList(List<Ship> ownedShipsList)
        {
            List<SerializableShipData> serializableOwnedShips = new List<SerializableShipData>();

            foreach (Ship ship in ownedShipsList)
            {
                serializableOwnedShips.Add(ship.GetDataToJson());
            }

            return JsonConvert.SerializeObject(serializableOwnedShips);
        }

        public void GetFishJsonValue()
        {

            PlayFabClientAPI.GetUserData(new GetUserDataRequest { 
                Keys = new List<string>(){fishKey}
            
            },  result => {
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



            }, OnError);
        }


    

        private  void HandleShipFishingOnPaused(SerializableShipData shipData)
        {
            List<SerializableShipData> ships;
            var getShipsRequest = new GetUserDataRequest {Keys= new List<string> { owned_ships_key } };

            PlayFabClientAPI.GetUserData(getShipsRequest, result =>
            {
                ships = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[owned_ships_key].Value);

                string shipsToJson = UpdatingShipFishingInfo(shipData, ships);

                var updateShipRequest = new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string> { { owned_ships_key, shipsToJson } },

                };

                PlayFabClientAPI.UpdateUserData(updateShipRequest, res =>
                {

                    Debug.Log(" PlayfabShipData.cs ShipFishing.cs Ship data is updated");

                }, OnError);


            }, OnError);

        

        }

        private static string UpdatingShipFishingInfo(SerializableShipData shipData, List<SerializableShipData> ships)
        {
            foreach (var ship in ships)
            {
                if (ship.shipName == shipData.shipName)
                {
                    ship.QuitTime = shipData.QuitTime;
                    ship.TimeToFill = shipData.TimeToFill;
                    ship.FishType = shipData.FishType;
                    ship.currentCapacity = shipData.currentCapacity;
                    ship.Stop = shipData.Stop;
                    ship.Fishing = shipData.Fishing;
                    ship.Xpos = shipData.Xpos;
                    ship.Ypos = shipData.Ypos;
                    break;
                }
            }


            string shipsToJson = JsonConvert.SerializeObject(ships);
            return shipsToJson;
        }


        private void HandleShipUIManagerOnPaused(SerializableShipData shipData)
        {
            List<SerializableShipData> ships;
            var getShipsRequest = new GetUserDataRequest { Keys = new List<string> { owned_ships_key } };

            PlayFabClientAPI.GetUserData(getShipsRequest, result =>
            {
                ships = JsonConvert.DeserializeObject<List<SerializableShipData>>(result.Data[owned_ships_key].Value);

                string shipsToJson = UpdatingShipUIMangerInfo(shipData, ships);

                var updateShipRequest = new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string> { { owned_ships_key, shipsToJson } },

                };

                PlayFabClientAPI.UpdateUserData(updateShipRequest, res =>
                {

                    Debug.Log(" PlayfabShipData.cs ShipUIManger.cs Ship data is updated");

                }, OnError);


            }, OnError);

        }


        private static string UpdatingShipUIMangerInfo(SerializableShipData shipData, List<SerializableShipData> ships)
        {
            foreach (var ship in ships)
            {
                if (ship.shipName == shipData.shipName)
                {
                    ship.Stop = shipData.Stop;
                    ship.Fishing = shipData.Fishing;
                    ship.Xpos = shipData.Xpos;
                    ship.Ypos = shipData.Ypos;
                    break;
                }
            }


            string shipsToJson = JsonConvert.SerializeObject(ships);
            return shipsToJson;
        }


    }



}