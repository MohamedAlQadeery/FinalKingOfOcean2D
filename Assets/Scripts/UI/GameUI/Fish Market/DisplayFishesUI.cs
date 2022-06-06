using FishGame.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.UI.GameUI.FishMarketUI
{
    public class DisplayFishesUI : MonoBehaviour
    {
        [SerializeField] Transform contentTransform;
        

        private void Awake()
        {
            PlayFabFishData.OnFishListUpdated += HandleOnFishListUpdated;
        }

     

        private void OnDestroy()
        {
            PlayFabFishData.OnFishListUpdated -= HandleOnFishListUpdated;

        }

        private void HandleOnFishListUpdated(Dictionary<string, int> fishDic)
        {
            
        }
    }

}