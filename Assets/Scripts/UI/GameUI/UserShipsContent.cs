using FishGame.Core;
using FishGame.Ships;
using FishGame.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FishGame.UI.GameUI
{
    public class UserShipsContent : MonoBehaviour
    {
        [SerializeField] ShipBox shipTypePrefab;
        [SerializeField] Transform allShipsContent;
        [SerializeField] Transform ownedShipContent;

        List<Ship> ownedShips;
        List<Ship> AllShips;
        PlayFabShipData shipDataService;
        ListUtil listUtilService;



        private void Awake()
        {
            shipDataService = PlayFabShipData.Instance;
            listUtilService = ListUtil.Instance;
            shipDataService.getAllShipsEventSuccess.AddListener(OnGetAllShipsSuccess);
            shipDataService.getOwnedShipsListEventSuccess.AddListener(GetOwnedShipsSuccess);
        }
        private void Start()
        {
            shipDataService.GetAllShips();
            shipDataService.GetOwnedShips();


        }
        private void GetOwnedShipsSuccess(List<SerializableShipData> ownedShipsSerializae)
        {
            Debug.LogError("Inside GetOwnedShipsSuccess()");
            ownedShips = listUtilService.DeserialzeShipDataToShipList(ownedShipsSerializae);
            foreach (var ship in ownedShips)
            {
                FillShipBoxItem(ship,ownedShipContent);

            }

        }


        private void OnGetAllShipsSuccess(List<SerializableShipData> ships)
        {
            Debug.LogError($"OnGetAllShipsSuccess() count :{ships.Count} ");
            Debug.LogError("Inside OnGetAllShipsSuccess()");
            AllShips = listUtilService.DeserialzeShipDataToShipList(ships);
            foreach (var ship in AllShips)
            {

                FillShipBoxItem(ship, allShipsContent);


            }
        }

        private void FillShipBoxItem(Ship ship, Transform contentParent)
        {
            ShipBox shipItem = Instantiate(shipTypePrefab, contentParent);
            shipItem.SetShipBoxName(ship.GetShipName());
            ////shipItem.SetShipBoxPrice("2000");
            shipItem.SetShipImage(ship.GetShipIcon());
        }
    }

}