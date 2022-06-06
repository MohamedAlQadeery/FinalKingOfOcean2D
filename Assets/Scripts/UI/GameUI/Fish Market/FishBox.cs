using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FishGame.UI.GameUI.FishMarketUI
{
    public class FishBox : MonoBehaviour
    {
        [SerializeField] Image fishIcon;
        [SerializeField] TMP_Text fishName;
        [SerializeField] TMP_Text fishPrice;
        [SerializeField] TMP_Text fishQuantity;


        public void SetFishIcon(Sprite icon)
        {
            fishIcon.sprite = icon;
        }

        public void SetFishName(string name)
        {
            fishName.text = name;
        }

        public void SetFishPrice(float price)
        {
            fishPrice.text = price.ToString();
        }

        public void SetFishQuantity(int quantity)
        {
            fishQuantity.text = quantity.ToString();
        }

    }

}