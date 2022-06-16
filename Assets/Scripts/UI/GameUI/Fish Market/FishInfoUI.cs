using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using FishGame.Core;
using FishGame.Systems;

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

        SoldFishUI soldFish;
        CurrencySystem currencySystem;
        PlayFabCurrency currencyService;
        public static Action<string> OnSellFish;

        private void Awake()
        {
            PlayFabFishData.OnFishSoldSuccessfully += HandleFishSoldSuccessfully;
        }
        private void Start()
        {
            currencySystem = CurrencySystem.Instance;
            currencyService = PlayFabCurrency.Instance;
            soldFish = FindObjectOfType<SoldFishUI>(true);
        }
        private void HandleFishSoldSuccessfully()
        {
            currencySystem.AddCoins(int.Parse(currentPrice.text));
            currencyService.GetUserCurrency();

            soldFish.SetFishName(fishName.text);
            soldFish.SetFishIcon(fishIcon.sprite);
            soldFish.SetTotalPrice(int.Parse(currentPrice.text));
            soldFish.SetIncome(currencySystem.GetCoins());
            soldFish.SetFishGoodness(100);

            soldFish.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            PlayFabFishData.OnFishSoldSuccessfully -= HandleFishSoldSuccessfully;

        }
        public void OnClickSellButton()
        {
            if(fishQuantity.text =="0")
            {
                Debug.Log("You have 0 fish");
                return;
            }
          
            Debug.Log("OnClickSellButton in FishInfoUI.cs");
            OnSellFish?.Invoke(fishName.text);

        }
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

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }

}