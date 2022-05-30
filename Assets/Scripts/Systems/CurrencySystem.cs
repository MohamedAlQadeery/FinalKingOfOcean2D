using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FishGame.Systems
{
    public class CurrencyEvent : UnityEvent<int> { }
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

        //public UnityEvent OnCurrencyUpdated;
        public CurrencyEvent OnCoinsAdded;
        public CurrencyEvent OnCoinSubtracted;
         public CurrencyEvent OnGemsAdded;
        public CurrencyEvent OnGemsSubtracted;
        


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
            OnCoinsAdded?.Invoke(amount);
        }

        public void AddGems(int amount)
        {
            if (amount <= 0) return;
            gems += amount;
            OnGemsAdded?.Invoke(amount);
        }

        public void SubtractCoins(int amount)
        {
            if (amount > coins) return;
            coins -= amount;
            OnCoinSubtracted?.Invoke(amount);
        }

        public void SubtractGems(int amount)
        {
            if (amount > gems) return;
            gems -= amount;
            OnGemsSubtracted?.Invoke(amount);
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
        public int GetCoins()
        {
            return coins;
        }

        public int GetGems()
        {
            return gems;
        }

        public void InitCurrencySystem(int cn,int gm)
        {
            coins = cn;
            gems = gm;
        }

    }

}