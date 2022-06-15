using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FishGame.UI.GameUI.FishMarketUI
{
    public class FishInfoUI : MonoBehaviour
    {
        [SerializeField] TMP_Text fishName;
        [SerializeField] TMP_Text fishQuantity;
        [SerializeField] TMP_Text currentPrice;
        [SerializeField] TMP_Text nextDeal;
        [SerializeField] TMP_Text fishGoodness;
        [SerializeField] Image fishIcon;


        public void SetFishName(string name)
        {
            fishName.text = name;
        }


        public void SetFishQuantity(int quantity)
        {
            fishQuantity.text = quantity.ToString();
        }

        public void SetCurrentPrice(int price)
        {
            currentPrice.text = price.ToString();
        }

        public void SetNextDeal(string nextD)
        {
            nextDeal.text = nextD;
        }

        public void SetFishGoodness(int number)
        {
            fishGoodness.text = number.ToString();
        }

        public void SetFishIcon(Sprite image)
        {
            fishIcon.sprite = image;
        }
    }

}