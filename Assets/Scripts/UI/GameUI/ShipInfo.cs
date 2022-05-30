using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FishGame.Fishes;

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

        //Start of Fish types
        [SerializeField] Image firstFishIcon;
        [SerializeField] Image secondFishIcon;
        [SerializeField] Image thridFishIcon;

        [SerializeField] TMP_Text firstFishName;
        [SerializeField] TMP_Text secondFishName;
        [SerializeField] TMP_Text thirdFishName;

        public void SetFirstFishIcon(Sprite fishSprite)
        {
            firstFishIcon.sprite = fishSprite;
        }
        public void SetSecondFishIcon(Sprite fishSprite)
        {
            secondFishIcon.sprite = fishSprite;
        } 
        public void SetThridFishIcon(Sprite fishSprite)
        {
            thridFishIcon.sprite = fishSprite;
        }
        

        public void SetFirstFishName(string name)
        {
            firstFishName.text = name;
        }
        public void SetSecondFishName(string name)
        {
            secondFishName.text = name;
        }
        public void SetThirdFishName(string name)
        {
            thirdFishName.text = name;
        }

        //End of Fish types

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
  
