using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishGame.Core;

public class TopBar : MonoBehaviour
{

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text gemText;

    private void Start()
    {
        PlayFabCurrency.instance.GetUserCurrency();
    }

    public void CoinsGems(int coin , int gem)
    {
        Debug.Log(coin+  " gem" +gem);
        coinText.text = coin.ToString();
        gemText.text = gem.ToString();
    }

    

}
