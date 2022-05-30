using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishGame.Systems;
using FishGame.Scenes;
using FishGame.Core;

namespace FishGame.UI.GameUI
{
    public class ConfirmSellUI : MonoBehaviour
    {
        Ship selectedShip;
         [SerializeField]  Button confirmButton;
        [SerializeField]Button cancelButton;
        [SerializeField] TMP_Text shipName;
        CurrencySystem currencySystem;
        PlayFabShipData shipService;
        PlayFabCurrency currencyService;


        private void Awake()
        {
            currencySystem = CurrencySystem.Instance;
            shipService = PlayFabShipData.Instance;
            currencyService = PlayFabCurrency.Instance;
        }

        private void OnEnable()
        {
            confirmButton.onClick.AddListener(ConfirmSell);
            cancelButton.onClick.AddListener(Cancel);
        }

        private void OnDisable()
        {
            confirmButton.onClick.RemoveListener(ConfirmSell);
            cancelButton.onClick.RemoveListener(Cancel);
        }

        public void SetSelectedShip(Ship ship)
        {
            selectedShip = ship;
            shipName.text = selectedShip.GetShipName();

        }

        public Ship GetSelectedShip()
        {
            return selectedShip;
        }
        public void ConfirmSell()
        {
            Debug.Log($"Confirmed Sell for {selectedShip}");
            if(GameManager.GetOwnedShipsList().Count ==1)
            {
                Debug.LogError("You Cant Sell your last ship !!");
                gameObject.SetActive(false);
                return;
            }
            else
            {
                currencySystem.AddCoins(selectedShip.GetPrice());
                GameManager.GetOwnedShipsList().Remove(selectedShip);
                GameManager.isOwnedShipsModifed = true;
                UserShipsContent.isUserShipsUpdated = true; ;
                shipService.UpdateOwnedShips(GameManager.GetOwnedShipsList());
                currencyService.GetUserCurrency();
                Debug.Log($"{selectedShip} is sold !!");

            }
            gameObject.SetActive(false);



        }


        public void Cancel()
        {
            Debug.Log($"Sell Canceled !! for {selectedShip}");

            gameObject.SetActive(false);
        }
    }

}