using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FishGame.Fishes;

namespace FishGame.UI.GameUI.ShipMarketUI
{
    public class ShipInfo : MonoBehaviour
    {
        [SerializeField] TMP_Text shipName;
        [SerializeField] TMP_Text capacity;
        [SerializeField] TMP_Text health;
        [SerializeField] TMP_Text sellPrice;
        [SerializeField] TMP_Text buyPrice;
        [SerializeField] TMP_Text fishingTime;

        [SerializeField] ShipFishType shipFishTypePrefab;
        [SerializeField] Transform canFishTypeTransform;
       
        public void InstantiateFishType(Fish fish)
        {
            ShipFishType fishBox = Instantiate(shipFishTypePrefab, canFishTypeTransform);
            fishBox.SetFishType(fish.GetFishIcon(), fish.GetName());
        }

       
        public void ClearOldFishTypes()
        {
            foreach(Transform transform in canFishTypeTransform)
            {
                Destroy(transform.gameObject);
            }
        }
        

        public void SetShipName(string nameText)
        {
            Debug.Log($"SetShipName = {nameText}");
            shipName.text = nameText;
        }

        public void SetCapacity(string capacityText)
        {
            capacity.text= capacityText;
        }

        public void SetHealth(string healthText)
        {
            health.text = healthText;
        }

        public void SetBuyPrice(string price)
        {
            Debug.Log($"SetBuyPrice = {price}");

            buyPrice.text = price;
        }

        public void SetSellPrice(string price )
        {
            sellPrice.text = price;
        }

        public void SetFishingTime(string fishingTimeText)
        {
            fishingTime.text = fishingTimeText;
        }

       

       
    }
}
  
