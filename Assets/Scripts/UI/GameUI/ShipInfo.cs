using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FishGame.UI.GameUI
{
    public class ShipInfo : MonoBehaviour
    {
        [SerializeField] TMP_Text shipName;
        [SerializeField] TMP_Text capacity;
        [SerializeField] TMP_Text health;
        [SerializeField] TMP_Text price;
        [SerializeField] TMP_Text fishingTime;
        [SerializeField] Image shipIcon;


        public void SetShipName(string nameText)
        {
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

        public void SetPrice(string priceText)
        {
            price.text = priceText;
        }

        public void SetFishingTime(string fishingTimeText)
        {
            fishingTime.text = fishingTimeText;
        }

        public void SetShipIcon(Sprite icon)
        {
            shipIcon.sprite = icon;
        }
    }
}
  
