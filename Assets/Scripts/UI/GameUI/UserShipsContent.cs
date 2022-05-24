using FishGame.Core;
using FishGame.Ships;
using FishGame.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FishGame.UI.GameUI
{
    public class UserShipsContent : MonoBehaviour
    {
        [SerializeField] ShipBox shipTypePrefab;
        [SerializeField] Transform parentContent;

        List<Ship> userShips;
        PlayFabShipData shipDataService;
        ListUtil listUtilService;



        private void Awake()
        {
            shipDataService = PlayFabShipData.Instance;
            listUtilService = ListUtil.Instance;
            shipDataService.getUserShipsEventSuccess.AddListener(OnGetUserShipsSuccess);

        }

        private void Start()
        {
            shipDataService.GetAllPlayerShips();


        }

        private void OnGetUserShipsSuccess(List<SerializableShipData> ships)
        {
            Debug.Log("inside OnGetUserShipsSuccess ()");
            userShips = listUtilService.DeserialzeShipDataToShipList(ships);
            foreach (var ship in userShips)
            {
                ShipBox shipItem = Instantiate(shipTypePrefab, parentContent);
                shipItem.SetShipBoxName(ship.GetShipName());
                shipItem.SetShipBoxPrice("2000");
                shipItem.SetShipImage(ship.GetShipIcon());


            }
        }

        
    }

}