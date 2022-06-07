using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishGame.Systems;
using FishGame.Scenes;
using FishGame.Core;

namespace FishGame.UI.GameUI.ShipMarketUI
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
        [SerializeField] GameObject succesBuyShip;

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
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
            Debug.Log($"Confirmed Sell for {selectedShip}");
            if(GameManager.GetOwnedShipsList().Count ==1)
            {
                Debug.LogError("You Cant Sell your last ship !!");
                gameObject.SetActive(false);
                return;
            }
            else
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.UpgradeBuildingSound);
                GameObject succesBuyShipo = Instantiate(succesBuyShip, transform.position, transform.rotation) as GameObject;
                succesBuyShipo.transform.SetParent(gameObject.transform, false);
                Destroy(succesBuyShipo,1);
                StartCoroutine("wait2");
                currencySystem.AddCoins(selectedShip.GetSellPrice());
                GameManager.GetOwnedShipsList().Remove(selectedShip);
                GameManager.isOwnedShipsModifed = true;
                UserShipsContent.isUserShipsUpdated = true;
                shipService.UpdateOwnedShips(GameManager.GetOwnedShipsList());
                currencyService.GetUserCurrency();
                Debug.Log($"{selectedShip} is sold !!");
                
            }
            //gameObject.SetActive(false);
        }
        IEnumerator wait2()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }

        public void Cancel()
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
            Debug.Log($"Sell Canceled !! for {selectedShip}");

            gameObject.SetActive(false);
        }
    }

}