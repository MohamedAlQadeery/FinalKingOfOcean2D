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

        float coins, gems;

        //public UnityEvent OnCurrencyUpdated;
        public CurrencyEvent OnCoinsAdded = new CurrencyEvent();
        public CurrencyEvent OnCoinSubtracted = new CurrencyEvent();
        public CurrencyEvent OnGemsAdded = new CurrencyEvent();
        public CurrencyEvent OnGemsSubtracted = new CurrencyEvent();
        


        public CurrencySystem()
        {
            _instance = this;
            _instance.coins = 0;
            _instance.gems = 0;
           
        }


        public void AddCoins(float amount)
        {
            if (amount <= 0) return;
            coins += amount;
            OnCoinsAdded?.Invoke(amount);
        }

        public void AddGems(float amount)
        {
            if (amount <= 0) return;
            gems += amount;
            OnGemsAdded?.Invoke(amount);
        }

        public void SubtractCoins(float amount)
        {
            if (amount > coins) return;
            coins -= amount;
            OnCoinSubtracted?.Invoke(amount);
        }

        public void SubtractGems(float amount)
        {
            if (amount > gems) return;
            gems -= amount;
            OnGemsSubtracted?.Invoke(amount);
        }

        // used for initliaze the class
        public void SetCoin(float coinsAmount)
        {
            coins = coinsAmount;
        }

        public void SetGems(float gemsAmount)
        {
            gems = gemsAmount;
        }
        //////////////////////////////
        public float GetCoins()
        {
            return coins;
        }

        public float GetGems()
        {
            return gems;
        }

        public void InitCurrencySystem(float cn, float gm)
        {
            coins = cn;
            gems = gm;
        }

    }
    public class CurrencyEvent : UnityEvent<float> { }

}