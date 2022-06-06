using FishGame.Core;
using FishGame.Fishes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.UI.GameUI.FishMarketUI
{
    public class DisplayFishesUI : MonoBehaviour
    {
        [SerializeField] Transform contentTransform;
        [SerializeField] FishBox fishBoxPrefab;
        private Dictionary<Fish, int> displayedFishes;
        public static Action OnGetAllFishes;

        private void Awake()
        {
            displayedFishes = new Dictionary<Fish, int>();
            PlayFabFishData.OnFishListUpdated += HandleOnFishListUpdated;
        }

     

        private void OnDestroy()
        {
            PlayFabFishData.OnFishListUpdated -= HandleOnFishListUpdated;

        }

        private void Start()
        {
            OnGetAllFishes?.Invoke();
        }
        private void HandleOnFishListUpdated(Dictionary<Fish, int> fishStorage)
        {
            ClearCurrentFishStorage(fishStorage);
            foreach(var fish in fishStorage)
            {
                FillFishBox(fish);
            }



        }

        private void FillFishBox(KeyValuePair<Fish, int> fish)
        {
            FishBox fishBox = Instantiate(fishBoxPrefab, contentTransform);
            fishBox.SetFishName(fish.Key.GetName());
            fishBox.SetFishPrice(fish.Key.GetCurrentPrice());
            fishBox.SetFishIcon(fish.Key.GetFishIcon());
            fishBox.SetFishQuantity(fish.Value);
        }

        private void ClearCurrentFishStorage(Dictionary<Fish, int> fishStorage)
        {
            foreach (Transform oldFishBox in contentTransform)
            {
                Destroy(oldFishBox.gameObject);
            }
            displayedFishes.Clear();
            displayedFishes = fishStorage;
        }
    }

}