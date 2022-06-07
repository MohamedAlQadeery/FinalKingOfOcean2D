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
        [SerializeField] GameObject errorBuyShip;
        [SerializeField] GameObject succesBuyShip;
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
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
            Debug.Log($"Confirm Buy for {selectedShip.GetShipName()}");
           if(currencySystem.GetCoins() < selectedShip.GetBuyPrice())
            {
                GameObject errorLogin = Instantiate(errorBuyShip, transform.position, transform.rotation) as GameObject;
                errorLogin.transform.SetParent(gameObject.transform, false);
                SoundManager.Instance.PlaySound(SoundManager.Sound.ErrorSound);
                Destroy(errorLogin, 1);
                Debug.Log($"You dont have enough coins to buy this ship ");
                //gameObject.SetActive(false);
                StartCoroutine("wait");
                return;
            }
            else
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.UpgradeBuildingSound);
                GameObject succesBuyShipo = Instantiate(succesBuyShip, transform.position, transform.rotation) as GameObject;
                succesBuyShipo.transform.SetParent(gameObject.transform, false);
                Debug.Log($"{selectedShip} is Bought !!");
                Destroy(succesBuyShipo ,1);
                StartCoroutine("wait2");
                currencySystem.SubtractCoins(selectedShip.GetBuyPrice());              
                GameManager.GetOwnedShipsList().Add(selectedShip);
                GameManager.isOwnedShipsModifed = true;
                UserShipsContent.isUserShipsUpdated = true;
                shipService.UpdateOwnedShips(GameManager.GetOwnedShipsList());
                currencyService.GetUserCurrency();                

            }


            //gameObject.SetActive(false);
        }
        IEnumerator wait2()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
            IEnumerator wait()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }

        public void OnClickCancelButton()
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
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