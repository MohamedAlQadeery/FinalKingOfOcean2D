using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishGame.UI.GameUI.FishMarketUI
{
    public class SoldFishUI : MonoBehaviour
    {
        [SerializeField] TMP_Text fishName;
        [SerializeField] TMP_Text totalPrice;
        [SerializeField] TMP_Text income;
        [SerializeField] TMP_Text fishGoodness;
        [SerializeField] Image fishIcon;

        public void SetFishName(string name)
        {
            fishName.text = name;
        }

        public void SetTotalPrice(float total)
        {
            totalPrice.text = total.ToString();
        }

        public void SetFishGoodness(int number)
        {
            fishGoodness.text = number.ToString();
        }

        public void SetFishIcon(Sprite image)
        {
            fishIcon.sprite = image;
        }


        public void SetIncome(float amount)
        {
            income.text = amount.ToString();
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
