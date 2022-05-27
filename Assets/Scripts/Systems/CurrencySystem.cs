using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FishGame.Systems
{
    public class CurrencySystem : MonoBehaviour
    {
        private static CurrencySystem _instance;

        public static CurrencySystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CurrencySystem();
                }
                return _instance;
            }
        }
        
        int coins, gems;

        public UnityEvent OnCoinsUpdated;
        public UnityEvent OnGemsUpdated;
        public CurrencySystem()
        {
            _instance = this;
            _instance.coins = 0;
            _instance.gems = 0;
           
        }


        public void AddCoins(int amount)
        {
            if (amount <= 0) return;
            coins += amount;
            OnCoinsUpdated?.Invoke();
        }
        
        public void AddGems(int amount)
        {
            if (amount <= 0) return;
            gems += amount;
            OnGemsUpdated?.Invoke();
        }
        
        public void SubtractCoins(int amount)
        {
            if (amount > coins) return;
            coins -= amount;
            OnCoinsUpdated?.Invoke();
        }

        public void SubtractGems(int amount)
        {
            if (amount > gems) return;
            gems -= amount;
            OnCoinsUpdated?.Invoke();
        }

        // used for initliaze the class
        public void SetCoin(int coinsAmount)
        {
            coins = coinsAmount;
        }

        public void SetGems(int gemsAmount)
        {
            gems = gemsAmount;
        }
        //////////////////////////////

    }

}