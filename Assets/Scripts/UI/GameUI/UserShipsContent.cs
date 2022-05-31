using FishGame.Core;
using FishGame.Scenes;
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


       [SerializeField] List<Ship> ownedShips;
       [SerializeField] List<Ship> AllShips;
        PlayFabShipData shipDataService;
        ListUtil listUtilService;


        private string allShipsContentTag = "all_ships_content";
        private string ownedShipsContentTag = "owned_ships_content";
        private string shipBoxTag = "ship_box_ui";

        public static bool isUserShipsUpdated = false;
        [SerializeField]  GameObject shipsPanel;


        private void Awake()
        {
            ownedShips = new List<Ship>();
            AllShips = new List<Ship>();

            shipDataService = PlayFabShipData.Instance;
            listUtilService = ListUtil.Instance;
            shipDataService.getAllShipsEventSuccess.AddListener(OnGetAllShipsSuccess);
            shipDataService.getOwnedShipsListEventSuccess.AddListener(GetOwnedShipsSuccess);
            shipDataService.updateOwnedShipsSuccessEvent.AddListener(OwnedShipsUpdatedSuccess);
        }

        private void OwnedShipsUpdatedSuccess(string arg0)
        {

            Debug.LogError($"Inside OnUpdatedOwnedShipsSuccess() in UserShipsContent.cs");

            DestroyAndClearShipBoxes();
            //get ownedShips is called again from GameManager.cs
            shipDataService.GetAllShips();





        }

        private void Start()
        {

            shipDataService.GetAllShips();
            shipDataService.GetOwnedShips();



        }
        private void GetOwnedShipsSuccess(List<SerializableShipData> ownedShipsSerializae)
        {
            Debug.LogError($"Inside GetOwnedShipsSuccess() in UserShipsContent.cs");

            ownedShips = listUtilService.DeserialzeShipDataToShipList(ownedShipsSerializae);
            foreach (var ship in ownedShips)
            {
                FillShipBoxItem(ship, ownedShipContent);

            }

        }


        private void OnGetAllShipsSuccess(List<SerializableShipData> ships)
        {
            Debug.LogError($"Inside OnGetAllShipsSuccess() in UserShipsContent.cs");
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
            shipItem.SetShipImage(ship.GetShipIcon());
            EnableButtonsByTag(shipItem,ship);

        }

        private void EnableButtonsByTag(ShipBox shipItem,Ship ship)
        {
            string parentTag = shipItem.transform.parent.tag;
            if (parentTag == allShipsContentTag)
            {
                //if user owns the ship dont show buy button
                if (!GameManager.GetOwnedShipsList().Contains(ship))
                {
                    shipItem.buyButton.gameObject.SetActive(true);
                    shipItem.buyButton.onClick.AddListener(()=>OnClickBuyButton(ship));
                }

            }
            else
            {
             //   shipItem.equipButton.gameObject.SetActive(true);

                if(GameManager.GetOwnedShipsList().Count > 1)
                {
                    shipItem.sellButton.gameObject.SetActive(true);
                    shipItem.sellButton.onClick.AddListener(() => OnClickSellButton(ship));
                }

            }
        }

        private void OnClickBuyButton(Ship ship)
        {
            if (GameManager.GetOwnedShipsList().Count == 3)
            {
                Debug.Log("You already have 3 ships you cant buy more !!");
                return;
            }
            else
            {
                Debug.Log($"Inside OnClickBuyButton() For {ship} ship ");
                ConfirmBuyUI confirmPanel = FindObjectOfType<ConfirmBuyUI>(true);
                confirmPanel.SetSelectedShip(ship);
                confirmPanel.gameObject.SetActive(true);
            }
        }

        public void OnClickSellButton(Ship ship)
        {
            Debug.Log($"Inside OnClickSellButton() For {ship} ship ");
            ConfirmSellUI confirmPanel = FindObjectOfType<ConfirmSellUI>(true);
            confirmPanel.SetSelectedShip(ship);
            confirmPanel.gameObject.SetActive(true);

        }


        public void DestroyAndClearShipBoxes()
        {
            if (!isUserShipsUpdated) return ;

            //clears both list
            ownedShips.Clear();
            AllShips.Clear();


            foreach (Transform transform in ownedShipContent)
            {
                Destroy(transform.gameObject);
            }

            foreach (Transform transform in allShipsContent)
            {
                Destroy(transform.gameObject);
            }
          
            
        }


    }

}