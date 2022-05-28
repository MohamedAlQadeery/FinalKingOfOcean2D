using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishGame.Core;
using FishGame.Systems;
using UnityEngine.UI;
using System;

public class TopBar : MonoBehaviour
{

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text gemText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text experinceText;
    [SerializeField] Slider experinceSlider;


    CurrencySystem currencySystem;
    PlayFabCurrency currencyService;
    LevelSystem levelSystem;
    PlayFabPlayerLevel levelService;


    private void Awake()
    {
        currencySystem = CurrencySystem.Instance;
        currencyService = PlayFabCurrency.Instance;
        currencyService.GetUserCurrency();
        currencyService.OnGetUserCurrencySuccess.AddListener(SetCoinsAndGems);
        levelService = PlayFabPlayerLevel.Instance;
        levelSystem = LevelSystem.Instance;

        levelService.OnLeveLUpdatedSuccess.AddListener(OnLevelUpateSuccess);
        levelService.OnExpUpdatedSuccess.AddListener(OnExperinceUpdateSuccess);
        levelService.OnGetLevelAndExpSuccess.AddListener(OnGetLevelAndExpSuccess);
    }
    private void Start()
    {
       
        
         levelService.GetUserCurrentLevelAndExp();

    }

    private void OnGetLevelAndExpSuccess(int level, int exp)
    {
        experinceText.text = $"{exp}/{levelSystem.GetExperinceToNextLevel(level)}";
        levelText.text = level.ToString();
    }

    private void OnExperinceUpdateSuccess()
    {
        experinceText.text = $"{levelSystem.GetCurrentExperince()}/{levelSystem.GetExperinceToNextLevel(levelSystem.GetCurrentLevel())}";

    }

    
    private void OnLevelUpateSuccess()
    {
        levelText.text = levelSystem.GetCurrentLevel().ToString();

    }

    public void SetCoinsAndGems(int coin , int gem)
    {
        currencySystem.InitCurrencySystem(coin, gem);
        coinText.text = currencySystem.GetCoins().ToString();
        gemText.text = currencySystem.GetGems().ToString();

      
    }



    

}
