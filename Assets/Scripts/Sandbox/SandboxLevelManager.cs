using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using FishGame.Core;

public class SandboxLevelManager : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text expText;
    [SerializeField] Button addExpButton;
    LevelSystem levelSystem;
    PlayFabPlayerLevel levelService;

    private void Awake()
    {
        levelSystem = LevelSystem.Instance;
        levelService = PlayFabPlayerLevel.Instance;
        addExpButton.onClick.AddListener(OnClickAddExpButton);
        levelService.OnLeveLUpdatedSuccess.AddListener(OnLevelUpateSuccess);
        levelService.OnExpUpdatedSuccess.AddListener(OnExperinceUpdateSuccess);
        levelService.OnGetLevelAndExpSuccess.AddListener(OnGetExpAndLevelSuccess);
    }

    private void OnGetExpAndLevelSuccess(int level, int exp)
    {
        levelSystem.SetLevel(level);
        levelSystem.SetExperince(exp);
        levelText.text = $"Level : {levelSystem.GetCurrentLevel()}";
        expText.text = $"Experince : {levelSystem.GetCurrentExperince()}";
       
    }

    private void Start()
    {
        levelService.GetUserCurrentLevelAndExp();
    }

    public void OnExperinceUpdateSuccess()
    {
        expText.text = $"Experince : {levelSystem.GetCurrentExperince()}";
    }

    public void OnLevelUpateSuccess()
    {
        levelText.text = $"Level : {levelSystem.GetCurrentLevel()}";
    }

    private void OnClickAddExpButton()
    {
        Debug.Log($"Adding 50 Exp");
        levelSystem.AddExperince(50);   
    }
}
