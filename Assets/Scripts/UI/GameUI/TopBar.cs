using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishGame.Core;
using FishGame.Systems;

public class TopBar : MonoBehaviour
{

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text gemText;

    CurrencySystem currencySystem;
    PlayFabCurrency currencyService;

    
    private void Start()
    {
        currencySystem = CurrencySystem.Instance;
        currencyService = PlayFabCurrency.Instance;
        currencyService.GetUserCurrency();
        currencySystem.OnCoinsAdded.AddListener();
        currencyService.OnGetUserCurrencySuccess.AddListener(SetCoinsAndGems);
    }

    public void SetCoinsAndGems(int coin , int gem)
    {
        currencySystem.SetCoin(coin);
        currencySystem.SetGems(gem);
        coinText.text = currencySystem.GetCoins().ToString();
        gemText.text = currencySystem.GetGems().ToString();

      
    }

    

}
