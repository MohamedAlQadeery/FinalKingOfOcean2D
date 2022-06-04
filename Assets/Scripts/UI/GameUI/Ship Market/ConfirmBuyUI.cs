using FishGame.Core;
using FishGame.Scenes;
using FishGame.Ships;
using FishGame.Systems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishGame.UI.GameUI.ShipMarketUI
{
    public class ConfirmBuyUI : MonoBehaviour
    {
        Ship selectedShip;
        [SerializeField] Button confirmButton;
        [SerializeField] Button cancelButton;
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
            confirmButton.onClick.AddListener(OnClickConfirmButton);
            cancelButton.onClick.AddListener(OnClickCancelButton);
        }

        private void OnDisable()
        {
            confirmButton.onClick.RemoveListener(OnClickConfirmButton);
            cancelButton.onClick.RemoveListener(OnClickCancelButton);
        }

        public void OnClickConfirmButton()
        {
            Debug.Log($"Confirm Buy for {selectedShip.GetShipName()}");
           if(currencySystem.GetCoins() < selectedShip.GetBuyPrice())
            {
                Debug.Log($"You dont have enough coins to buy this ship ");
                gameObject.SetActive(false);

                return;
            }
            else
            {
                currencySystem.SubtractCoins(selectedShip.GetBuyPrice());

                GameManager.GetOwnedShipsList().Add(selectedShip);
                GameManager.isOwnedShipsModifed = true;
                UserShipsContent.isUserShipsUpdated = true;
                shipService.UpdateOwnedShips(GameManager.GetOwnedShipsList());
                currencyService.GetUserCurrency();
                Debug.Log($"{selectedShip} is Bought !!");


            }


            gameObject.SetActive(false);
        }


        public void OnClickCancelButton()
        {
            Debug.Log($"Canceled Buy for {selectedShip.GetShipName()}");
            gameObject.SetActive(false);
        }

        public void SetSelectedShip(Ship ship)
        {
            selectedShip = ship;
            shipName.text = selectedShip.GetShipName();

        }
    }

}