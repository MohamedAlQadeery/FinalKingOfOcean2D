using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishGame.Fishes;
using FishGame.Utilities;
using System;

namespace FishGame.UI.GameUI.FishMarketUI
{
    public class FishBox : MonoBehaviour
    {
        [SerializeField] Image fishIcon;
        [SerializeField] TMP_Text fishName;
        [SerializeField] TMP_Text fishPrice;
        [SerializeField] TMP_Text fishQuantity;

        // we use hidden name to compare with the ship name and gets the correct ship
        [SerializeField] TMP_Text hiddenName;

        FishInfoUI fishInfo;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClickFishInfo);
        }
        private void Start()
        {
            fishInfo = FindObjectOfType<FishInfoUI>(true);
        }


        private void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClickFishInfo);

        }
        public void OnClickFishInfo()
        {
            Fish fish = ResourcesUtil.Instance.FindScriptableObjectFish(hiddenName.text);

            FillFishInfo(fish);
            fishInfo.gameObject.SetActive(true);

        }

        private void FillFishInfo(Fish fish)
        {

            fishInfo.SetFishName(fishName.text);
            fishInfo.SetFishQuantity(int.Parse(fishQuantity.text));
            fishInfo.SetCurrentPrice(int.Parse(fishPrice.text) * int.Parse(fishQuantity.text));
            fishInfo.SetFishGoodness(100);
            fishInfo.SetFishIcon(fish.GetFishIcon());
            //////////
            DateTime dateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
            fishInfo.SetNextDeal("10");
            ///////////////////
        }

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