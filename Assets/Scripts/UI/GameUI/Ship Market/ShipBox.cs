using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FishGame.Utilities;
using FishGame.Ships;
using FishGame.Fishes;

namespace FishGame.UI.GameUI.ShipMarketUI
{
    public class ShipBox : MonoBehaviour
    {
        [SerializeField] Image Image;
        [SerializeField] TMP_Text shipName;
        //[SerializeField] TMP_Text shipPrice;
        public Button sellButton;
        public Button buyButton;
        //public Button equipButton;
        //public Button unEquipButton;
         ShipInfo shipInfo;


        private void Start()
        {
            shipInfo = FindObjectOfType<ShipInfo>(true);
        }
        public void OnClickShipInfo()
        {
            Ship ship = ResourcesUtil.Instance.FindScriptableObjectShip(shipName.text);
            Debug.LogError(shipInfo);
            FillShipInfo(ship);
            shipInfo.gameObject.SetActive(true);
            
        }

        private void FillShipInfo(Ship ship)
        {
            shipInfo.SetShipName(ship.GetShipName());
            shipInfo.SetCapacity(ship.GetMaxCapacity().ToString());
            shipInfo.SetHealth(ship.GetMaxHealth().ToString());
            shipInfo.SetFishingTime($"{ship.GetFishingDuration()} min"); // we should add min in arabic
            shipInfo.SetSellPrice(ship.GetSellPrice().ToString()); // we should add price in arabic  
            shipInfo.SetBuyPrice(ship.GetBuyPrice().ToString()); // we should add price in arabic  

            //start of fish types info
           foreach(Fish fish in ship.GetCanFishTypesList())
            {
                shipInfo.InstantiateFishType(fish);
            }
            //end of fish types info
        }

        public void SetShipBoxName(string name)
        {
            shipName.text = name;
        }

        //public void SetShipBoxPrice(string price)
        //{
        //    shipPrice.text = price;
        //}

        public void SetShipImage(Sprite shipImage)
        {
            Image.sprite = shipImage;
        }

    }

}